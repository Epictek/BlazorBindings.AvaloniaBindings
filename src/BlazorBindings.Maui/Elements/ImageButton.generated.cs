// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ImageButton : View
    {
        static ImageButton()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Aspect Aspect { get; set; }
        [Parameter] public Color BorderColor { get; set; }
        [Parameter] public double BorderWidth { get; set; }
        [Parameter] public int CornerRadius { get; set; }
        [Parameter] public bool IsOpaque { get; set; }
        [Parameter] public Thickness Padding { get; set; }
        [Parameter] public MC.ImageSource Source { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public EventCallback OnPress { get; set; }
        [Parameter] public EventCallback OnRelease { get; set; }

        public new MC.ImageButton NativeControl => (MC.ImageButton)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ImageButton();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Aspect):
                    if (!Equals(Aspect, value))
                    {
                        Aspect = (Aspect)value;
                        NativeControl.Aspect = Aspect;
                    }
                    break;
                case nameof(BorderColor):
                    if (!Equals(BorderColor, value))
                    {
                        BorderColor = (Color)value;
                        NativeControl.BorderColor = BorderColor;
                    }
                    break;
                case nameof(BorderWidth):
                    if (!Equals(BorderWidth, value))
                    {
                        BorderWidth = (double)value;
                        NativeControl.BorderWidth = BorderWidth;
                    }
                    break;
                case nameof(CornerRadius):
                    if (!Equals(CornerRadius, value))
                    {
                        CornerRadius = (int)value;
                        NativeControl.CornerRadius = CornerRadius;
                    }
                    break;
                case nameof(IsOpaque):
                    if (!Equals(IsOpaque, value))
                    {
                        IsOpaque = (bool)value;
                        NativeControl.IsOpaque = IsOpaque;
                    }
                    break;
                case nameof(Padding):
                    if (!Equals(Padding, value))
                    {
                        Padding = (Thickness)value;
                        NativeControl.Padding = Padding;
                    }
                    break;
                case nameof(Source):
                    if (!Equals(Source, value))
                    {
                        Source = (MC.ImageSource)value;
                        NativeControl.Source = Source;
                    }
                    break;
                case nameof(OnClick):
                    if (!Equals(OnClick, value))
                    {
                        void NativeControlClicked(object sender, System.EventArgs e) => OnClick.InvokeAsync();

                        OnClick = (EventCallback)value;
                        NativeControl.Clicked -= NativeControlClicked;
                        NativeControl.Clicked += NativeControlClicked;
                    }
                    break;
                case nameof(OnPress):
                    if (!Equals(OnPress, value))
                    {
                        void NativeControlPressed(object sender, System.EventArgs e) => OnPress.InvokeAsync();

                        OnPress = (EventCallback)value;
                        NativeControl.Pressed -= NativeControlPressed;
                        NativeControl.Pressed += NativeControlPressed;
                    }
                    break;
                case nameof(OnRelease):
                    if (!Equals(OnRelease, value))
                    {
                        void NativeControlReleased(object sender, System.EventArgs e) => OnRelease.InvokeAsync();

                        OnRelease = (EventCallback)value;
                        NativeControl.Released -= NativeControlReleased;
                        NativeControl.Released += NativeControlReleased;
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
