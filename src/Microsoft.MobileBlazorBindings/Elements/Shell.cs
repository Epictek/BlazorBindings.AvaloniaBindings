// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Shell : Page
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.NavBarIsVisible",
                (element, value) => XF.Shell.SetNavBarIsVisible(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.NavBarHasShadow",
                (element, value) => XF.Shell.SetNavBarHasShadow(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarIsVisible",
                (element, value) => XF.Shell.SetTabBarIsVisible(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.BackgroundColor",
                (element, value) => XF.Shell.SetBackgroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.DisabledColor",
                (element, value) => XF.Shell.SetDisabledColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.ForegroundColor",
                (element, value) => XF.Shell.SetForegroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarBackgroundColor",
                (element, value) => XF.Shell.SetTabBarBackgroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarDisabledColor",
                (element, value) => XF.Shell.SetTabBarDisabledColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarForegroundColor",
                (element, value) => XF.Shell.SetTabBarForegroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarTitleColor",
                (element, value) => XF.Shell.SetTabBarTitleColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarUnselectedColor",
                (element, value) => XF.Shell.SetTabBarUnselectedColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TitleColor",
                (element, value) => XF.Shell.SetTitleColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.UnselectedColor",
                (element, value) => XF.Shell.SetUnselectedColor(element, AttributeHelper.StringToColor(value)));
        }

        [Parameter] public RenderFragment FlyoutHeader { get; set; }

        [Parameter] public EventCallback<XF.ShellNavigatedEventArgs> OnNavigated { get; set; }
        [Parameter] public EventCallback<XF.ShellNavigatingEventArgs> OnNavigating { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onnavigated", OnNavigated);
            builder.AddAttribute("onnavigating", OnNavigating);
        }

        public async Task GoTo(XF.ShellNavigationState state, bool animate = true)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            await NativeControl.GoToAsync(state, animate).ConfigureAwait(true);
        }

        protected override RenderFragment GetChildContent() => RenderChildContent;

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            if (FlyoutHeader != null)
            {
                builder.OpenComponent<ShellFlyoutHeader>(1);
                builder.AddAttribute(0, nameof(ChildContent), FlyoutHeader);
                builder.CloseComponent();
            }

            builder.AddContent(2, ChildContent);
        }
    }
}
