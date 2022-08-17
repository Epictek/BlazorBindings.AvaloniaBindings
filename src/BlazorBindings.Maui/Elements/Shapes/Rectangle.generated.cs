// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using MC = Microsoft.Maui.Controls;
using MCS = Microsoft.Maui.Controls.Shapes;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public partial class Rectangle : Shape
    {
        static Rectangle()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double RadiusX { get; set; }
        [Parameter] public double RadiusY { get; set; }

        public new MCS.Rectangle NativeControl => (MCS.Rectangle)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MCS.Rectangle();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(RadiusX):
                    if (!Equals(RadiusX, value))
                    {
                        RadiusX = (double)value;
                        NativeControl.RadiusX = RadiusX;
                    }
                    break;
                case nameof(RadiusY):
                    if (!Equals(RadiusY, value))
                    {
                        RadiusY = (double)value;
                        NativeControl.RadiusY = RadiusY;
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
