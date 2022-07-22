// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.Shapes.Handlers;
using MC = Microsoft.Maui.Controls;
using MCS = Microsoft.Maui.Controls.Shapes;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public abstract partial class Shape : BlazorBindings.Maui.Elements.View
    {
        static Shape()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.Stretch? Aspect { get; set; }
        [Parameter] public double? StrokeDashOffset { get; set; }
        [Parameter] public MCS.PenLineCap? StrokeLineCap { get; set; }
        [Parameter] public MCS.PenLineJoin? StrokeLineJoin { get; set; }
        [Parameter] public double? StrokeMiterLimit { get; set; }
        [Parameter] public double? StrokeThickness { get; set; }

        public new MCS.Shape NativeControl => (ElementHandler as ShapeHandler)?.ShapeControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Aspect != null)
            {
                builder.AddAttribute(nameof(Aspect), (int)Aspect.Value);
            }
            if (StrokeDashOffset != null)
            {
                builder.AddAttribute(nameof(StrokeDashOffset), AttributeHelper.DoubleToString(StrokeDashOffset.Value));
            }
            if (StrokeLineCap != null)
            {
                builder.AddAttribute(nameof(StrokeLineCap), (int)StrokeLineCap.Value);
            }
            if (StrokeLineJoin != null)
            {
                builder.AddAttribute(nameof(StrokeLineJoin), (int)StrokeLineJoin.Value);
            }
            if (StrokeMiterLimit != null)
            {
                builder.AddAttribute(nameof(StrokeMiterLimit), AttributeHelper.DoubleToString(StrokeMiterLimit.Value));
            }
            if (StrokeThickness != null)
            {
                builder.AddAttribute(nameof(StrokeThickness), AttributeHelper.DoubleToString(StrokeThickness.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
