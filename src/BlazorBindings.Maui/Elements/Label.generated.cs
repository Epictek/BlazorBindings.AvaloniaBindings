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
    public partial class Label : View
    {
        static Label()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double CharacterSpacing { get; set; }
        [Parameter] public MC.FontAttributes FontAttributes { get; set; }
        [Parameter] public bool FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double FontSize { get; set; }
        [Parameter] public TextAlignment HorizontalTextAlignment { get; set; }
        [Parameter] public LineBreakMode LineBreakMode { get; set; }
        [Parameter] public double LineHeight { get; set; }
        [Parameter] public int MaxLines { get; set; }
        [Parameter] public Thickness Padding { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TextDecorations TextDecorations { get; set; }
        [Parameter] public TextTransform TextTransform { get; set; }
        [Parameter] public TextType TextType { get; set; }
        [Parameter] public TextAlignment VerticalTextAlignment { get; set; }

        public new MC.Label NativeControl => (MC.Label)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Label();

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
                case nameof(HorizontalTextAlignment):
                    if (!Equals(HorizontalTextAlignment, value))
                    {
                        HorizontalTextAlignment = (TextAlignment)value;
                        NativeControl.HorizontalTextAlignment = HorizontalTextAlignment;
                    }
                    break;
                case nameof(LineBreakMode):
                    if (!Equals(LineBreakMode, value))
                    {
                        LineBreakMode = (LineBreakMode)value;
                        NativeControl.LineBreakMode = LineBreakMode;
                    }
                    break;
                case nameof(LineHeight):
                    if (!Equals(LineHeight, value))
                    {
                        LineHeight = (double)value;
                        NativeControl.LineHeight = LineHeight;
                    }
                    break;
                case nameof(MaxLines):
                    if (!Equals(MaxLines, value))
                    {
                        MaxLines = (int)value;
                        NativeControl.MaxLines = MaxLines;
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
                case nameof(TextDecorations):
                    if (!Equals(TextDecorations, value))
                    {
                        TextDecorations = (TextDecorations)value;
                        NativeControl.TextDecorations = TextDecorations;
                    }
                    break;
                case nameof(TextTransform):
                    if (!Equals(TextTransform, value))
                    {
                        TextTransform = (TextTransform)value;
                        NativeControl.TextTransform = TextTransform;
                    }
                    break;
                case nameof(TextType):
                    if (!Equals(TextType, value))
                    {
                        TextType = (TextType)value;
                        NativeControl.TextType = TextType;
                    }
                    break;
                case nameof(VerticalTextAlignment):
                    if (!Equals(VerticalTextAlignment, value))
                    {
                        VerticalTextAlignment = (TextAlignment)value;
                        NativeControl.VerticalTextAlignment = VerticalTextAlignment;
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
