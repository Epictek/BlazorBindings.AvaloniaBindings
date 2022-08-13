// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class GradientBrush : Brush
    {
        static GradientBrush()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.GradientBrush NativeControl => (MC.GradientBrush)((Element)this).NativeControl;



        static partial void RegisterAdditionalHandlers();
    }
}
