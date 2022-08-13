// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Button : View
    {
        static Button()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color BorderColor { get; set; }
        [Parameter] public double BorderWidth { get; set; }
        [Parameter] public double CharacterSpacing { get; set; }
        [Parameter] public int CornerRadius { get; set; }
        [Parameter] public MC.FontAttributes FontAttributes { get; set; }
        [Parameter] public bool FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double FontSize { get; set; }
        [Parameter] public MC.ImageSource ImageSource { get; set; }
        [Parameter] public LineBreakMode LineBreakMode { get; set; }
        [Parameter] public Thickness Padding { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TextTransform TextTransform { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public EventCallback OnPress { get; set; }
        [Parameter] public EventCallback OnRelease { get; set; }

        public new MC.Button NativeControl => (MC.Button)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Button();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
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
                case nameof(CharacterSpacing):
                    if (!Equals(CharacterSpacing, value))
                    {
                        CharacterSpacing = (double)value;
                        NativeControl.CharacterSpacing = CharacterSpacing;
                    }
                    break;
                case nameof(CornerRadius):
                    if (!Equals(CornerRadius, value))
                    {
                        CornerRadius = (int)value;
                        NativeControl.CornerRadius = CornerRadius;
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
                case nameof(ImageSource):
                    if (!Equals(ImageSource, value))
                    {
                        ImageSource = (MC.ImageSource)value;
                        NativeControl.ImageSource = ImageSource;
                    }
                    break;
                case nameof(LineBreakMode):
                    if (!Equals(LineBreakMode, value))
                    {
                        LineBreakMode = (LineBreakMode)value;
                        NativeControl.LineBreakMode = LineBreakMode;
                    }
                    break;
                case nameof(Padding):
                    if (!Equals(Padding, value))
                    {
                        Padding = (Thickness)value;
                        NativeControl.Padding = Padding;
                    }
                    break;
                case nameof(Text):
                    if (!Equals(Text, value))
                    {
                        Text = (string)value;
                        NativeControl.Text = Text;
                    }
                    break;
                case nameof(TextColor):
                    if (!Equals(TextColor, value))
                    {
                        TextColor = (Color)value;
                        NativeControl.TextColor = TextColor;
                    }
                    break;
                case nameof(TextTransform):
                    if (!Equals(TextTransform, value))
                    {
                        TextTransform = (TextTransform)value;
                        NativeControl.TextTransform = TextTransform;
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
                case nameof(OnPress):
                    if (!Equals(OnPress, value))
                    {
                        void NativeControlPressed(object sender, EventArgs e) => OnPress.InvokeAsync();

                        OnPress = (EventCallback)value;
                        NativeControl.Pressed -= NativeControlPressed;
                        NativeControl.Pressed += NativeControlPressed;
                    }
                    break;
                case nameof(OnRelease):
                    if (!Equals(OnRelease, value))
                    {
                        void NativeControlReleased(object sender, EventArgs e) => OnRelease.InvokeAsync();

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

        static partial void RegisterAdditionalHandlers();
    }
}
