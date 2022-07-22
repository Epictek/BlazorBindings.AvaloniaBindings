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
    public partial class Polyline : Shape
    {
        static Polyline()
        {
            ElementHandlerRegistry.RegisterElementHandler<Polyline>(
                renderer => new PolylineHandler(renderer, new MCS.Polyline()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MCS.FillRule? FillRule { get; set; }

        public new MCS.Polyline NativeControl => (ElementHandler as PolylineHandler)?.PolylineControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (FillRule != null)
            {
                builder.AddAttribute(nameof(FillRule), (int)FillRule.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
