// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public partial class RadioButton : TemplatedView
    {
        [Parameter] public string Text { get; set; }
        [Parameter] public object Value { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (Text != null || Value != null)
            {
                builder.AddAttribute(nameof(Text), Text ?? Value.ToString());
            }
            if (Value != null)
            {
                builder.AddAttribute(nameof(Value), AttributeHelper.ObjectToDelegate(Value));
            }
        }
    }
}
