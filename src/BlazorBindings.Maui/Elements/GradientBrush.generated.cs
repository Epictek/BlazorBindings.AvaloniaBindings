// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class GradientBrush : Brush
    {
        static GradientBrush()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public EventCallback OnInvalidateGradientBrushRequested { get; set; }

        public new MC.GradientBrush NativeControl => (MC.GradientBrush)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(OnInvalidateGradientBrushRequested):
                    if (!Equals(OnInvalidateGradientBrushRequested, value))
                    {
                        void NativeControlInvalidateGradientBrushRequested(object sender, EventArgs e) => OnInvalidateGradientBrushRequested.InvokeAsync();

                        OnInvalidateGradientBrushRequested = (EventCallback)value;
                        NativeControl.InvalidateGradientBrushRequested -= NativeControlInvalidateGradientBrushRequested;
                        NativeControl.InvalidateGradientBrushRequested += NativeControlInvalidateGradientBrushRequested;
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
