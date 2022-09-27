using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComponentWrapperGenerator
{
    public partial class GeneratedPropertyInfo
    {
        private static readonly string[] ContentTypes = new[]
        {
            "Microsoft.Maui.IView",
            "Microsoft.Maui.Controls.BaseMenuItem",
            "Microsoft.Maui.Controls.ControlTemplate"
        };

        public bool IsRenderFragmentProperty => Kind == GeneratedPropertyKind.RenderFragment;
        public bool IsControlTemplate => _propertyInfo.Type.ToDisplayString() == "Microsoft.Maui.Controls.ControlTemplate";

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
            //    (renderer, parent, component) => new ContentPropertyHandler<MC.ContentPage>((page, value) => page.Content = (MC.View)value));

            var contentHandler = GetContentHandler();

            return @$"
            ElementHandlerRegistry.RegisterPropertyContentHandler<{ComponentName}>(nameof({ComponentPropertyName}),
                (renderer, parent, component) => {contentHandler});";
        }

        private string GetContentHandler()
        {
            var type = (INamedTypeSymbol)_propertyInfo.Type;

            if (IsControlTemplate)
            {
                // new ControlTemplatePropertyHandler<MC.TemplatedView>(component, (view, controlTemplate) => view.ControlTemplate = controlTemplate)
                var controlTemplateHandlerName = GetTypeNameAndAddNamespace("BlazorBindings.Maui.Elements.Handlers", "ControlTemplatePropertyHandler");
                return $"new {controlTemplateHandlerName}<{MauiContainingTypeName}>(component,\r\n                    (x, controlTemplate) => x.{_propertyInfo.Name} = controlTemplate)";
            }
            else if (type.IsGenericType && type.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IList_T)
            {
                // new ListContentPropertyHandler<MC.Page, MC.ToolbarItem>(page => page.ToolbarItems)
                var itemTypeName = GetTypeNameAndAddNamespace(type.TypeArguments[0]);
                var listContentHandlerTypeName = GetTypeNameAndAddNamespace("BlazorBindings.Maui.Elements.Handlers", "ListContentPropertyHandler");
                return $"new {listContentHandlerTypeName}<{MauiContainingTypeName}, {itemTypeName}>(x => x.{_propertyInfo.Name})";
            }
            else
            {
                // new ContentPropertyHandler<MC.ContentPage>((page, value) => page.Content = (MC.View)value));
                var propTypeName = GetTypeNameAndAddNamespace(type);
                var contentHandlerTypeName = GetTypeNameAndAddNamespace("BlazorBindings.Maui.Elements.Handlers", "ContentPropertyHandler");
                return $"new {contentHandlerTypeName}<{MauiContainingTypeName}>((x, value) => x.{_propertyInfo.Name} = ({propTypeName})value)";
            }
        }

        public string RenderContentProperty()
        {
            if (IsControlTemplate)
            {
                // RenderTreeBuilderHelper.AddControlTemplateProperty(builder, sequence++, typeof(TemplatedView), ControlTemplate);
                return $"\r\n            RenderTreeBuilderHelper.AddControlTemplateProperty(builder, sequence++, typeof({ComponentName}), {ComponentPropertyName});";
            }
            else
            {
                // RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ContentPage), ChildContent);
                return $"\r\n            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof({ComponentName}), {ComponentPropertyName});";
            }
        }

        internal static GeneratedPropertyInfo[] GetContentProperties(Compilation compilation, GeneratedComponentInfo componentInfo, IList<UsingStatement> usings)
        {
            var propInfos = componentInfo.TypeSymbol.GetMembers().OfType<IPropertySymbol>()
                .Where(e => !componentInfo.Exclude.Contains(e.Name))
                .Where(IsPublicProperty)
                .Where(prop => IsRenderFragmentPropertySymbol(compilation, prop))
                .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase);

            return propInfos.Select(prop => new GeneratedPropertyInfo(compilation, prop, GeneratedPropertyKind.RenderFragment, usings)).ToArray();
        }

        private static bool IsRenderFragmentPropertySymbol(Compilation compilation, IPropertySymbol prop)
        {
            var type = prop.Type;
            if (IsContent(type) && HasPublicSetter(prop))
                return true;

            if (type is INamedTypeSymbol namedType
                && namedType.IsGenericType
                && namedType.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IList_T
                && IsContent(namedType.TypeArguments[0]))
            {
                return true;
            }

            return false;

            bool IsContent(ITypeSymbol type) => ContentTypes.Any(t =>
            {
                var contentTypeSymbol = compilation.GetTypeByMetadataName(t);
                return compilation.ClassifyConversion(type, contentTypeSymbol) is { IsIdentity: true } or { IsReference: true, IsImplicit: true };
            });
        }
    }
}
