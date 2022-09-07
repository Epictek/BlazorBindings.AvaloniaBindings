// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class GestureElement : Element
    {
        static GestureElement()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.GestureElement NativeControl => (MC.GestureElement)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.GestureElement();


        static partial void RegisterAdditionalHandlers();
    }
}
