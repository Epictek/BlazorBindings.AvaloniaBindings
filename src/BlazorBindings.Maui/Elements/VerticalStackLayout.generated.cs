// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class VerticalStackLayout : StackBase
    {
        static VerticalStackLayout()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.VerticalStackLayout NativeControl => (MC.VerticalStackLayout)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.VerticalStackLayout();


        partial void RenderAdditionalAttributes(AttributesBuilder builder);
        static partial void RegisterAdditionalHandlers();
    }
}
