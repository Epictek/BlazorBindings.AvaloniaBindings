// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Shell : Page
    {
        static Shell()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color FlyoutBackgroundColor { get; set; }
        [Parameter] public MC.ImageSource FlyoutBackgroundImage { get; set; }
        [Parameter] public Aspect FlyoutBackgroundImageAspect { get; set; }
        [Parameter] public FlyoutBehavior FlyoutBehavior { get; set; }
        [Parameter] public MC.FlyoutHeaderBehavior FlyoutHeaderBehavior { get; set; }
        [Parameter] public double FlyoutHeight { get; set; }
        [Parameter] public MC.ImageSource FlyoutIcon { get; set; }
        [Parameter] public bool FlyoutIsPresented { get; set; }
        [Parameter] public MC.ScrollMode FlyoutVerticalScrollMode { get; set; }
        [Parameter] public double FlyoutWidth { get; set; }
        [Parameter] public EventCallback<MC.ShellNavigatedEventArgs> OnNavigated { get; set; }
        [Parameter] public EventCallback<MC.ShellNavigatingEventArgs> OnNavigating { get; set; }

        public new MC.Shell NativeControl => (MC.Shell)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Shell();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(FlyoutBackgroundColor):
                    if (!Equals(FlyoutBackgroundColor, value))
                    {
                        FlyoutBackgroundColor = (Color)value;
                        NativeControl.FlyoutBackgroundColor = FlyoutBackgroundColor;
                    }
                    break;
                case nameof(FlyoutBackgroundImage):
                    if (!Equals(FlyoutBackgroundImage, value))
                    {
                        FlyoutBackgroundImage = (MC.ImageSource)value;
                        NativeControl.FlyoutBackgroundImage = FlyoutBackgroundImage;
                    }
                    break;
                case nameof(FlyoutBackgroundImageAspect):
                    if (!Equals(FlyoutBackgroundImageAspect, value))
                    {
                        FlyoutBackgroundImageAspect = (Aspect)value;
                        NativeControl.FlyoutBackgroundImageAspect = FlyoutBackgroundImageAspect;
                    }
                    break;
                case nameof(FlyoutBehavior):
                    if (!Equals(FlyoutBehavior, value))
                    {
                        FlyoutBehavior = (FlyoutBehavior)value;
                        NativeControl.FlyoutBehavior = FlyoutBehavior;
                    }
                    break;
                case nameof(FlyoutHeaderBehavior):
                    if (!Equals(FlyoutHeaderBehavior, value))
                    {
                        FlyoutHeaderBehavior = (MC.FlyoutHeaderBehavior)value;
                        NativeControl.FlyoutHeaderBehavior = FlyoutHeaderBehavior;
                    }
                    break;
                case nameof(FlyoutHeight):
                    if (!Equals(FlyoutHeight, value))
                    {
                        FlyoutHeight = (double)value;
                        NativeControl.FlyoutHeight = FlyoutHeight;
                    }
                    break;
                case nameof(FlyoutIcon):
                    if (!Equals(FlyoutIcon, value))
                    {
                        FlyoutIcon = (MC.ImageSource)value;
                        NativeControl.FlyoutIcon = FlyoutIcon;
                    }
                    break;
                case nameof(FlyoutIsPresented):
                    if (!Equals(FlyoutIsPresented, value))
                    {
                        FlyoutIsPresented = (bool)value;
                        NativeControl.FlyoutIsPresented = FlyoutIsPresented;
                    }
                    break;
                case nameof(FlyoutVerticalScrollMode):
                    if (!Equals(FlyoutVerticalScrollMode, value))
                    {
                        FlyoutVerticalScrollMode = (MC.ScrollMode)value;
                        NativeControl.FlyoutVerticalScrollMode = FlyoutVerticalScrollMode;
                    }
                    break;
                case nameof(FlyoutWidth):
                    if (!Equals(FlyoutWidth, value))
                    {
                        FlyoutWidth = (double)value;
                        NativeControl.FlyoutWidth = FlyoutWidth;
                    }
                    break;
                case nameof(OnNavigated):
                    if (!Equals(OnNavigated, value))
                    {
                        void NativeControlNavigated(object sender, MC.ShellNavigatedEventArgs e) => OnNavigated.InvokeAsync(e);

                        OnNavigated = (EventCallback<MC.ShellNavigatedEventArgs>)value;
                        NativeControl.Navigated -= NativeControlNavigated;
                        NativeControl.Navigated += NativeControlNavigated;
                    }
                    break;
                case nameof(OnNavigating):
                    if (!Equals(OnNavigating, value))
                    {
                        void NativeControlNavigating(object sender, MC.ShellNavigatingEventArgs e) => OnNavigating.InvokeAsync(e);

                        OnNavigating = (EventCallback<MC.ShellNavigatingEventArgs>)value;
                        NativeControl.Navigating -= NativeControlNavigating;
                        NativeControl.Navigating += NativeControlNavigating;
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
