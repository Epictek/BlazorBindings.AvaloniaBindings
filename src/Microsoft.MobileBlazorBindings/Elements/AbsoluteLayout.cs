// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class AbsoluteLayout : Layout
    {
        static AbsoluteLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<AbsoluteLayout>(
                renderer => new AbsoluteLayoutHandler(renderer, new XF.AbsoluteLayout()));

            RegisterAdditionalHandlers();
        }

        static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("AbsoluteLayout.LayoutBounds",
                (element, value) => XF.AbsoluteLayout.SetLayoutBounds(element, AttributeHelper.StringToBoundsRect(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("AbsoluteLayout.LayoutFlags",
                (element, value) => XF.AbsoluteLayout.SetLayoutFlags(element, AttributeHelper.GetEnum<XF.AbsoluteLayoutFlags>(value)));
        }
    }
}
