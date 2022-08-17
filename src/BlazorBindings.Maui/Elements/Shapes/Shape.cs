// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui.Graphics;
using MC = Microsoft.Maui.Controls;
using MCS = Microsoft.Maui.Controls.Shapes;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public abstract partial class Shape : BlazorBindings.Maui.Elements.View
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<Shape>(nameof(Fill),
                _ => new ContentPropertyHandler<MCS.Shape>((shape, contentElement) => shape.Fill = (MC.Brush)contentElement));

            ElementHandlerRegistry.RegisterPropertyContentHandler<Shape>(nameof(Stroke),
                _ => new ContentPropertyHandler<MCS.Shape>((shape, contentElement) => shape.Stroke = (MC.Brush)contentElement));
        }

        [Parameter] public string StrokeDashArray { get; set; }
        [Parameter] public RenderFragment Fill { get; set; }
        [Parameter] public Color FillColor { get; set; }
        [Parameter] public RenderFragment Stroke { get; set; }
        [Parameter] public Color StrokeColor { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(StrokeDashArray):
                    if (!Equals(StrokeDashArray, value))
                    {
                        NativeControl.StrokeDashArray = AttributeHelper.StringToDoubleCollection((string)value);
                        StrokeDashArray = (string)value;
                    }
                    return true;
                case nameof(FillColor):
                    if (!Equals(FillColor, value))
                    {
                        NativeControl.Fill = (Color)value;
                        FillColor = (Color)value;
                    }
                    return true;
                case nameof(StrokeColor):
                    if (!Equals(StrokeColor, value))
                    {
                        NativeControl.Stroke = (Color)value;
                        StrokeColor = (Color)value;
                    }
                    return true;
                case nameof(Fill):
                    Fill = (RenderFragment)value;
                    return true;
                default:
                    return base.HandleAdditionalParameter(name, value);
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Shape), Fill);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Shape), Stroke);
        }
    }
}
