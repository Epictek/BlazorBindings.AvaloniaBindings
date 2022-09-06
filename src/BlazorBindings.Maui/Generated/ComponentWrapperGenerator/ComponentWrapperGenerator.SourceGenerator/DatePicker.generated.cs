// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class DatePicker : View
    {
        static DatePicker()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double CharacterSpacing { get; set; }
        [Parameter] public DateTime Date { get; set; }
        [Parameter] public MC.FontAttributes FontAttributes { get; set; }
        [Parameter] public bool FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double FontSize { get; set; }
        [Parameter] public string Format { get; set; }
        [Parameter] public DateTime MaximumDate { get; set; }
        [Parameter] public DateTime MinimumDate { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public EventCallback<DateTime> DateChanged { get; set; }

        public new MC.DatePicker NativeControl => (MC.DatePicker)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.DatePicker();

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
                case nameof(Date):
                    if (!Equals(Date, value))
                    {
                        Date = (DateTime)value;
                        NativeControl.Date = Date;
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
                case nameof(MaximumDate):
                    if (!Equals(MaximumDate, value))
                    {
                        MaximumDate = (DateTime)value;
                        NativeControl.MaximumDate = MaximumDate;
                    }
                    break;
                case nameof(MinimumDate):
                    if (!Equals(MinimumDate, value))
                    {
                        MinimumDate = (DateTime)value;
                        NativeControl.MinimumDate = MinimumDate;
                    }
                    break;
                case nameof(TextColor):
                    if (!Equals(TextColor, value))
                    {
                        TextColor = (Color)value;
                        NativeControl.TextColor = TextColor;
                    }
                    break;
                case nameof(DateChanged):
                    if (!Equals(DateChanged, value))
                    {
                        void NativeControlDateSelected(object sender, MC.DateChangedEventArgs e) => DateChanged.InvokeAsync(NativeControl.Date);

                        DateChanged = (EventCallback<DateTime>)value;
                        NativeControl.DateSelected -= NativeControlDateSelected;
                        NativeControl.DateSelected += NativeControlDateSelected;
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
