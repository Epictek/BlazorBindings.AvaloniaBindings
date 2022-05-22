// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class AbsoluteLayoutHandler : LayoutHandler
    {
        public AbsoluteLayoutHandler(NativeComponentRenderer renderer, XF.AbsoluteLayout layoutControl) : base(renderer, layoutControl)
        {
        }
    }
}
