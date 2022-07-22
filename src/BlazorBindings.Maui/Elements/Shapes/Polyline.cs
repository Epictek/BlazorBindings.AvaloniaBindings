// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public partial class Polyline : Shape
    {
        [Parameter] public string Points { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (Points != null)
            {
                builder.AddAttribute(nameof(Points), Points);
            }
        }
    }
}
