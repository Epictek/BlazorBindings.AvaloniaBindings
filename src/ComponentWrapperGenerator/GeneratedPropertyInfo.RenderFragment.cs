using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComponentWrapperGenerator
{
    public partial class GeneratedPropertyInfo
    {
        private static readonly Type[] ContentTypes = new[]
        {
            typeof(IView),
            typeof(BaseMenuItem)
        };

        public bool IsRenderFragmentProperty => Kind == GeneratedPropertyKind.RenderFragment;

        public string GetHandleContentProperty()
        {
            return $@"                case nameof({ComponentPropertyName}):
                    {ComponentPropertyName} = (RenderFragment)value;
                    break;
";
        }

        public string GetContentHandlerRegistration()
        {
            // ElementHandlerRegistry.RegisterPropertyContentHandler<ContentPage>(nameof(ChildContent),
            //    _ => new ContentPropertyHandler<MC.ContentPage>((page, value) => page.Content = (MC.View)value));

            var contentHandler = GetContentHandler();

            return @$"
            ElementHandlerRegistry.RegisterPropertyContentHandler<{ComponentName}>(nameof({ComponentPropertyName}),
                _ => {contentHandler});";
        }

        private string GetContentHandler()
        {
            var type = _propertyInfo.PropertyType;

            if (type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableTo(typeof(IList<>)))
            {
                // new ListContentPropertyHandler<MC.Page, MC.ToolbarItem>(page => page.ToolbarItems)
                var itemTypeName = GetTypeNameAndAddNamespace(type.GenericTypeArguments[0]);
                var listContentHandlerTypeName = GetTypeNameAndAddNamespace("BlazorBindings.Maui.Elements.Handlers", "ListContentPropertyHandler");
                return $"new {listContentHandlerTypeName}<{MauiDeclaringTypeName}, {itemTypeName}>(x => x.{_propertyInfo.Name})";
            }
            else
            {
                // new ContentPropertyHandler<MC.ContentPage>((page, value) => page.Content = (MC.View)value));
                var propTypeName = GetTypeNameAndAddNamespace(type);
                var contentHandlerTypeName = GetTypeNameAndAddNamespace("BlazorBindings.Maui.Elements.Handlers", "ContentPropertyHandler");
                return $"new {contentHandlerTypeName}<{MauiDeclaringTypeName}>((x, value) => x.{_propertyInfo.Name} = ({propTypeName})value)";
            }
        }

        public string RenderContentProperty()
        {
            // RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ContentPage), ChildContent);
            return $"\r\n            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof({ComponentName}), {ComponentPropertyName});";
        }

        internal static GeneratedPropertyInfo[] GetContentProperties(Type componentType, IList<UsingStatement> usings)
        {
            var propInfos = componentType.GetProperties()
                    .Where(IsPublicProperty)
                    .Where(prop => prop.DeclaringType == componentType)
                    .Where(prop => IsRenderFragmentPropertyInfo(prop))
                    .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase);

            return propInfos.Select(prop => new GeneratedPropertyInfo(prop, GeneratedPropertyKind.RenderFragment, usings)).ToArray();
        }

        private static bool IsRenderFragmentPropertyInfo(PropertyInfo prop)
        {
            var type = prop.PropertyType;
            if (IsContent(type) && HasPublicSetter(prop))
                return true;

            if (type.IsGenericType
                && type.GetGenericTypeDefinition().IsAssignableTo(typeof(IList<>))
                && IsContent(type.GenericTypeArguments[0]))
                return true;

            return false;

            static bool IsContent(Type type) => ContentTypes.Any(t => type.IsAssignableTo(t));
        }
    }
}
