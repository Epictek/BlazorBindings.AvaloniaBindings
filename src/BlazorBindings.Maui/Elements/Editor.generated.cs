// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Editor : InputView
    {
        static Editor()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.EditorAutoSizeOption AutoSize { get; set; }
        [Parameter] public int CursorPosition { get; set; }
        [Parameter] public MC.FontAttributes FontAttributes { get; set; }
        [Parameter] public bool FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double FontSize { get; set; }
        [Parameter] public TextAlignment HorizontalTextAlignment { get; set; }
        [Parameter] public bool IsTextPredictionEnabled { get; set; }
        [Parameter] public int SelectionLength { get; set; }
        [Parameter] public TextAlignment VerticalTextAlignment { get; set; }
        [Parameter] public EventCallback OnCompleted { get; set; }

        public new MC.Editor NativeControl => (MC.Editor)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Editor();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(AutoSize):
                    if (!Equals(AutoSize, value))
                    {
                        AutoSize = (MC.EditorAutoSizeOption)value;
                        NativeControl.AutoSize = AutoSize;
                    }
                    break;
                case nameof(CursorPosition):
                    if (!Equals(CursorPosition, value))
                    {
                        CursorPosition = (int)value;
                        NativeControl.CursorPosition = CursorPosition;
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
                case nameof(IsTextPredictionEnabled):
                    if (!Equals(IsTextPredictionEnabled, value))
                    {
                        IsTextPredictionEnabled = (bool)value;
                        NativeControl.IsTextPredictionEnabled = IsTextPredictionEnabled;
                    }
                    break;
                case nameof(SelectionLength):
                    if (!Equals(SelectionLength, value))
                    {
                        SelectionLength = (int)value;
                        NativeControl.SelectionLength = SelectionLength;
                    }
                    break;
                case nameof(VerticalTextAlignment):
                    if (!Equals(VerticalTextAlignment, value))
                    {
                        VerticalTextAlignment = (TextAlignment)value;
                        NativeControl.VerticalTextAlignment = VerticalTextAlignment;
                    }
                    break;
                case nameof(OnCompleted):
                    if (!Equals(OnCompleted, value))
                    {
                        void NativeControlCompleted(object sender, EventArgs e) => OnCompleted.InvokeAsync();

                        OnCompleted = (EventCallback)value;
                        NativeControl.Completed -= NativeControlCompleted;
                        NativeControl.Completed += NativeControlCompleted;
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
