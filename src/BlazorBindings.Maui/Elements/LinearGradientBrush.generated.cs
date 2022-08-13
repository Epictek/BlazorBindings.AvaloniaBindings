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
    public partial class LinearGradientBrush : GradientBrush
    {
        static LinearGradientBrush()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Point EndPoint { get; set; }
        [Parameter] public Point StartPoint { get; set; }

        public new MC.LinearGradientBrush NativeControl => (MC.LinearGradientBrush)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.LinearGradientBrush();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(EndPoint):
                    if (!Equals(EndPoint, value))
                    {
                        EndPoint = (Point)value;
                        NativeControl.EndPoint = EndPoint;
                    }
                    break;
                case nameof(StartPoint):
                    if (!Equals(StartPoint, value))
                    {
                        StartPoint = (Point)value;
                        NativeControl.StartPoint = StartPoint;
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
