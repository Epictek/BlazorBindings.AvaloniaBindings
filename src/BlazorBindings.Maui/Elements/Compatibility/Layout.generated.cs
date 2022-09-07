// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using MC = Microsoft.Maui.Controls;
using MCC = Microsoft.Maui.Controls.Compatibility;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Compatibility
{
    public abstract partial class Layout : BlazorBindings.Maui.Elements.View
    {
        static Layout()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public bool CascadeInputTransparent { get; set; }
        [Parameter] public bool IsClippedToBounds { get; set; }
        [Parameter] public Thickness Padding { get; set; }

        public new MCC.Layout NativeControl => (MCC.Layout)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(CascadeInputTransparent):
                    if (!Equals(CascadeInputTransparent, value))
                    {
                        CascadeInputTransparent = (bool)value;
                        NativeControl.CascadeInputTransparent = CascadeInputTransparent;
                    }
                    break;
                case nameof(IsClippedToBounds):
                    if (!Equals(IsClippedToBounds, value))
                    {
                        IsClippedToBounds = (bool)value;
                        NativeControl.IsClippedToBounds = IsClippedToBounds;
                    }
                    break;
                case nameof(Padding):
                    if (!Equals(Padding, value))
                    {
                        Padding = (Thickness)value;
                        NativeControl.Padding = Padding;
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
