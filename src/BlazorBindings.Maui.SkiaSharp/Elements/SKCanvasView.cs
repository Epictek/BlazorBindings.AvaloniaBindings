// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui.Elements;
using Microsoft.AspNetCore.Components;
using SkiaSharp.Views.Maui;
using MC = Microsoft.Maui.Controls;
using SK = SkiaSharp.Views.Maui.Controls;

namespace BlazorBindings.Maui.SkiaSharp;

public class SKCanvasView : View
{
    [Parameter] public EventCallback<SKPaintSurfaceEventArgs> OnPaintSurface { get; set; }

    public new SK.SKCanvasView NativeControl => (SK.SKCanvasView)((Element)this).NativeControl;

    protected override MC.Element CreateNativeElement() => new SK.SKCanvasView();

    protected override void HandleParameter(string name, object value)
    {
        if (name == nameof(OnPaintSurface))
        {
            if (!Equals(OnPaintSurface, value))
            {
                void NativeControlPaintSurface(object sender, SKPaintSurfaceEventArgs e) => OnPaintSurface.InvokeAsync(e);

                OnPaintSurface = (EventCallback<SKPaintSurfaceEventArgs>)value;
                NativeControl.PaintSurface -= NativeControlPaintSurface;
                NativeControl.PaintSurface += NativeControlPaintSurface;
            }
        }
        else
        {
            base.HandleParameter(name, value);
        }
    }

    public void InvalidateSurface()
    {
        NativeControl.InvalidateSurface();
    }
}
