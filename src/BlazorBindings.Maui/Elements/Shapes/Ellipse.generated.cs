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
    public partial class Ellipse : Shape
    {
        static Ellipse()
        {
            ElementHandlerRegistry.RegisterElementHandler<Ellipse>(
                renderer => new EllipseHandler(renderer, new MCS.Ellipse()));

            RegisterAdditionalHandlers();
        }

        public new MCS.Ellipse NativeControl => (ElementHandler as EllipseHandler)?.EllipseControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
