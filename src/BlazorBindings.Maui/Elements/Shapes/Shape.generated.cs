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

        [Parameter] public MC.Stretch Aspect { get; set; }
        [Parameter] public double StrokeDashOffset { get; set; }
        [Parameter] public MCS.PenLineCap StrokeLineCap { get; set; }
        [Parameter] public MCS.PenLineJoin StrokeLineJoin { get; set; }
        [Parameter] public double StrokeMiterLimit { get; set; }
        [Parameter] public double StrokeThickness { get; set; }

        public new MCS.Shape NativeControl => (MCS.Shape)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Aspect):
                    if (!Equals(Aspect, value))
                    {
                        Aspect = (MC.Stretch)value;
                        NativeControl.Aspect = Aspect;
                    }
                    break;
                case nameof(StrokeDashOffset):
                    if (!Equals(StrokeDashOffset, value))
                    {
                        StrokeDashOffset = (double)value;
                        NativeControl.StrokeDashOffset = StrokeDashOffset;
                    }
                    break;
                case nameof(StrokeLineCap):
                    if (!Equals(StrokeLineCap, value))
                    {
                        StrokeLineCap = (MCS.PenLineCap)value;
                        NativeControl.StrokeLineCap = StrokeLineCap;
                    }
                    break;
                case nameof(StrokeLineJoin):
                    if (!Equals(StrokeLineJoin, value))
                    {
                        StrokeLineJoin = (MCS.PenLineJoin)value;
                        NativeControl.StrokeLineJoin = StrokeLineJoin;
                    }
                    break;
                case nameof(StrokeMiterLimit):
                    if (!Equals(StrokeMiterLimit, value))
                    {
                        StrokeMiterLimit = (double)value;
                        NativeControl.StrokeMiterLimit = StrokeMiterLimit;
                    }
                    break;
                case nameof(StrokeThickness):
                    if (!Equals(StrokeThickness, value))
                    {
                        StrokeThickness = (double)value;
                        NativeControl.StrokeThickness = StrokeThickness;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
        static partial void RegisterAdditionalHandlers();
    }
}
