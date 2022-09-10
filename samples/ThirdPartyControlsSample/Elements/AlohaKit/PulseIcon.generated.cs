// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using AC = AlohaKit.Controls;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.AlohaKit
{
    public partial class PulseIcon : BlazorBindings.Maui.Elements.GraphicsView
    {
        static PulseIcon()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public bool IsPulsing { get; set; }
        [Parameter] public Color PulseColor { get; set; }
        [Parameter] public AC.PulseIconDrawable PulseIconDrawable { get; set; }
        [Parameter] public string Source { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        public new AC.PulseIcon NativeControl => (AC.PulseIcon)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new AC.PulseIcon();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(IsPulsing):
                    if (!Equals(IsPulsing, value))
                    {
                        IsPulsing = (bool)value;
                        NativeControl.IsPulsing = IsPulsing;
                    }
                    break;
                case nameof(PulseColor):
                    if (!Equals(PulseColor, value))
                    {
                        PulseColor = (Color)value;
                        NativeControl.PulseColor = PulseColor;
                    }
                    break;
                case nameof(PulseIconDrawable):
                    if (!Equals(PulseIconDrawable, value))
                    {
                        PulseIconDrawable = (AC.PulseIconDrawable)value;
                        NativeControl.PulseIconDrawable = PulseIconDrawable;
                    }
                    break;
                case nameof(Source):
                    if (!Equals(Source, value))
                    {
                        Source = (string)value;
                        NativeControl.Source = Source;
                    }
                    break;
                case nameof(OnClick):
                    if (!Equals(OnClick, value))
                    {
                        void NativeControlClicked(object sender, EventArgs e) => OnClick.InvokeAsync();

                        OnClick = (EventCallback)value;
                        NativeControl.Clicked -= NativeControlClicked;
                        NativeControl.Clicked += NativeControlClicked;
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
