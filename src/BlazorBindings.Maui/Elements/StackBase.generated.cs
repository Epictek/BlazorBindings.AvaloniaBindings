// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class StackBase : Layout
    {
        static StackBase()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double Spacing { get; set; }

        public new MC.StackBase NativeControl => (MC.StackBase)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Spacing):
                    if (!Equals(Spacing, value))
                    {
                        Spacing = (double)value;
                        NativeControl.Spacing = Spacing;
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
