using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComponentWrapperGenerator
{
    public partial class GeneratedPropertyInfo
    {
        private static readonly EventToGenerate[] EventsToGenerate = new EventToGenerate[]
        {
            new ("BaseShellItem","Appearing","OnAppearing"),
            new ("BaseShellItem","Disappearing","OnDisappearing"),
            new ("CheckBox","CheckedChanged","IsCheckedChanged", typeArgument:"double"),
            new ("DatePicker","DateSelected","DateChanged", typeArgument:"DateTime"),
            new ("Editor", "Completed", "OnCompleted"),
            new ("Entry", "Completed", "OnCompleted"),
            new ("ImageButton", "Clicked", "OnClick"),
            new ("ImageButton", "Pressed", "OnPress"),
            new ("ImageButton", "Released", "OnRelease"),
            new ("MenuItem", "Clicked", "OnClick"),
            new ("Page","Appearing","OnAppearing"),
            new ("Page","Disappearing","OnDisappearing"),
            new ("Shell","Navigated","OnNavigated"),
            new ("Shell","Navigating","OnNavigating"),
            new ("Button", "Clicked", "OnClick"),
            new ("Button", "Pressed", "OnPress"),
            new ("Button", "Released", "OnRelease"),
            new ("ScrollView", "Scrolled", "OnScrolled"),
            new ("Stepper", "ValueChanged", "ValueChanged", typeArgument: "double"),
            new ("VisualElement", "Focused", "OnFocused"),
            new ("VisualElement", "Unfocused", "OnUnfocused"),
            new ("VisualElement", "SizeChanged", "OnSizeChanged"),
        };

        private bool _isPropertyChangedEvent;
        private Type _eventHandlerType;

        private bool IsBindEvent => ComponentPropertyName.EndsWith("Changed");

        public string GetHandleEventCallbackProperty()
        {
            /* 
                case nameof(OnClick):
                    if (!Equals(OnClick, value))
                    {
                        void Clicked(object sender, System.EventArgs e) => OnClick.InvokeAsync();

                        OnClick = (EventCallback)value;
                        NativeControl.Clicked -= Clicked;
                        NativeControl.Clicked += Clicked;
                    }

                    return true; */

            var eventName = MauiPropertyName;
            string argument;

            if (IsBindEvent)
            {
                argument = $"NativeControl.{ComponentPropertyName.Replace("Changed", "")}";
            }
            else
            {
                argument = _eventHandlerType.IsGenericType ? "e" : "";
            }

            var localFunctionName = $"NativeControl{eventName}";

            return $@"                case nameof({ComponentPropertyName}):
                    if (!Equals({ComponentPropertyName}, value))
                    {{
                        void {localFunctionName}(object sender, {EventHandlerTypeString()} e) => {ComponentPropertyName}.InvokeAsync({argument});

                        {ComponentPropertyName} = ({ComponentPropertyType})value;
                        NativeControl.{eventName} -= {localFunctionName};
                        NativeControl.{eventName} += {localFunctionName};
                    }}
                    break;
";
        }

        internal static GeneratedPropertyInfo[] GetEventCallbackProperties(Type componentType, IList<UsingStatement> usings)
        {
            return EventsToGenerate
                .Where(e => e.TypeName == componentType.Name)
                .Select(info =>
                {
                    var isBindEvent = info.ComponentEventName.EndsWith("Changed");
                    var eventInfo = componentType.GetEvent(info.MauiEventName);

                    if (eventInfo is null)
                        throw new Exception($"Cannot find event {info.TypeName}.{info.MauiEventName}.");

                    var generatedPropertyInfo = new GeneratedPropertyInfo(
                        info.MauiEventName,
                        ComponentWrapperGenerator.GetTypeNameAndAddNamespace(componentType, usings),
                        ComponentWrapperGenerator.GetIdentifierName(componentType.Name),
                        info.ComponentEventName,
                        GetRenderFragmentPropertyType(eventInfo, info.TypeArgument, usings),
                        GeneratedPropertyKind.EventCallback,
                        usings);

                    generatedPropertyInfo._isPropertyChangedEvent = info.IsPropertyChangedEvent;
                    generatedPropertyInfo._eventHandlerType = eventInfo.EventHandlerType;
                    return generatedPropertyInfo;
                })
                .ToArray();
        }

        private static string GetRenderFragmentPropertyType(EventInfo eventInfo, string callbackTypeArgument, IList<UsingStatement> usings)
        {
            if (callbackTypeArgument != null)
            {
                return $"EventCallback<{callbackTypeArgument}>";
            }
            else if (eventInfo is null || eventInfo.EventHandlerType.IsGenericType)
            {
                var eventArgType = eventInfo.EventHandlerType.GetGenericArguments()[0];
                return $"EventCallback<{ComponentWrapperGenerator.GetTypeNameAndAddNamespace(eventArgType, usings)}>";
            }
            else
            {
                return "EventCallback";
            }
        }

        private string EventHandlerTypeString()
        {
            return !_eventHandlerType.IsGenericType
                ? "System.EventArgs"
                : GetTypeNameAndAddNamespace(_eventHandlerType.GenericTypeArguments[0]);
        }

        class EventToGenerate
        {
            public EventToGenerate(string typeName, string mauiEventName, string componentEventName, string typeArgument = null, bool isPropertyChanged = false)
            {
                TypeName = typeName;
                MauiEventName = mauiEventName;
                ComponentEventName = componentEventName;
                IsPropertyChangedEvent = isPropertyChanged;
                TypeArgument = typeArgument;
            }

            public EventToGenerate(string typeName, string eventName, bool isPropertyChanged = false)
                : this(typeName, eventName, eventName, null, isPropertyChanged)
            {
            }

            public string TypeName { get; set; }
            public string MauiEventName { get; set; }
            public string ComponentEventName { get; set; }
            public string TypeArgument { get; set; }
            public bool IsPropertyChangedEvent { get; set; }
        }
    }
}
