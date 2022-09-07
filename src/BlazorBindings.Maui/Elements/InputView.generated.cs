// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class InputView : View
    {
        static InputView()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double CharacterSpacing { get; set; }
        [Parameter] public bool IsReadOnly { get; set; }
        [Parameter] public bool IsSpellCheckEnabled { get; set; }
        [Parameter] public Keyboard Keyboard { get; set; }
        [Parameter] public int MaxLength { get; set; }
        [Parameter] public string Placeholder { get; set; }
        [Parameter] public Color PlaceholderColor { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public Color TextColor { get; set; }
        [Parameter] public TextTransform TextTransform { get; set; }
        [Parameter] public EventCallback<string> TextChanged { get; set; }

        public new MC.InputView NativeControl => (MC.InputView)((Element)this).NativeControl;


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
                case nameof(IsReadOnly):
                    if (!Equals(IsReadOnly, value))
                    {
                        IsReadOnly = (bool)value;
                        NativeControl.IsReadOnly = IsReadOnly;
                    }
                    break;
                case nameof(IsSpellCheckEnabled):
                    if (!Equals(IsSpellCheckEnabled, value))
                    {
                        IsSpellCheckEnabled = (bool)value;
                        NativeControl.IsSpellCheckEnabled = IsSpellCheckEnabled;
                    }
                    break;
                case nameof(Keyboard):
                    if (!Equals(Keyboard, value))
                    {
                        Keyboard = (Keyboard)value;
                        NativeControl.Keyboard = Keyboard;
                    }
                    break;
                case nameof(MaxLength):
                    if (!Equals(MaxLength, value))
                    {
                        MaxLength = (int)value;
                        NativeControl.MaxLength = MaxLength;
                    }
                    break;
                case nameof(Placeholder):
                    if (!Equals(Placeholder, value))
                    {
                        Placeholder = (string)value;
                        NativeControl.Placeholder = Placeholder;
                    }
                    break;
                case nameof(PlaceholderColor):
                    if (!Equals(PlaceholderColor, value))
                    {
                        PlaceholderColor = (Color)value;
                        NativeControl.PlaceholderColor = PlaceholderColor;
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
                case nameof(TextChanged):
                    if (!Equals(TextChanged, value))
                    {
                        void NativeControlTextChanged(object sender, MC.TextChangedEventArgs e) => TextChanged.InvokeAsync(NativeControl.Text);

                        TextChanged = (EventCallback<string>)value;
                        NativeControl.TextChanged -= NativeControlTextChanged;
                        NativeControl.TextChanged += NativeControlTextChanged;
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
