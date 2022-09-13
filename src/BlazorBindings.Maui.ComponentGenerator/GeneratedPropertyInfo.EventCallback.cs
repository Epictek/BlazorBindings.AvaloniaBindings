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

            var propertyChangedEvents = componentInfo.PropertyChangedEvents
                .Select(propertyForEvent =>
                {
                    var propertyInfo = componentType.GetProperty(propertyForEvent)
                        ?? throw new Exception($"Cannot find property {componentType.Name}.{propertyForEvent}.");

                    var eventInfo = componentType.GetEvent("PropertyChanged", includeBaseTypes: true);

                    var componentEventName = $"{propertyInfo.Name}Changed";

                    var generatedPropertyInfo = new GeneratedPropertyInfo(
                        compilation,
                        "PropertyChanged",
                        ComponentWrapperGenerator.GetTypeNameAndAddNamespace(componentType, usings),
                        ComponentWrapperGenerator.GetIdentifierName(componentType.Name),
                        componentEventName,
                        GetRenderFragmentType(null, propertyInfo.Type, usings),
                        GeneratedPropertyKind.EventCallback,
                        usings);

                    generatedPropertyInfo._isBindEvent = true;
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

                    var generatedPropertyInfo = new GeneratedPropertyInfo(
                        compilation,
                        eventInfo.Name,
                        ComponentWrapperGenerator.GetTypeNameAndAddNamespace(componentType, usings),
                        ComponentWrapperGenerator.GetIdentifierName(componentType.Name),
                        eventCallbackName,
                        GetRenderFragmentType(eventInfo, bindedProperty?.Type, usings),
                        GeneratedPropertyKind.EventCallback,
                        usings);

                    generatedPropertyInfo._isBindEvent = isBindEvent;
                    generatedPropertyInfo._eventHandlerType = (INamedTypeSymbol)eventInfo.Type;
                    return generatedPropertyInfo;
                });

            return propertyChangedEvents.Concat(inferredEvents).ToArray();
        }

        private static string GetRenderFragmentType(IEventSymbol eventInfo, ITypeSymbol callbackTypeArgument, IList<UsingStatement> usings)
        {
            if (callbackTypeArgument != null)
            {
                var typeName = ComponentWrapperGenerator.GetTypeNameAndAddNamespace(callbackTypeArgument, usings);
                return $"EventCallback<{typeName}>";
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
    }
}
