// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlyoutPage : Page
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<FlyoutPage>(nameof(Detail),
                _ => new ContentPropertyHandler<MC.FlyoutPage>((page, value) =>
                {
                    // We cannot set Detail to null. An actual page will probably be set on next invocation anyway.
                    if (value == null)
                        return;

                    if (value is not MC.NavigationPage navigationPage)
                    {
                        navigationPage = new MC.NavigationPage((MC.Page)value);
                    }
                    page.Detail = navigationPage;
                }));
        }
    }
}
