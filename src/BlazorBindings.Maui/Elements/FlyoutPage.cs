// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlyoutPage : Page
    {
        [Parameter] public RenderFragment Flyout { get; set; }
        [Parameter] public RenderFragment Detail { get; set; }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(FlyoutPage), nameof(Flyout), Flyout);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(FlyoutPage), nameof(Detail), Detail);
        }

        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<FlyoutPage>(nameof(Flyout),
                _ => new ContentPropertyHandler<MC.FlyoutPage>((page, value) => page.Flyout = (MC.Page)value));

            ElementHandlerRegistry.RegisterPropertyContentHandler<FlyoutPage>(nameof(Detail),
                _ => new ContentPropertyHandler<MC.FlyoutPage>((page, value) =>
                {
                    if (value is not MC.NavigationPage navigationPage)
                    {
                        navigationPage = new MC.NavigationPage((MC.Page)value);
                    }
                    page.Detail = navigationPage;
                }));
        }
    }
}
