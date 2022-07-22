// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.Shapes.Handlers;
using MCS = Microsoft.Maui.Controls.Shapes;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public partial class Rectangle : Shape
    {
        static Rectangle()
        {
            ElementHandlerRegistry.RegisterElementHandler<Rectangle>(
                renderer => new RectangleHandler(renderer, new MCS.Rectangle()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? RadiusX { get; set; }
        [Parameter] public double? RadiusY { get; set; }

        public new MCS.Rectangle NativeControl => (ElementHandler as RectangleHandler)?.RectangleControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (RadiusX != null)
            {
                builder.AddAttribute(nameof(RadiusX), AttributeHelper.DoubleToString(RadiusX.Value));
            }
            if (RadiusY != null)
            {
                builder.AddAttribute(nameof(RadiusY), AttributeHelper.DoubleToString(RadiusY.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
