using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class ContentPage : TemplatedPage
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<ContentPage>(nameof(TitleView),
                (renderer, parent, component) => new ContentPropertyHandler<MC.Page>((x, value) =>
                {
                    MC.Shell.SetTitleView(x, (MC.View)value);
                    MC.NavigationPage.SetTitleView(x, (MC.View)value);
                }));

            ElementHandlerRegistry.RegisterPropertyContentHandler<ContentPage>(nameof(SearchHandler),
                (renderer, parent, component) => new ContentPropertyHandler<MC.Page>((x, value) =>
                    MC.Shell.SetSearchHandler(x, (MC.SearchHandler)value)));

            ElementHandlerRegistry.RegisterPropertyContentHandler<ContentPage>(nameof(BackButtonBehavior),
                (renderer, parent, component) => new ContentPropertyHandler<MC.Page>((x, value) =>
                    MC.Shell.SetBackButtonBehavior(x, (MC.BackButtonBehavior)value)));
        }

        /// <summary>
        /// Sets a value that indicates whether or not this Page element has a navigation bar.
        /// </summary>
        [Parameter] public bool? NavBarIsVisible { get; set; }

        /// <summary>
        /// Sets a value that indicates whether or not this Page element navigation bar has a shadow.
        /// This property is ignored if the application does not use Shell.
        /// </summary>
        [Parameter] public bool? NavBarHasShadow { get; set; }

        /// <summary>
        /// Sets a value that indicates whether or not this Page element has tab bar visible.
        /// This property is ignored if the application does not use Shell.
        /// </summary>
        [Parameter] public bool? TabBarIsVisible { get; set; }

        /// <summary>
        /// Change Shell navigation behavior.
        /// This property is ignored if the application does not use Shell.
        /// </summary>
        [Parameter] public PresentationMode? PresentationMode { get; set; }


        /// <summary>
        /// Defines the color used for the title of the page.
        /// </summary>
        [Parameter] public Color TitleColor { get; set; }

        /// <summary>
        /// Defines the view that can be displayed in the navigation bar.
        /// </summary>
        [Parameter] public RenderFragment TitleView { get; set; }

        /// <summary>
        /// Configures Shell integrated search capabilities. It accepts <see cref="Elements.SearchHandler{T}"/> child only.
        /// </summary>
        [Parameter] public RenderFragment SearchHandler { get; set; }

        /// <summary>
        /// Defines the behavior of the back button. This property is ignored if the application does not use Shell.
        /// It accepts <see cref="Elements.BackButtonBehavior"/> child only.
        /// </summary>
        [Parameter] public RenderFragment BackButtonBehavior { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(NavBarIsVisible):
                    if (!Equals(NavBarIsVisible, value))
                    {
                        NavBarIsVisible = (bool?)value;
                        MC.Shell.SetNavBarIsVisible(NativeControl, NavBarIsVisible ?? true);
                        MC.NavigationPage.SetHasNavigationBar(NativeControl, NavBarIsVisible ?? true);
                    }
                    return true;
                case nameof(NavBarHasShadow):
                    if (!Equals(NavBarHasShadow, value))
                    {
                        NavBarHasShadow = (bool?)value;
                        MC.Shell.SetNavBarHasShadow(NativeControl, NavBarHasShadow ?? true);
                    }
                    return true;
                case nameof(TabBarIsVisible):
                    if (!Equals(TabBarIsVisible, value))
                    {
                        TabBarIsVisible = (bool?)value;
                        MC.Shell.SetTabBarIsVisible(NativeControl, TabBarIsVisible ?? true);
                    }
                    return true;
                case nameof(PresentationMode):
                    if (!Equals(PresentationMode, value))
                    {
                        PresentationMode = (PresentationMode?)value;
                        MC.Shell.SetPresentationMode(NativeControl, PresentationMode ?? (PresentationMode)MC.Shell.PresentationModeProperty.DefaultValue);
                    }
                    return true;
                case nameof(TitleColor):
                    if (!Equals(TitleColor, value))
                    {
                        TitleColor = (Color)value;
                        MC.Shell.SetTitleColor(NativeControl, TitleColor);
                    }
                    return true;
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
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ContentPage), TitleView);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ContentPage), SearchHandler);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ContentPage), BackButtonBehavior);
        }
    }
}
