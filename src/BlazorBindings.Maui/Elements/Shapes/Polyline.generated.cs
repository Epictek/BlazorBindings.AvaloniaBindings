// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.Shapes.Handlers;
using MC = Microsoft.Maui.Controls;
using MCS = Microsoft.Maui.Controls.Shapes;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Shapes
{
    public partial class Polyline : Shape
    {
        static Polyline()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public MCS.FillRule FillRule { get; set; }

        public new MCS.Polyline NativeControl => (MCS.Polyline)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MCS.Polyline();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(FillRule):
                    if (!Equals(FillRule, value))
                    {
                        FillRule = (MCS.FillRule)value;
                        NativeControl.FillRule = FillRule;
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
