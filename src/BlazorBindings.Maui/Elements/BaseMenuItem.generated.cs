// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class BaseMenuItem : Element
    {
        static BaseMenuItem()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.BaseMenuItem NativeControl => (MC.BaseMenuItem)((Element)this).NativeControl;



        static partial void RegisterAdditionalHandlers();
    }
}
