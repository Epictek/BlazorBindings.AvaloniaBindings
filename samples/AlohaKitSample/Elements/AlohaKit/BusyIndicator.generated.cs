// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using AC = AlohaKit.Controls;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.AlohaKit
{
    public partial class BusyIndicator : BlazorBindings.Maui.Elements.GraphicsView
    {
        static BusyIndicator()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color BackgroundColor { get; set; }
        [Parameter] public AC.BusyIndicatorDrawable BusyIndicatorDrawable { get; set; }
        [Parameter] public Color Color { get; set; }
        [Parameter] public bool HasShadow { get; set; }
        [Parameter] public Color ShadowColor { get; set; }

        public new AC.BusyIndicator NativeControl => (AC.BusyIndicator)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new AC.BusyIndicator();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(BackgroundColor):
                    if (!Equals(BackgroundColor, value))
                    {
                        BackgroundColor = (Color)value;
                        NativeControl.BackgroundColor = BackgroundColor;
                    }
                    break;
                case nameof(BusyIndicatorDrawable):
                    if (!Equals(BusyIndicatorDrawable, value))
                    {
                        BusyIndicatorDrawable = (AC.BusyIndicatorDrawable)value;
                        NativeControl.BusyIndicatorDrawable = BusyIndicatorDrawable;
                    }
                    break;
                case nameof(Color):
                    if (!Equals(Color, value))
                    {
                        Color = (Color)value;
                        NativeControl.Color = Color;
                    }
                    break;
                case nameof(HasShadow):
                    if (!Equals(HasShadow, value))
                    {
                        HasShadow = (bool)value;
                        NativeControl.HasShadow = HasShadow;
                    }
                    break;
                case nameof(ShadowColor):
                    if (!Equals(ShadowColor, value))
                    {
                        ShadowColor = (Color)value;
                        NativeControl.ShadowColor = ShadowColor;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        static partial void RegisterAdditionalHandlers();
    }
}
