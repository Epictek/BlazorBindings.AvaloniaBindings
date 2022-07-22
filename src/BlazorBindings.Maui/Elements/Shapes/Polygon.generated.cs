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
    public partial class Polygon : Shape
    {
        static Polygon()
        {
            ElementHandlerRegistry.RegisterElementHandler<Polygon>(
                renderer => new PolygonHandler(renderer, new MCS.Polygon()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MCS.FillRule? FillRule { get; set; }

        public new MCS.Polygon NativeControl => (ElementHandler as PolygonHandler)?.PolygonControl;

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
