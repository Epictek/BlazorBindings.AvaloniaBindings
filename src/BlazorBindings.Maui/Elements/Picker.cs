// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class Picker<TItem> : View
    {
        public new MC.Picker NativeControl => base.NativeControl as MC.Picker;

        [Parameter] public List<TItem> ItemsSource { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public Func<TItem, string> ItemDisplayBinding { get; set; }
        [Parameter] public TItem SelectedItem { get; set; }
        [Parameter] public int? SelectedIndex { get; set; }
        [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
        [Parameter] public EventCallback<int> SelectedIndexChanged { get; set; }
        [Parameter] public double? CharacterSpacing { get; set; }
        /// <summary>
        /// Gets a value that indicates whether the font for the label is bold, italic, or neither.
        /// </summary>
        [Parameter] public MC.FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Gets the font family to which the font for the label belongs.
        /// </summary>
        [Parameter] public string FontFamily { get; set; }
        /// <summary>
        /// Gets the size of the font for the label.
        /// </summary>
        [Parameter] public double? FontSize { get; set; }
        /// <summary>
        /// Gets or sets the horizontal alignment of the Text property. This is a bindable property.
        /// </summary>
        [Parameter] public TextAlignment? HorizontalTextAlignment { get; set; }

#pragma warning disable CA1200 // Avoid using cref tags with a prefix; these are copied from Xamarin.Forms as-is
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.Color" /> for the text of this Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.Color" /> value.
        /// </value>
        [Parameter] public Color TextColor { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="T:Xamarin.Forms.Color" /> for the tite of this Label. This is a bindable property.
        /// </summary>
        /// <value>
        /// The <see cref="T:Xamarin.Forms.Color" /> value.
        /// </value>
        [Parameter] public Color TitleColor { get; set; }
        /// <summary>
        /// Gets or sets the vertical alignement of the Text property. This is a bindable property.
        /// </summary>
        [Parameter] public TextAlignment? VerticalTextAlignment { get; set; }
#pragma warning restore CA1200 // Avoid using cref tags with a prefix

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(CharacterSpacing):
                    if (CharacterSpacing != (double?)value)
                    {
                        CharacterSpacing = (double?)value;
                        NativeControl.CharacterSpacing = CharacterSpacing ?? (double)MC.Picker.CharacterSpacingProperty.DefaultValue;
                    }
                    break;
                case nameof(FontAttributes):
                    if (FontAttributes != (MC.FontAttributes?)value)
                    {
                        FontAttributes = (MC.FontAttributes?)value;
                        NativeControl.FontAttributes = FontAttributes ?? (MC.FontAttributes)MC.Picker.FontAttributesProperty.DefaultValue;
                    }
                    break;
                case nameof(FontFamily):
                    if (FontFamily != (string)value)
                    {
                        FontFamily = (string)value;
                        NativeControl.FontFamily = FontFamily;
                    }
                    break;
                case nameof(FontSize):
                    if (FontSize != (double?)value)
                    {
                        FontSize = (double?)value;
                        NativeControl.FontSize = FontSize ?? (double)MC.Picker.FontSizeProperty.DefaultValue;
                    }
                    break;
                case nameof(HorizontalTextAlignment):
                    if (HorizontalTextAlignment != (TextAlignment?)value)
                    {
                        HorizontalTextAlignment = (TextAlignment?)value;
                        NativeControl.HorizontalTextAlignment = HorizontalTextAlignment ?? (TextAlignment)MC.Picker.HorizontalTextAlignmentProperty.DefaultValue;
                    }
                    break;
                case nameof(ItemsSource):
                    if (!Equals(ItemsSource, value))
                    {
                        ItemsSource = (List<TItem>)value;
                        NativeControl.ItemsSource = ItemsSource;
                    }
                    break;
                case nameof(SelectedItem):
                    if (!Equals(SelectedItem, value))
                    {
                        SelectedItem = (TItem)value;
                        NativeControl.SelectedItem = SelectedItem;
                    }
                    break;
                case nameof(SelectedIndex):
                    if (SelectedIndex != (int?)value)
                    {
                        SelectedIndex = (int?)value;
                        NativeControl.SelectedIndex = SelectedIndex ?? -1;
                    }
                    break;
                case nameof(TextColor):
                    if (!Equals(TextColor, value))
                    {
                        TextColor = (Color)value;
                        NativeControl.TextColor = TextColor;
                    }
                    break;
                case nameof(Title):
                    if (!Equals(Title, value))
                    {
                        Title = (string)value;
                        NativeControl.Title = Title;
                    }
                    break;
                case nameof(TitleColor):
                    if (!Equals(TitleColor, value))
                    {
                        TitleColor = (Color)value;
                        NativeControl.TitleColor = TitleColor;
                    }
                    break;
                case nameof(VerticalTextAlignment):
                    if (!Equals(VerticalTextAlignment, value))
                    {
                        VerticalTextAlignment = (TextAlignment?)value;
                        NativeControl.VerticalTextAlignment = VerticalTextAlignment ?? (TextAlignment)MC.Picker.VerticalTextAlignmentProperty.DefaultValue;
                    }
                    break;
                case nameof(ItemDisplayBinding):
                    if (!Equals(ItemDisplayBinding, value))
                    {
                        ItemDisplayBinding = (Func<TItem, string>)value;
                        NativeControl.ItemDisplayBinding = new MC.Internals.TypedBinding<TItem, string>((item) => (ItemDisplayBinding(item), true), null, null);
                    }
                    break;

                case nameof(SelectedIndexChanged):
                    if (!Equals(SelectedIndexChanged, value))
                    {
                        void NativeControlSelectedIndexChanged(object sender, EventArgs e) => SelectedIndexChanged.InvokeAsync(NativeControl.SelectedIndex);

                        SelectedIndexChanged = (EventCallback<int>)value;
                        NativeControl.SelectedIndexChanged -= NativeControlSelectedIndexChanged;
                        NativeControl.SelectedIndexChanged += NativeControlSelectedIndexChanged;
                    }
                    break;
                case nameof(SelectedItemChanged):
                    if (!Equals(SelectedItemChanged, value))
                    {
                        void NativeControlSelectedIndexChanged(object sender, EventArgs e) => SelectedItemChanged.InvokeAsync((TItem)NativeControl.SelectedItem);

                        SelectedItemChanged = (EventCallback<TItem>)value;
                        NativeControl.SelectedIndexChanged -= NativeControlSelectedIndexChanged;
                        NativeControl.SelectedIndexChanged += NativeControlSelectedIndexChanged;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override MC.Element CreateNativeElement() => new MC.Picker();
    }
}
