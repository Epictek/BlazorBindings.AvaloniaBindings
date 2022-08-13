// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ProgressBar : View
    {
        static ProgressBar()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double Progress { get; set; }
        [Parameter] public Color ProgressColor { get; set; }

        public new MC.ProgressBar NativeControl => (MC.ProgressBar)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ProgressBar();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Progress):
                    if (!Equals(Progress, value))
                    {
                        Progress = (double)value;
                        NativeControl.Progress = Progress;
                    }
                    break;
                case nameof(ProgressColor):
                    if (!Equals(ProgressColor, value))
                    {
                        ProgressColor = (Color)value;
                        NativeControl.ProgressColor = ProgressColor;
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
