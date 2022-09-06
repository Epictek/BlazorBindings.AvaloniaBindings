// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class CheckBox : View
    {
        static CheckBox()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }
        [Parameter] public bool IsChecked { get; set; }
        [Parameter] public EventCallback<bool> IsCheckedChanged { get; set; }

        public new MC.CheckBox NativeControl => (MC.CheckBox)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.CheckBox();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Color):
                    if (!Equals(Color, value))
                    {
                        Color = (Color)value;
                        NativeControl.Color = Color;
                    }
                    break;
                case nameof(IsChecked):
                    if (!Equals(IsChecked, value))
                    {
                        IsChecked = (bool)value;
                        NativeControl.IsChecked = IsChecked;
                    }
                    break;
                case nameof(IsCheckedChanged):
                    if (!Equals(IsCheckedChanged, value))
                    {
                        void NativeControlCheckedChanged(object sender, MC.CheckedChangedEventArgs e) => IsCheckedChanged.InvokeAsync(NativeControl.IsChecked);

                        IsCheckedChanged = (EventCallback<bool>)value;
                        NativeControl.CheckedChanged -= NativeControlCheckedChanged;
                        NativeControl.CheckedChanged += NativeControlCheckedChanged;
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
