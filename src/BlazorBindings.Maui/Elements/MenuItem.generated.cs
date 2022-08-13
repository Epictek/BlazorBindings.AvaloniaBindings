// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class MenuItem : BaseMenuItem
    {
        static MenuItem()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public string @class { get; set; }
        [Parameter] public MC.ImageSource IconImageSource { get; set; }
        [Parameter] public bool IsDestructive { get; set; }
        [Parameter] public bool IsEnabled { get; set; }
        [Parameter] public string StyleClass { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        public new MC.MenuItem NativeControl => (MC.MenuItem)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.MenuItem();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(@class):
                    if (!Equals(@class, value))
                    {
                        @class = (string)value;
                        NativeControl.@class = AttributeHelper.GetStringList(@class);
                    }
                    break;
                case nameof(IconImageSource):
                    if (!Equals(IconImageSource, value))
                    {
                        IconImageSource = (MC.ImageSource)value;
                        NativeControl.IconImageSource = IconImageSource;
                    }
                    break;
                case nameof(IsDestructive):
                    if (!Equals(IsDestructive, value))
                    {
                        IsDestructive = (bool)value;
                        NativeControl.IsDestructive = IsDestructive;
                    }
                    break;
                case nameof(IsEnabled):
                    if (!Equals(IsEnabled, value))
                    {
                        IsEnabled = (bool)value;
                        NativeControl.IsEnabled = IsEnabled;
                    }
                    break;
                case nameof(StyleClass):
                    if (!Equals(StyleClass, value))
                    {
                        StyleClass = (string)value;
                        NativeControl.StyleClass = AttributeHelper.GetStringList(StyleClass);
                    }
                    break;
                case nameof(Text):
                    if (!Equals(Text, value))
                    {
                        Text = (string)value;
                        NativeControl.Text = Text;
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
