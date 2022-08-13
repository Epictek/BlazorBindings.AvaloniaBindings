// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Switch : View
    {
        static Switch()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public bool IsToggled { get; set; }
        [Parameter] public Color OnColor { get; set; }
        [Parameter] public Color ThumbColor { get; set; }

        public new MC.Switch NativeControl => (MC.Switch)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Switch();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(IsToggled):
                    if (!Equals(IsToggled, value))
                    {
                        IsToggled = (bool)value;
                        NativeControl.IsToggled = IsToggled;
                    }
                    break;
                case nameof(OnColor):
                    if (!Equals(OnColor, value))
                    {
                        OnColor = (Color)value;
                        NativeControl.OnColor = OnColor;
                    }
                    break;
                case nameof(ThumbColor):
                    if (!Equals(ThumbColor, value))
                    {
                        ThumbColor = (Color)value;
                        NativeControl.ThumbColor = ThumbColor;
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
