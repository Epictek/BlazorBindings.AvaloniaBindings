// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class RadialGradientBrush : GradientBrush
    {
        static RadialGradientBrush()
        {
            ElementHandlerRegistry.RegisterElementHandler<RadialGradientBrush>(
                renderer => new RadialGradientBrushHandler(renderer, new MC.RadialGradientBrush()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Point? Center { get; set; }
        [Parameter] public double? Radius { get; set; }

        public new MC.RadialGradientBrush NativeControl => (ElementHandler as RadialGradientBrushHandler)?.RadialGradientBrushControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Center != null)
            {
                builder.AddAttribute(nameof(Center), AttributeHelper.PointToString(Center.Value));
            }
            if (Radius != null)
            {
                builder.AddAttribute(nameof(Radius), AttributeHelper.DoubleToString(Radius.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
