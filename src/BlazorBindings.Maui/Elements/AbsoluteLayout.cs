// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Layouts;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class AbsoluteLayout : Layout
    {
        static AbsoluteLayout()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("AbsoluteLayout.LayoutBounds",
                (element, value) => MC.AbsoluteLayout.SetLayoutBounds(element, AttributeHelper.GetBoundsRect(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("AbsoluteLayout.LayoutFlags",
                (element, value) => MC.AbsoluteLayout.SetLayoutFlags(element, AttributeHelper.GetEnum<AbsoluteLayoutFlags>(value)));
        }

        protected override MC.Element CreateNativeElement() => new MC.AbsoluteLayout();
    }
}
