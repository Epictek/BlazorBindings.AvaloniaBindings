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
    public partial class Line : Shape
    {
        static Line()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double X1 { get; set; }
        [Parameter] public double X2 { get; set; }
        [Parameter] public double Y1 { get; set; }
        [Parameter] public double Y2 { get; set; }

        public new MCS.Line NativeControl => (MCS.Line)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MCS.Line();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(X1):
                    if (!Equals(X1, value))
                    {
                        X1 = (double)value;
                        NativeControl.X1 = X1;
                    }
                    break;
                case nameof(X2):
                    if (!Equals(X2, value))
                    {
                        X2 = (double)value;
                        NativeControl.X2 = X2;
                    }
                    break;
                case nameof(Y1):
                    if (!Equals(Y1, value))
                    {
                        Y1 = (double)value;
                        NativeControl.Y1 = Y1;
                    }
                    break;
                case nameof(Y2):
                    if (!Equals(Y2, value))
                    {
                        Y2 = (double)value;
                        NativeControl.Y2 = Y2;
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
