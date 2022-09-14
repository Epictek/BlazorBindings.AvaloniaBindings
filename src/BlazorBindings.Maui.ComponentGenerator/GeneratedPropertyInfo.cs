using ComponentWrapperGenerator.Extensions;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ComponentWrapperGenerator
{
    public partial class GeneratedPropertyInfo
    {
        private static readonly Dictionary<string, string> Aliases = new()
        {
            ["FlyoutPage.Detail"] = "Detail"
        };

        private readonly IPropertySymbol _propertyInfo;
        private readonly IList<UsingStatement> _usings;
        private readonly Lazy<string> _componentPropertyNameLazy;
        private readonly Lazy<string> _componentTypeLazy;

        public GeneratedPropertyKind Kind { get; }
        public string MauiPropertyName { get; }
        public string MauiContainingTypeName { get; }
        public string ComponentName { get; }
        public Compilation Compilation { get; }
        public string ComponentPropertyName => _componentPropertyNameLazy.Value;
        public string ComponentType => _componentTypeLazy.Value;

        private GeneratedPropertyInfo(Compilation compilation,
                                      string mauiPropertyName,
                                      string mauiContainingTypeName,
                                      string componentName,
                                      string componentPropertyName,
                                      string componentType,
                                      GeneratedPropertyKind kind,
                                      IList<UsingStatement> usings)
        {
            Compilation = compilation;
            Kind = kind;
            MauiPropertyName = mauiPropertyName;
            MauiContainingTypeName = mauiContainingTypeName;
            ComponentName = componentName;
            _componentTypeLazy = new Lazy<string>(() => componentType);
            _componentPropertyNameLazy = new Lazy<string>(() => componentPropertyName);
            _usings = usings;
        }

        private GeneratedPropertyInfo(Compilation compilation, IPropertySymbol propertyInfo, GeneratedPropertyKind kind, IList<UsingStatement> usings)
        {
            _propertyInfo = propertyInfo;
            _usings = usings;
            Kind = kind;

            Compilation = compilation;
            MauiPropertyName = propertyInfo.Name;
            MauiContainingTypeName = GetTypeNameAndAddNamespace(propertyInfo.ContainingType);

            ComponentName = ComponentWrapperGenerator.GetIdentifierName(_propertyInfo.ContainingType.Name);
            _componentPropertyNameLazy = new Lazy<string>(GetComponentPropertyName);
            _componentTypeLazy = new Lazy<string>(GetComponentType);

            string GetComponentPropertyName()
            {
                if (Aliases.TryGetValue($"{_propertyInfo.ContainingType.Name}.{MauiPropertyName}", out var aliasName))
                    return aliasName;

                if (IsRenderFragmentProperty && _propertyInfo.ContainingType.GetAttributes().Any(a
                    => a.AttributeClass.Name == "ContentPropertyAttribute" && Equals(a.ConstructorArguments.FirstOrDefault().Value, _propertyInfo.Name)))
                {
                    return "ChildContent";
                }

                return ComponentWrapperGenerator.GetIdentifierName(_propertyInfo.Name);
            }

            string GetComponentType()
            {
                var elementType = (INamedTypeSymbol)_propertyInfo?.Type;
                if (elementType.IsGenericType
                    && elementType.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IList_T
                    && elementType.TypeArguments[0].SpecialType == SpecialType.System_String)
                {
                    // Lists of strings are special-cased because they are handled specially by the handlers as a comma-separated list
                    return "string";
                }
                else if (IsRenderFragmentProperty)
                {
                    return "RenderFragment";
                }
                else if (elementType.IsValueType && !elementType.IsNullableStruct())
                {
                    return GetTypeNameAndAddNamespace(elementType) + "?";
                }
                else
                {
                    return GetTypeNameAndAddNamespace(elementType);
                }
            }
        }

        public string GetPropertyDeclaration()
        {
            // razor compiler doesn't allow 'new' properties, it condiders them as duplicates.
            if (_propertyInfo is not null && _propertyInfo.IsHidingMember())
            {
                return "";
            }

            const string indent = "        ";

            var xmlDocContents = _propertyInfo is null ? "" : ComponentWrapperGenerator.GetXmlDocContents(_propertyInfo, indent);

            return $@"{xmlDocContents}{indent}[Parameter] public {ComponentType} {ComponentPropertyName} {{ get; set; }}
";
        }

        public string GetHandleValueProperty()
        {
            var propName = ComponentPropertyName;

            return $@"                case nameof({propName}):
                    if (!Equals({propName}, value))
                    {{
                        {propName} = ({ComponentType})value;
                        NativeControl.{propName} = {GetConvertedProperty(_propertyInfo.Type, propName)};
                    }}
                    break;
";

            string GetConvertedProperty(ITypeSymbol propertyType, string propName)
            {
                if (propertyType is INamedTypeSymbol namedType)
                {
                    if (namedType.IsGenericType
                        && namedType.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IList_T
                        && namedType.TypeArguments[0].SpecialType == SpecialType.System_String)
                    {
                        return $"AttributeHelper.GetStringList({propName})";
                    }

                    if (namedType.IsValueType && !namedType.IsNullableStruct())
                    {
                        var hasBindingProperty = !_propertyInfo.ContainingType.GetMembers($"{propName}Property").IsEmpty;
                        var defaultValue = hasBindingProperty
                            ? $"({GetTypeNameAndAddNamespace(propertyType)}){MauiContainingTypeName}.{propName}Property.DefaultValue"
                            : "default";

                        return $"{propName} ?? {defaultValue}";
                    }
                }

                return propName;
            }
        }

        internal static GeneratedPropertyInfo[] GetValueProperties(Compilation compilation, GeneratedComponentInfo componentInfo, IList<UsingStatement> usings)
        {
            var props = componentInfo.TypeSymbol.GetMembers().OfType<IPropertySymbol>()
                .Where(p => !componentInfo.Exclude.Contains(p.Name))
                .Where(IsPublicProperty)
                .Where(HasPublicSetter)
                .Where(prop => !DisallowedComponentTypes.Contains(prop.Type.GetFullName()))
                .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase);

            return props.Select(prop => new GeneratedPropertyInfo(compilation, prop, GeneratedPropertyKind.Value, usings)).ToArray();
        }

        private static bool IsPublicProperty(IPropertySymbol propertyInfo)
        {
            return propertyInfo.GetMethod?.DeclaredAccessibility == Accessibility.Public && IsBrowsable(propertyInfo) && !propertyInfo.IsIndexer;
        }
        private static bool IsBrowsable(ISymbol propInfo)
        {
            // [EditorBrowsable(EditorBrowsableState.Never)]
            return !propInfo.GetAttributes().Any(a => a.AttributeClass.Name == nameof(EditorBrowsableAttribute)
                && a.ConstructorArguments.FirstOrDefault().Value?.Equals((int)EditorBrowsableState.Never) == true);
        }

        private static bool HasPublicSetter(IPropertySymbol propertyInfo)
        {
            return propertyInfo.SetMethod?.DeclaredAccessibility == Accessibility.Public;
        }

        private static readonly List<string> DisallowedComponentTypes = new()
        {
            "Microsoft.Maui.Controls.Brush",
            "Microsoft.Maui.Controls.Button.ButtonContentLayout", // TODO: This is temporary; should be possible to add support later
            "Microsoft.Maui.Controls.ColumnDefinitionCollection",

            "Microsoft.Maui.Controls.PointCollection",
            "Microsoft.Maui.Controls.DoubleCollection",
            "Microsoft.Maui.Controls.ControlTemplate",
            "Microsoft.Maui.Controls.DataTemplate",
            "Microsoft.Maui.Controls.Element",
            "Microsoft.Maui.Font", // TODO: This is temporary; should be possible to add support later
            "Microsoft.Maui.Graphics.Font", // TODO: This is temporary; should be possible to add support later
            "Microsoft.Maui.Controls.FormattedString",
            "Microsoft.Maui.Controls.Shapes.Geometry",
            "Microsoft.Maui.Controls.GradientStopCollection",
            "System.Windows.Input.ICommand",
            "System.Object",
            "Microsoft.Maui.Controls.Page",
            "Microsoft.Maui.Controls.ResourceDictionary",
            "Microsoft.Maui.Controls.RowDefinitionCollection",
            "Microsoft.Maui.Controls.Shadow",
            "Microsoft.Maui.Controls.ShellContent",
            "Microsoft.Maui.Controls.ShellItem",
            "Microsoft.Maui.Controls.ShellSection",
            "Microsoft.Maui.Controls.Style", // TODO: This is temporary; should be possible to add support later
            "Microsoft.Maui.Controls.IVisual",
            "Microsoft.Maui.Controls.View",
            "Microsoft.Maui.IView",
            "Microsoft.Maui.IViewHandler"
        };

        private string GetTypeNameAndAddNamespace(ITypeSymbol type)
        {
            return ComponentWrapperGenerator.GetTypeNameAndAddNamespace(type, _usings);
        }

        private string GetTypeNameAndAddNamespace(string @namespace, string typeName)
        {
            return ComponentWrapperGenerator.GetTypeNameAndAddNamespace(@namespace, typeName, _usings);
        }
    }
}
