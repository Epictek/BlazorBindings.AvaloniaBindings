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
    public partial class Slider : View
    {
        static Slider()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double Maximum { get; set; }
        [Parameter] public Color MaximumTrackColor { get; set; }
        [Parameter] public double Minimum { get; set; }
        [Parameter] public Color MinimumTrackColor { get; set; }
        [Parameter] public Color ThumbColor { get; set; }
        [Parameter] public MC.ImageSource ThumbImageSource { get; set; }
        [Parameter] public double Value { get; set; }

        public new MC.Slider NativeControl => (MC.Slider)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Slider();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Maximum):
                    if (!Equals(Maximum, value))
                    {
                        Maximum = (double)value;
                        NativeControl.Maximum = Maximum;
                    }
                    break;
                case nameof(MaximumTrackColor):
                    if (!Equals(MaximumTrackColor, value))
                    {
                        MaximumTrackColor = (Color)value;
                        NativeControl.MaximumTrackColor = MaximumTrackColor;
                    }
                    break;
                case nameof(Minimum):
                    if (!Equals(Minimum, value))
                    {
                        Minimum = (double)value;
                        NativeControl.Minimum = Minimum;
                    }
                    break;
                case nameof(MinimumTrackColor):
                    if (!Equals(MinimumTrackColor, value))
                    {
                        MinimumTrackColor = (Color)value;
                        NativeControl.MinimumTrackColor = MinimumTrackColor;
                    }
                    break;
                case nameof(ThumbColor):
                    if (!Equals(ThumbColor, value))
                    {
                        ThumbColor = (Color)value;
                        NativeControl.ThumbColor = ThumbColor;
                    }
                    break;
                case nameof(ThumbImageSource):
                    if (!Equals(ThumbImageSource, value))
                    {
                        ThumbImageSource = (MC.ImageSource)value;
                        NativeControl.ThumbImageSource = ThumbImageSource;
                    }
                    break;
                case nameof(Value):
                    if (!Equals(Value, value))
                    {
                        Value = (double)value;
                        NativeControl.Value = Value;
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
