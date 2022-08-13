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
    public partial class Image : View
    {
        static Image()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Aspect Aspect { get; set; }
        [Parameter] public bool IsAnimationPlaying { get; set; }
        [Parameter] public bool IsOpaque { get; set; }
        [Parameter] public MC.ImageSource Source { get; set; }

        public new MC.Image NativeControl => (MC.Image)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Image();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Aspect):
                    if (!Equals(Aspect, value))
                    {
                        Aspect = (Aspect)value;
                        NativeControl.Aspect = Aspect;
                    }
                    break;
                case nameof(IsAnimationPlaying):
                    if (!Equals(IsAnimationPlaying, value))
                    {
                        IsAnimationPlaying = (bool)value;
                        NativeControl.IsAnimationPlaying = IsAnimationPlaying;
                    }
                    break;
                case nameof(IsOpaque):
                    if (!Equals(IsOpaque, value))
                    {
                        IsOpaque = (bool)value;
                        NativeControl.IsOpaque = IsOpaque;
                    }
                    break;
                case nameof(Source):
                    if (!Equals(Source, value))
                    {
                        Source = (MC.ImageSource)value;
                        NativeControl.Source = Source;
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
