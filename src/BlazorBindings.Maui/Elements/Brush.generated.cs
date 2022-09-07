// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class Brush : Element
    {
        static Brush()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.Brush NativeControl => (MC.Brush)((Element)this).NativeControl;



        static partial void RegisterAdditionalHandlers();
    }
}
