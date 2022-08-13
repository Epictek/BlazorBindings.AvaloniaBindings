// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class Shell : Page
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.NavBarIsVisible",
                (element, value) => MC.Shell.SetNavBarIsVisible(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.NavBarHasShadow",
                (element, value) => MC.Shell.SetNavBarHasShadow(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarIsVisible",
                (element, value) => MC.Shell.SetTabBarIsVisible(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.BackgroundColor",
                (element, value) => MC.Shell.SetBackgroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.DisabledColor",
                (element, value) => MC.Shell.SetDisabledColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.ForegroundColor",
                (element, value) => MC.Shell.SetForegroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarBackgroundColor",
                (element, value) => MC.Shell.SetTabBarBackgroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarDisabledColor",
                (element, value) => MC.Shell.SetTabBarDisabledColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarForegroundColor",
                (element, value) => MC.Shell.SetTabBarForegroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarTitleColor",
                (element, value) => MC.Shell.SetTabBarTitleColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarUnselectedColor",
                (element, value) => MC.Shell.SetTabBarUnselectedColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TitleColor",
                (element, value) => MC.Shell.SetTitleColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.UnselectedColor",
                (element, value) => MC.Shell.SetUnselectedColor(element, AttributeHelper.StringToColor(value)));

            ElementHandlerRegistry.RegisterPropertyContentHandler<Shell>(nameof(FlyoutHeader),
                renderer => new ContentPropertyHandler<MC.Shell>(
                    (shell, valueElement) => shell.FlyoutHeader = valueElement));

            ElementHandlerRegistry.RegisterPropertyContentHandler<Shell>(nameof(ItemTemplate),
                (renderer, _, component) => new DataTemplatePropertyHandler<MC.Shell, MC.BaseShellItem>(component,
                    (shell, dataTemplate) => MC.Shell.SetItemTemplate(shell, dataTemplate)));

            ElementHandlerRegistry.RegisterPropertyContentHandler<Shell>(nameof(MenuItemTemplate),
                (renderer, _, component) => new DataTemplatePropertyHandler<MC.Shell, MC.BaseShellItem>(component,
                    (shell, dataTemplate) => MC.Shell.SetMenuItemTemplate(shell, dataTemplate)));
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment FlyoutHeader { get; set; }
        [Parameter] public RenderFragment<MC.BaseShellItem> ItemTemplate { get; set; }
        [Parameter] public RenderFragment<MC.BaseShellItem> MenuItemTemplate { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Shell), FlyoutHeader);
            RenderTreeBuilderHelper.AddDataTemplateProperty(builder, sequence++, typeof(Shell), ItemTemplate);
            RenderTreeBuilderHelper.AddDataTemplateProperty(builder, sequence++, typeof(Shell), MenuItemTemplate);
        }
    }
}
