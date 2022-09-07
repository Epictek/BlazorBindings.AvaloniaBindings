// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ActivityIndicator : View
    {
        static ActivityIndicator()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }
        [Parameter] public bool IsRunning { get; set; }

        public new MC.ActivityIndicator NativeControl => (MC.ActivityIndicator)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ActivityIndicator();

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
                case nameof(IsRunning):
                    if (!Equals(IsRunning, value))
                    {
                        IsRunning = (bool)value;
                        NativeControl.IsRunning = IsRunning;
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
