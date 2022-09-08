using ComponentWrapperGenerator.Extensions;
using Microsoft.CodeAnalysis;
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
            new ("RefreshView", "PropertyChanged", "IsRefreshingChanged", typeArgument: "bool"),
            new ("TimePicker", "PropertyChanged", "TimeChanged", typeArgument: "TimeSpan"),
        };

        private INamedTypeSymbol _eventHandlerType;
        private bool _isBindEvent;

        private bool IsPropertyChangedEvent => MauiPropertyName == "PropertyChanged";
        private ITypeSymbol EventArgsType => _eventHandlerType.GetMethod("Invoke")?.Parameters[1].Type;

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

        internal static GeneratedPropertyInfo[] GetEventCallbackProperties(Compilation compilation, GeneratedComponentInfo componentInfo, IList<UsingStatement> usings)
        {
            var componentType = componentInfo.TypeSymbol;

            var requestedEvents = EventsToGenerate
                .Where(e => e.TypeName == componentType.Name)
                .Select(info =>
                {
                    var eventInfo = componentType.GetEvent(info.MauiEventName, includeBaseTypes: true);

                    if (eventInfo is null)
                        throw new Exception($"Cannot find event {info.TypeName}.{info.MauiEventName}.");

                    var generatedPropertyInfo = new GeneratedPropertyInfo(
                        compilation,
                        info.MauiEventName,
                        ComponentWrapperGenerator.GetTypeNameAndAddNamespace(componentType, usings),
                        ComponentWrapperGenerator.GetIdentifierName(componentType.Name),
                        info.ComponentEventName,
                        GetRenderFragmentType(eventInfo, info.TypeArgument, usings),
                        GeneratedPropertyKind.EventCallback,
                        usings);

                    generatedPropertyInfo._isBindEvent = info.TypeArgument != null;
                    generatedPropertyInfo._eventHandlerType = (INamedTypeSymbol)eventInfo.Type;
                    return generatedPropertyInfo;
                });

            var inferredEvents = componentType.GetMembers().OfType<IEventSymbol>()
                .Where(e => !componentInfo.Exclude.Contains(e.Name))
                .Where(e => e.DeclaredAccessibility == Accessibility.Public && IsBrowsable(e))
                .Select(eventInfo =>
                {
                    var isBindEvent = IsBindEvent(eventInfo, out var bindedProperty);

                    var eventCallbackName = isBindEvent ? $"{bindedProperty.Name}Changed" : GetEventCallbackName(eventInfo);

                    var typeArgumentName = bindedProperty is null ? null : ComponentWrapperGenerator.GetTypeNameAndAddNamespace(bindedProperty.Type, usings);

                    var generatedIPropertySymbol = new GeneratedPropertyInfo(
                        compilation,
                        eventInfo.Name,
                        ComponentWrapperGenerator.GetTypeNameAndAddNamespace(componentType, usings),
                        ComponentWrapperGenerator.GetIdentifierName(componentType.Name),
                        eventCallbackName,
                        GetRenderFragmentType(eventInfo, typeArgumentName, usings),
                        GeneratedPropertyKind.EventCallback,
                        usings);

                    generatedIPropertySymbol._isBindEvent = isBindEvent;
                    generatedIPropertySymbol._eventHandlerType = (INamedTypeSymbol)eventInfo.Type;
                    return generatedIPropertySymbol;
                });

            return requestedEvents.Concat(inferredEvents).ToArray();
        }

        private static string GetRenderFragmentType(IEventSymbol eventInfo, string callbackTypeArgument, IList<UsingStatement> usings)
        {
            if (callbackTypeArgument != null)
            {
                return $"EventCallback<{callbackTypeArgument}>";
            }

            var eventArgType = eventInfo.Type.GetMethod("Invoke").Parameters[1].Type;
            if (eventArgType.Name != nameof(EventArgs))
            {
                return $"EventCallback<{ComponentWrapperGenerator.GetTypeNameAndAddNamespace(eventArgType, usings)}>";
            }
            else
            {
                return "EventCallback";
            }
        }

        private static string GetEventCallbackName(IEventSymbol eventSymbol)
        {
            return eventSymbol.Name switch
            {
                "Clicked" => "OnClick",
                "Pressed" => "OnPress",
                "Released" => "OnRelease",
                _ => $"On{eventSymbol.Name}"
            };
        }

        private static bool IsBindEvent(IEventSymbol eventSymbol, out IPropertySymbol property)
        {
            var properties = eventSymbol.ContainingType.GetMembers().OfType<IPropertySymbol>()
                .Where(p => IsPublicProperty(p) && HasPublicSetter(p));

            property = properties.FirstOrDefault(p =>
                eventSymbol.Name == $"{p.Name}Changed"  // e.g. Value - ValueChanged
                || eventSymbol.Name == $"{p.Name}Selected"  // e.g. Date - DateSelected
                || eventSymbol.Name == $"Is{p.Name}Changed"  // e.g. Selected - IsSelectedChanged
                || $"Is{eventSymbol.Name}" == $"{p.Name}Changed"  // e.g. IsSelected - SelectedChanged
                || $"Is{eventSymbol.Name}" == $"{p.Name}");  // e.g. IsToggled - Toggled

            return property != null;
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
