using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class Page
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.BackButtonTitle",
                (element, value) => MC.NavigationPage.SetBackButtonTitle(element, value?.ToString()));
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.HasBackButton",
                (element, value) => MC.NavigationPage.SetHasBackButton((MC.Page)element, AttributeHelper.GetBool(value)));
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.HasNavigationBar",
                (element, value) => MC.NavigationPage.SetHasNavigationBar(element, AttributeHelper.GetBool(value)));
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.IconColor",
                (element, value) => MC.NavigationPage.SetIconColor(element, AttributeHelper.GetColor(value)));

            ElementHandlerRegistry.RegisterPropertyContentHandler<Page>(nameof(TitleView),
                (renderer, parent, component) => new ContentPropertyHandler<MC.Page>((x, value) =>
                {
                    MC.Shell.SetTitleView(x, (MC.View)value);
                    MC.NavigationPage.SetTitleView(x, (MC.View)value);
                }));

            ElementHandlerRegistry.RegisterPropertyContentHandler<Page>(nameof(SearchHandler),
                (renderer, parent, component) => new ContentPropertyHandler<MC.Page>((x, value) =>
                    MC.Shell.SetSearchHandler(x, (MC.SearchHandler)value)));

            ElementHandlerRegistry.RegisterPropertyContentHandler<Page>(nameof(BackButtonBehavior),
                (renderer, parent, component) => new ContentPropertyHandler<MC.Page>((x, value) =>
                    MC.Shell.SetBackButtonBehavior(x, (MC.BackButtonBehavior)value)));
        }

        /// <summary>
        /// Defines the view that can be displayed in the navigation bar.
        /// </summary>
        [Parameter] public RenderFragment TitleView { get; set; }

        /// <summary>
        /// Configures Shell integrated search capabilities.
        /// </summary>
        [Parameter] public RenderFragment SearchHandler { get; set; }

        /// <summary>
        /// Defines the behavior of the back button. This property is ignored if the application does not use Shell.
        /// </summary>
        [Parameter] public RenderFragment BackButtonBehavior { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(TitleView):
                    TitleView = (RenderFragment)value;
                    return true;
                case nameof(SearchHandler):
                    SearchHandler = (RenderFragment)value;
                    return true;
                case nameof(BackButtonBehavior):
                    BackButtonBehavior = (RenderFragment)value;
                    return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }

        protected override void RenderAdditionalPartialElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalPartialElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Page), TitleView);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Page), SearchHandler);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Page), BackButtonBehavior);
        }
    }
}
