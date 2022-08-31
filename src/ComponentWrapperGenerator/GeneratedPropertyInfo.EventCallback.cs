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
            new ("CheckBox","CheckedChanged","IsCheckedChanged", typeArgument:"bool"),
            new ("DatePicker","DateSelected","DateChanged", typeArgument:"DateTime"),
            new ("Editor", "Completed", "OnCompleted"),
            new ("Entry", "Completed", "OnCompleted"),
            new ("ImageButton", "Clicked", "OnClick"),
            new ("ImageButton", "Pressed", "OnPress"),
            new ("ImageButton", "Released", "OnRelease"),
            new ("InputView", "TextChanged", "TextChanged", typeArgument: "string"),
            new ("MenuItem", "Clicked", "OnClick"),
            new ("Page","Appearing","OnAppearing"),
            new ("Page","Disappearing","OnDisappearing"),
            new ("Shell","Navigated","OnNavigated"),
            new ("Shell","Navigating","OnNavigating"),
            new ("Button", "Clicked", "OnClick"),
            new ("Button", "Pressed", "OnPress"),
            new ("Button", "Released", "OnRelease"),
            new ("RefreshView", "PropertyChanged", "IsRefreshingChanged", typeArgument: "bool"),
            new ("ScrollView", "Scrolled", "OnScrolled"),
            new ("Stepper", "ValueChanged", "ValueChanged", typeArgument: "double"),
            new ("TimePicker", "PropertyChanged", "TimeChanged", typeArgument: "TimeSpan"),
            new ("Slider", "DragCompleted", "OnDragCompleted"),
            new ("Slider", "DragStarted", "OnDragStarted"),
            new ("Slider", "ValueChanged", "ValueChanged", typeArgument: "double"),
            new ("Switch", "PropertyChanged", "IsToggledChanged", typeArgument: "bool"),
            new ("VisualElement", "Focused", "OnFocused"),
            new ("VisualElement", "Unfocused", "OnUnfocused"),
            new ("VisualElement", "SizeChanged", "OnSizeChanged"),
        };

        private Type _eventHandlerType;
        private bool _isBindEvent;

        private bool IsPropertyChangedEvent => MauiPropertyName == "PropertyChanged";
        private Type EventArgsType => _eventHandlerType.GetMethod("Invoke").GetParameters()[1].ParameterType;

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

            if (_isBindEvent)
            {
                var bindedPropertyName = ComponentPropertyName.Replace("Changed", "");
                argument = $"NativeControl.{bindedPropertyName}";
            }
            else
            {
                argument = _eventHandlerType.IsGenericType ? "e" : "";
            }

            var localFunctionName = $"NativeControl{eventName}";

            var localFunctionBody = _isBindEvent && IsPropertyChangedEvent
                ? $@"
                        {{
                            if (e.PropertyName == nameof({argument}))
                            {{
                                {ComponentPropertyName}.InvokeAsync({argument});
                            }}
                        }}"
                : $" => {ComponentPropertyName}.InvokeAsync({argument});";

            return $@"                case nameof({ComponentPropertyName}):
                    if (!Equals({ComponentPropertyName}, value))
                    {{
                        void {localFunctionName}(object sender, {GetTypeNameAndAddNamespace(EventArgsType)} e){localFunctionBody}

                        {ComponentPropertyName} = ({ComponentType})value;
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

                    var generatedIPropertySymbol = new GeneratedPropertyInfo(
                        info.MauiEventName,
                        ComponentWrapperGenerator.GetTypeNameAndAddNamespace(componentType, usings),
                        ComponentWrapperGenerator.GetIdentifierName(componentType.Name),
                        info.ComponentEventName,
                        GetRenderFragmentType(eventInfo, info.TypeArgument, usings),
                        GeneratedPropertyKind.EventCallback,
                        usings);

                    generatedIPropertySymbol._isBindEvent = info.TypeArgument != null;
                    generatedIPropertySymbol._eventHandlerType = eventInfo.EventHandlerType;
                    return generatedIPropertySymbol;
                })
                .ToArray();
        }

        private static string GetRenderFragmentType(EventInfo eventInfo, string callbackTypeArgument, IList<UsingStatement> usings)
        {
            if (callbackTypeArgument != null)
            {
                return $"EventCallback<{callbackTypeArgument}>";
            }

            var eventArgType = eventInfo.EventHandlerType.GetMethod("Invoke").GetParameters()[1].ParameterType;
            if (eventArgType != typeof(EventArgs))
            {
                return $"EventCallback<{ComponentWrapperGenerator.GetTypeNameAndAddNamespace(eventArgType, usings)}>";
            }
            else
            {
                return "EventCallback";
            }
        }

        class EventToGenerate
        {
            public EventToGenerate(string typeName, string mauiEventName, string componentEventName, string typeArgument = null)
            {
                TypeName = typeName;
                MauiEventName = mauiEventName;
                ComponentEventName = componentEventName;
                TypeArgument = typeArgument;
            }

            public EventToGenerate(string typeName, string eventName)
                : this(typeName, eventName, eventName, null)
            {
            }

            public string TypeName { get; set; }
            public string MauiEventName { get; set; }
            public string ComponentEventName { get; set; }
            public string TypeArgument { get; set; }
        }
    }
}
