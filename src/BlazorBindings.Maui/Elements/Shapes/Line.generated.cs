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
    public partial class Line : Shape
    {
        static Line()
        {
            ElementHandlerRegistry.RegisterElementHandler<Line>(
                renderer => new LineHandler(renderer, new MCS.Line()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? X1 { get; set; }
        [Parameter] public double? X2 { get; set; }
        [Parameter] public double? Y1 { get; set; }
        [Parameter] public double? Y2 { get; set; }

        public new MCS.Line NativeControl => (ElementHandler as LineHandler)?.LineControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (X1 != null)
            {
                builder.AddAttribute(nameof(X1), AttributeHelper.DoubleToString(X1.Value));
            }
            if (X2 != null)
            {
                builder.AddAttribute(nameof(X2), AttributeHelper.DoubleToString(X2.Value));
            }
            if (Y1 != null)
            {
                builder.AddAttribute(nameof(Y1), AttributeHelper.DoubleToString(Y1.Value));
            }
            if (Y2 != null)
            {
                builder.AddAttribute(nameof(Y2), AttributeHelper.DoubleToString(Y2.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
