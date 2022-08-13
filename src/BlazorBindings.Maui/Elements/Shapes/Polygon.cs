// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public partial class Polygon : Shape
    {
        [Parameter] public string Points { set { } }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Points):
                    NativeControl.Points = AttributeHelper.StringToPointCollection(value);
                    return true;

                default:
                    return base.HandleAdditionalParameter(name, value);
            }
        }
    }
}
