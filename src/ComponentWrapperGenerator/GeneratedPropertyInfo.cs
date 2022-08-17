using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace ComponentWrapperGenerator
{
    public partial class GeneratedPropertyInfo
    {
        private static readonly Dictionary<string, string> Aliases = new()
        {
            ["FlyoutPage.Detail"] = "Detail"
        };

        private readonly PropertyInfo _propertyInfo;
        private readonly IList<UsingStatement> _usings;
        private readonly Lazy<string> _componentPropertyNameLazy;
        private readonly Lazy<string> _componentPropertyTypeLazy;

        public GeneratedPropertyKind Kind { get; }
        public string MauiPropertyName { get; }
        public string MauiDeclaringTypeName { get; }
        public string ComponentName { get; }
        public string ComponentPropertyName => _componentPropertyNameLazy.Value;
        public string ComponentPropertyType => _componentPropertyTypeLazy.Value;

        private GeneratedPropertyInfo(string mauiPropertyName,
                                      string mauiDeclaringTypeName,
                                      string componentName,
                                      string componentPropertyName,
                                      string componentPropertyType,
                                      GeneratedPropertyKind kind,
                                      IList<UsingStatement> usings)
        {
            Kind = kind;
            MauiPropertyName = mauiPropertyName;
            MauiDeclaringTypeName = mauiDeclaringTypeName;
            ComponentName = componentName;
            _componentPropertyTypeLazy = new Lazy<string>(componentPropertyType);
            _componentPropertyNameLazy = new Lazy<string>(componentPropertyName);
            _usings = usings;
        }

        private GeneratedPropertyInfo(PropertyInfo propertyInfo, GeneratedPropertyKind kind, IList<UsingStatement> usings)
        {
            _propertyInfo = propertyInfo;
            _usings = usings;
            Kind = kind;

            MauiPropertyName = propertyInfo.Name;
            MauiDeclaringTypeName = GetTypeNameAndAddNamespace(propertyInfo.DeclaringType);

            ComponentName = ComponentWrapperGenerator.GetIdentifierName(_propertyInfo.DeclaringType.Name);
            _componentPropertyNameLazy = new Lazy<string>(GetComponentPropertyName);
            _componentPropertyTypeLazy = new Lazy<string>(GetComponentPropertyType);

            string GetComponentPropertyName()
            {
                if (Aliases.TryGetValue($"{_propertyInfo.DeclaringType.Name}.{MauiPropertyName}", out var aliasName))
                    return aliasName;

                if (IsRenderFragmentProperty && _propertyInfo.DeclaringType.GetCustomAttribute<ContentPropertyAttribute>()?.Name == _propertyInfo.Name)
                    return "ChildContent";

                return ComponentWrapperGenerator.GetIdentifierName(_propertyInfo.Name);
            }

            string GetComponentPropertyType()
            {
                var elementPropertyType = _propertyInfo?.PropertyType;
                if (elementPropertyType == typeof(IList<string>))
                {
                    // Lists of strings are special-cased because they are handled specially by the handlers as a comma-separated list
                    return "string";
                }
                else if (IsRenderFragmentProperty)
                {
                    return "RenderFragment";
                }
                else
                {
                    return GetTypeNameAndAddNamespace(elementPropertyType);
                }
            }
        }

        public string GetPropertyDeclaration()
        {
            const string indent = "        ";

            //var xmlDocContents = _propertyInfo is null ? "" : ComponentWrapperGenerator.GetXmlDocContents(_propertyInfo, indent);
            var xmlDocContents = "";

            return $@"{xmlDocContents}{indent}[Parameter] public {ComponentPropertyType} {ComponentPropertyName} {{ get; set; }}
";
        }

        public string GetHandleValueProperty()
        {
            var propName = ComponentPropertyName;

            return $@"                case nameof({propName}):
                    if (!Equals({propName}, value))
                    {{
                        {propName} = ({ComponentPropertyType})value;
                        NativeControl.{propName} = {GetConvertedProperty(_propertyInfo.PropertyType, propName)};
                    }}
                    break;
";

            static string GetConvertedProperty(Type propertyType, string propName)
            {
                if (propertyType == typeof(IList<string>))
                    return $"AttributeHelper.GetStringList({propName})";

                return propName;
            }
        }

        internal static GeneratedPropertyInfo[] GetValueProperties(Type componentType, IList<UsingStatement> usings)
        {
            var props = componentType.GetProperties()
                    .Where(IsPublicProperty)
                    .Where(HasPublicSetter)
                    .Where(prop => prop.DeclaringType == componentType)
                    .Where(prop => !DisallowedComponentPropertyTypes.Contains(prop.PropertyType))
                    .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase);

            return props.Select(prop => new GeneratedPropertyInfo(prop, GeneratedPropertyKind.Value, usings)).ToArray();
        }

        private static bool IsPublicProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetGetMethod() != null && IsPropertyBrowsable(propertyInfo) && !IsIndexProperty(propertyInfo);

            static bool IsIndexProperty(PropertyInfo propInfo)
                => propInfo.GetIndexParameters().Length > 0;

            static bool IsPropertyBrowsable(PropertyInfo propInfo)
            {
                // [EditorBrowsable(EditorBrowsableState.Never)]
                var attr = (EditorBrowsableAttribute)Attribute.GetCustomAttribute(propInfo, typeof(EditorBrowsableAttribute));
                return (attr == null) || (attr.State != EditorBrowsableState.Never);
            }
        }

        private static bool HasPublicSetter(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetSetMethod() != null;
        }

        private static readonly List<Type> DisallowedComponentPropertyTypes = new List<Type>
        {
            typeof(Brush),
            typeof(Button.ButtonContentLayout), // TODO: This is temporary; should be possible to add support later
            typeof(ColumnDefinitionCollection),
            typeof(PointCollection),
            typeof(DoubleCollection),
            typeof(ControlTemplate),
            typeof(DataTemplate),
            typeof(Element),
            // typeof(Maui.Font), // TODO: This is temporary; should be possible to add support later
            typeof(FormattedString),
            typeof(Microsoft.Maui.Controls.Shapes.Geometry),
            typeof(GradientStopCollection),
            typeof(ICommand),
            typeof(object),
            typeof(Page),
            typeof(ResourceDictionary),
            typeof(RowDefinitionCollection),
            typeof(Shadow),
            typeof(ShellContent),
            typeof(ShellItem),
            typeof(ShellSection),
            typeof(Style), // TODO: This is temporary; should be possible to add support later
            typeof(IVisual),
            typeof(View),
            typeof(IView),
            typeof(IViewHandler)
        };

        private string GetTypeNameAndAddNamespace(Type type)
        {
            return ComponentWrapperGenerator.GetTypeNameAndAddNamespace(type, _usings);
        }

        private string GetTypeNameAndAddNamespace(string @namespace, string typeName)
        {
            return ComponentWrapperGenerator.GetTypeNameAndAddNamespace(@namespace, typeName, _usings);
        }
    }
}
