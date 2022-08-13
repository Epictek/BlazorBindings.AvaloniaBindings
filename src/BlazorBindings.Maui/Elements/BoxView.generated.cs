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
    public partial class BoxView : View
    {
        static BoxView()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }
        [Parameter] public CornerRadius CornerRadius { get; set; }

        public new MC.BoxView NativeControl => (MC.BoxView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.BoxView();

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
                case nameof(CornerRadius):
                    if (!Equals(CornerRadius, value))
                    {
                        CornerRadius = (CornerRadius)value;
                        NativeControl.CornerRadius = CornerRadius;
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
