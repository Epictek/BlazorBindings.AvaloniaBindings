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
    public partial class Ellipse : Shape
    {
        static Ellipse()
        {
            RegisterAdditionalHandlers();
        }

        public new MCS.Ellipse NativeControl => (MCS.Ellipse)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MCS.Ellipse();


        static partial void RegisterAdditionalHandlers();
    }
}
