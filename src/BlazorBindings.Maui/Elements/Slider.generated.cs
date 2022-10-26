// <auto-generated>
//     This code was generated by a BlazorBindings.Maui component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Slider : View
    {
        static Slider()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double? Maximum { get; set; }
        [Parameter] public Color MaximumTrackColor { get; set; }
        [Parameter] public double? Minimum { get; set; }
        [Parameter] public Color MinimumTrackColor { get; set; }
        [Parameter] public Color ThumbColor { get; set; }
        [Parameter] public MC.ImageSource ThumbImageSource { get; set; }
        [Parameter] public double? Value { get; set; }
        [Parameter] public EventCallback<double> ValueChanged { get; set; }
        [Parameter] public EventCallback OnDragStarted { get; set; }
        [Parameter] public EventCallback OnDragCompleted { get; set; }

        public new MC.Slider NativeControl => (MC.Slider)((BindableObject)this).NativeControl;

        protected override MC.Slider CreateNativeElement() => new();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Maximum):
                    if (!Equals(Maximum, value))
                    {
                        Maximum = (double?)value;
                        NativeControl.Maximum = Maximum ?? (double)MC.Slider.MaximumProperty.DefaultValue;
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
                        Minimum = (double?)value;
                        NativeControl.Minimum = Minimum ?? (double)MC.Slider.MinimumProperty.DefaultValue;
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
                        Value = (double?)value;
                        NativeControl.Value = Value ?? (double)MC.Slider.ValueProperty.DefaultValue;
                    }
                    break;
                case nameof(ValueChanged):
                    if (!Equals(ValueChanged, value))
                    {
                        void NativeControlValueChanged(object sender, MC.ValueChangedEventArgs e)
                        {
                            var value = NativeControl.Value;
                            Value = value;
                            InvokeAsync(() => ValueChanged.InvokeAsync(value));
                        }

                        ValueChanged = (EventCallback<double>)value;
                        NativeControl.ValueChanged -= NativeControlValueChanged;
                        NativeControl.ValueChanged += NativeControlValueChanged;
                    }
                    break;
                case nameof(OnDragStarted):
                    if (!Equals(OnDragStarted, value))
                    {
                        void NativeControlDragStarted(object sender, EventArgs e) => InvokeAsync(() => OnDragStarted.InvokeAsync());

                        OnDragStarted = (EventCallback)value;
                        NativeControl.DragStarted -= NativeControlDragStarted;
                        NativeControl.DragStarted += NativeControlDragStarted;
                    }
                    break;
                case nameof(OnDragCompleted):
                    if (!Equals(OnDragCompleted, value))
                    {
                        void NativeControlDragCompleted(object sender, EventArgs e) => InvokeAsync(() => OnDragCompleted.InvokeAsync());

                        OnDragCompleted = (EventCallback)value;
                        NativeControl.DragCompleted -= NativeControlDragCompleted;
                        NativeControl.DragCompleted += NativeControlDragCompleted;
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
