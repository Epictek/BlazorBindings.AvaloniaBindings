// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TimePicker : View
    {
        static TimePicker()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double CharacterSpacing { get; set; }
        [Parameter] public MC.FontAttributes FontAttributes { get; set; }
        [Parameter] public bool FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double FontSize { get; set; }
        [Parameter] public string Format { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TimeSpan Time { get; set; }
        [Parameter] public EventCallback<TimeSpan> TimeChanged { get; set; }

        public new MC.TimePicker NativeControl => (MC.TimePicker)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.TimePicker();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(CharacterSpacing):
                    if (!Equals(CharacterSpacing, value))
                    {
                        CharacterSpacing = (double)value;
                        NativeControl.CharacterSpacing = CharacterSpacing;
                    }
                    break;
                case nameof(FontAttributes):
                    if (!Equals(FontAttributes, value))
                    {
                        FontAttributes = (MC.FontAttributes)value;
                        NativeControl.FontAttributes = FontAttributes;
                    }
                    break;
                case nameof(FontAutoScalingEnabled):
                    if (!Equals(FontAutoScalingEnabled, value))
                    {
                        FontAutoScalingEnabled = (bool)value;
                        NativeControl.FontAutoScalingEnabled = FontAutoScalingEnabled;
                    }
                    break;
                case nameof(FontFamily):
                    if (!Equals(FontFamily, value))
                    {
                        FontFamily = (string)value;
                        NativeControl.FontFamily = FontFamily;
                    }
                    break;
                case nameof(FontSize):
                    if (!Equals(FontSize, value))
                    {
                        FontSize = (double)value;
                        NativeControl.FontSize = FontSize;
                    }
                    break;
                case nameof(Format):
                    if (!Equals(Format, value))
                    {
                        Format = (string)value;
                        NativeControl.Format = Format;
                    }
                    break;
                case nameof(TextColor):
                    if (!Equals(TextColor, value))
                    {
                        TextColor = (Color)value;
                        NativeControl.TextColor = TextColor;
                    }
                    break;
                case nameof(Time):
                    if (!Equals(Time, value))
                    {
                        Time = (TimeSpan)value;
                        NativeControl.Time = Time;
                    }
                    break;
                case nameof(TimeChanged):
                    if (!Equals(TimeChanged, value))
                    {
                        void NativeControlPropertyChanged(object sender, PropertyChangedEventArgs e)
                        {
                            if (e.PropertyName == nameof(NativeControl.Time))
                            {
                                TimeChanged.InvokeAsync(NativeControl.Time);
                            }
                        }

                        TimeChanged = (EventCallback<TimeSpan>)value;
                        NativeControl.PropertyChanged -= NativeControlPropertyChanged;
                        NativeControl.PropertyChanged += NativeControlPropertyChanged;
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
