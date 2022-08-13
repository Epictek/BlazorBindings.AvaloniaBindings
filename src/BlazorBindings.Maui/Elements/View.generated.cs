// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class View : VisualElement
    {
        static View()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.LayoutOptions HorizontalOptions { get; set; }
        [Parameter] public Thickness Margin { get; set; }
        [Parameter] public MC.LayoutOptions VerticalOptions { get; set; }

        public new MC.View NativeControl => (MC.View)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(HorizontalOptions):
                    if (!Equals(HorizontalOptions, value))
                    {
                        HorizontalOptions = (MC.LayoutOptions)value;
                        NativeControl.HorizontalOptions = HorizontalOptions;
                    }
                    break;
                case nameof(Margin):
                    if (!Equals(Margin, value))
                    {
                        Margin = (Thickness)value;
                        NativeControl.Margin = Margin;
                    }
                    break;
                case nameof(VerticalOptions):
                    if (!Equals(VerticalOptions, value))
                    {
                        VerticalOptions = (MC.LayoutOptions)value;
                        NativeControl.VerticalOptions = VerticalOptions;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
        static partial void RegisterAdditionalHandlers();
    }
}
