// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class FlexLayout : Layout
    {
#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.AlignSelf",
                (element, value) => XF.FlexLayout.SetAlignSelf(element, (XF.FlexAlignSelf)AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Grow",
                (element, value) => XF.FlexLayout.SetGrow(element, AttributeHelper.StringToSingle((string)value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Shrink",
                (element, value) => XF.FlexLayout.SetShrink(element, AttributeHelper.StringToSingle((string)value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Order",
                (element, value) => XF.FlexLayout.SetOrder(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Basis",
                (element, value) => XF.FlexLayout.SetBasis(element, AttributeHelper.StringToFlexBasis(value)));
        }

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
