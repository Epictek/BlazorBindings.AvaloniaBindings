// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Border : View
    {
        static Border()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<Border>(nameof(ChildContent),
                _ => new ContentPropertyHandler<MC.Border>((x, value) => x.Content = (MC.View)value));
            RegisterAdditionalHandlers();
        }

        [Parameter] public Thickness Padding { get; set; }
        [Parameter] public double StrokeDashOffset { get; set; }
        [Parameter] public PenLineCap StrokeLineCap { get; set; }
        [Parameter] public PenLineJoin StrokeLineJoin { get; set; }
        [Parameter] public double StrokeMiterLimit { get; set; }
        [Parameter] public IShape StrokeShape { get; set; }
        [Parameter] public double StrokeThickness { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        public new MC.Border NativeControl => (MC.Border)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Border();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Padding):
                    if (!Equals(Padding, value))
                    {
                        Padding = (Thickness)value;
                        NativeControl.Padding = Padding;
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
                        StrokeLineCap = (PenLineCap)value;
                        NativeControl.StrokeLineCap = StrokeLineCap;
                    }
                    break;
                case nameof(StrokeLineJoin):
                    if (!Equals(StrokeLineJoin, value))
                    {
                        StrokeLineJoin = (PenLineJoin)value;
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
                case nameof(StrokeShape):
                    if (!Equals(StrokeShape, value))
                    {
                        StrokeShape = (IShape)value;
                        NativeControl.StrokeShape = StrokeShape;
                    }
                    break;
                case nameof(StrokeThickness):
                    if (!Equals(StrokeThickness, value))
                    {
                        StrokeThickness = (double)value;
                        NativeControl.StrokeThickness = StrokeThickness;
                    }
                    break;
                case nameof(ChildContent):
                    ChildContent = (RenderFragment)value;
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Border), ChildContent);;
        }

        static partial void RegisterAdditionalHandlers();
    }
}
