// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TemplatedView : BlazorBindings.Maui.Elements.Compatibility.Layout
    {
        static TemplatedView()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.TemplatedView NativeControl => (MC.TemplatedView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.TemplatedView();


        partial void RenderAdditionalAttributes(AttributesBuilder builder);
        static partial void RegisterAdditionalHandlers();
    }
}
