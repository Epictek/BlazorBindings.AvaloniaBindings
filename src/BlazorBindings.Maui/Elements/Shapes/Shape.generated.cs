// <auto-generated>
//     This code was generated by a BlazorBindings.Maui component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using MCS = Microsoft.Maui.Controls.Shapes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public abstract partial class Shape : BlazorBindings.Maui.Elements.View
    {
        static Shape()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<Shape>(nameof(Fill),
                (renderer, parent, component) => new ContentPropertyHandler<MCS.Shape>((x, value) => x.Fill = (MC.Brush)value));
            ElementHandlerRegistry.RegisterPropertyContentHandler<Shape>(nameof(Stroke),
                (renderer, parent, component) => new ContentPropertyHandler<MCS.Shape>((x, value) => x.Stroke = (MC.Brush)value));
            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.Stretch? Aspect { get; set; }
        [Parameter] public Color FillColor { get; set; }
        [Parameter] public Color StrokeColor { get; set; }
        [Parameter] public double? StrokeDashOffset { get; set; }
        [Parameter] public MCS.PenLineCap? StrokeLineCap { get; set; }
        [Parameter] public MCS.PenLineJoin? StrokeLineJoin { get; set; }
        [Parameter] public double? StrokeMiterLimit { get; set; }
        [Parameter] public double? StrokeThickness { get; set; }
        [Parameter] public RenderFragment Fill { get; set; }
        [Parameter] public RenderFragment Stroke { get; set; }

        public new MCS.Shape NativeControl => (MCS.Shape)((BindableObject)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Aspect):
                    if (!Equals(Aspect, value))
                    {
                        Aspect = (MC.Stretch?)value;
                        NativeControl.Aspect = Aspect ?? (MC.Stretch)MCS.Shape.AspectProperty.DefaultValue;
                    }
                    break;
                case nameof(FillColor):
                    if (!Equals(FillColor, value))
                    {
                        FillColor = (Color)value;
                        NativeControl.Fill = FillColor;
                    }
                    break;
                case nameof(StrokeColor):
                    if (!Equals(StrokeColor, value))
                    {
                        StrokeColor = (Color)value;
                        NativeControl.Stroke = StrokeColor;
                    }
                    break;
                case nameof(StrokeDashOffset):
                    if (!Equals(StrokeDashOffset, value))
                    {
                        StrokeDashOffset = (double?)value;
                        NativeControl.StrokeDashOffset = StrokeDashOffset ?? (double)MCS.Shape.StrokeDashOffsetProperty.DefaultValue;
                    }
                    break;
                case nameof(StrokeLineCap):
                    if (!Equals(StrokeLineCap, value))
                    {
                        StrokeLineCap = (MCS.PenLineCap?)value;
                        NativeControl.StrokeLineCap = StrokeLineCap ?? (MCS.PenLineCap)MCS.Shape.StrokeLineCapProperty.DefaultValue;
                    }
                    break;
                case nameof(StrokeLineJoin):
                    if (!Equals(StrokeLineJoin, value))
                    {
                        StrokeLineJoin = (MCS.PenLineJoin?)value;
                        NativeControl.StrokeLineJoin = StrokeLineJoin ?? (MCS.PenLineJoin)MCS.Shape.StrokeLineJoinProperty.DefaultValue;
                    }
                    break;
                case nameof(StrokeMiterLimit):
                    if (!Equals(StrokeMiterLimit, value))
                    {
                        StrokeMiterLimit = (double?)value;
                        NativeControl.StrokeMiterLimit = StrokeMiterLimit ?? (double)MCS.Shape.StrokeMiterLimitProperty.DefaultValue;
                    }
                    break;
                case nameof(StrokeThickness):
                    if (!Equals(StrokeThickness, value))
                    {
                        StrokeThickness = (double?)value;
                        NativeControl.StrokeThickness = StrokeThickness ?? (double)MCS.Shape.StrokeThicknessProperty.DefaultValue;
                    }
                    break;
                case nameof(Fill):
                    Fill = (RenderFragment)value;
                    break;
                case nameof(Stroke):
                    Stroke = (RenderFragment)value;
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Shape), Fill);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Shape), Stroke);
        }

        static partial void RegisterAdditionalHandlers();
    }
}
