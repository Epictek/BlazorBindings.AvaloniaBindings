// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class SolidColorBrush : Brush
    {
        static SolidColorBrush()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }

        public new MC.SolidColorBrush NativeControl => (MC.SolidColorBrush)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.SolidColorBrush();

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

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        static partial void RegisterAdditionalHandlers();
    }
}
