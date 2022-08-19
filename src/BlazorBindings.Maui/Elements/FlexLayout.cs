// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Layouts;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlexLayout
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.AlignSelf",
                (element, value) => MC.FlexLayout.SetAlignSelf(element, (FlexAlignSelf)AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Grow",
                (element, value) => MC.FlexLayout.SetGrow(element, AttributeHelper.GetSingle(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Shrink",
                (element, value) => MC.FlexLayout.SetShrink(element, AttributeHelper.GetSingle(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Order",
                (element, value) => MC.FlexLayout.SetOrder(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("FlexLayout.Basis",
                (element, value) => MC.FlexLayout.SetBasis(element, AttributeHelper.StringToFlexBasis(value)));
        }
    }
}
