// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class VisualElement : NavigableElement
    {
        static VisualElement()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double AnchorX { get; set; }
        [Parameter] public double AnchorY { get; set; }
        [Parameter] public Color BackgroundColor { get; set; }
        [Parameter] public FlowDirection FlowDirection { get; set; }
        [Parameter] public Rect Frame { get; set; }
        [Parameter] public double HeightRequest { get; set; }
        [Parameter] public bool InputTransparent { get; set; }
        [Parameter] public bool IsEnabled { get; set; }
        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public double MaximumHeightRequest { get; set; }
        [Parameter] public double MaximumWidthRequest { get; set; }
        [Parameter] public double MinimumHeightRequest { get; set; }
        [Parameter] public double MinimumWidthRequest { get; set; }
        [Parameter] public double Opacity { get; set; }
        [Parameter] public double Rotation { get; set; }
        [Parameter] public double RotationX { get; set; }
        [Parameter] public double RotationY { get; set; }
        [Parameter] public double Scale { get; set; }
        [Parameter] public double ScaleX { get; set; }
        [Parameter] public double ScaleY { get; set; }
        [Parameter] public double TranslationX { get; set; }
        [Parameter] public double TranslationY { get; set; }
        [Parameter] public double WidthRequest { get; set; }
        [Parameter] public int ZIndex { get; set; }
        [Parameter] public EventCallback<MC.FocusEventArgs> OnFocused { get; set; }
        [Parameter] public EventCallback<MC.FocusEventArgs> OnUnfocused { get; set; }
        [Parameter] public EventCallback OnSizeChanged { get; set; }

        public new MC.VisualElement NativeControl => (MC.VisualElement)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(AnchorX):
                    if (!Equals(AnchorX, value))
                    {
                        AnchorX = (double)value;
                        NativeControl.AnchorX = AnchorX;
                    }
                    break;
                case nameof(AnchorY):
                    if (!Equals(AnchorY, value))
                    {
                        AnchorY = (double)value;
                        NativeControl.AnchorY = AnchorY;
                    }
                    break;
                case nameof(BackgroundColor):
                    if (!Equals(BackgroundColor, value))
                    {
                        BackgroundColor = (Color)value;
                        NativeControl.BackgroundColor = BackgroundColor;
                    }
                    break;
                case nameof(FlowDirection):
                    if (!Equals(FlowDirection, value))
                    {
                        FlowDirection = (FlowDirection)value;
                        NativeControl.FlowDirection = FlowDirection;
                    }
                    break;
                case nameof(Frame):
                    if (!Equals(Frame, value))
                    {
                        Frame = (Rect)value;
                        NativeControl.Frame = Frame;
                    }
                    break;
                case nameof(HeightRequest):
                    if (!Equals(HeightRequest, value))
                    {
                        HeightRequest = (double)value;
                        NativeControl.HeightRequest = HeightRequest;
                    }
                    break;
                case nameof(InputTransparent):
                    if (!Equals(InputTransparent, value))
                    {
                        InputTransparent = (bool)value;
                        NativeControl.InputTransparent = InputTransparent;
                    }
                    break;
                case nameof(IsEnabled):
                    if (!Equals(IsEnabled, value))
                    {
                        IsEnabled = (bool)value;
                        NativeControl.IsEnabled = IsEnabled;
                    }
                    break;
                case nameof(IsVisible):
                    if (!Equals(IsVisible, value))
                    {
                        IsVisible = (bool)value;
                        NativeControl.IsVisible = IsVisible;
                    }
                    break;
                case nameof(MaximumHeightRequest):
                    if (!Equals(MaximumHeightRequest, value))
                    {
                        MaximumHeightRequest = (double)value;
                        NativeControl.MaximumHeightRequest = MaximumHeightRequest;
                    }
                    break;
                case nameof(MaximumWidthRequest):
                    if (!Equals(MaximumWidthRequest, value))
                    {
                        MaximumWidthRequest = (double)value;
                        NativeControl.MaximumWidthRequest = MaximumWidthRequest;
                    }
                    break;
                case nameof(MinimumHeightRequest):
                    if (!Equals(MinimumHeightRequest, value))
                    {
                        MinimumHeightRequest = (double)value;
                        NativeControl.MinimumHeightRequest = MinimumHeightRequest;
                    }
                    break;
                case nameof(MinimumWidthRequest):
                    if (!Equals(MinimumWidthRequest, value))
                    {
                        MinimumWidthRequest = (double)value;
                        NativeControl.MinimumWidthRequest = MinimumWidthRequest;
                    }
                    break;
                case nameof(Opacity):
                    if (!Equals(Opacity, value))
                    {
                        Opacity = (double)value;
                        NativeControl.Opacity = Opacity;
                    }
                    break;
                case nameof(Rotation):
                    if (!Equals(Rotation, value))
                    {
                        Rotation = (double)value;
                        NativeControl.Rotation = Rotation;
                    }
                    break;
                case nameof(RotationX):
                    if (!Equals(RotationX, value))
                    {
                        RotationX = (double)value;
                        NativeControl.RotationX = RotationX;
                    }
                    break;
                case nameof(RotationY):
                    if (!Equals(RotationY, value))
                    {
                        RotationY = (double)value;
                        NativeControl.RotationY = RotationY;
                    }
                    break;
                case nameof(Scale):
                    if (!Equals(Scale, value))
                    {
                        Scale = (double)value;
                        NativeControl.Scale = Scale;
                    }
                    break;
                case nameof(ScaleX):
                    if (!Equals(ScaleX, value))
                    {
                        ScaleX = (double)value;
                        NativeControl.ScaleX = ScaleX;
                    }
                    break;
                case nameof(ScaleY):
                    if (!Equals(ScaleY, value))
                    {
                        ScaleY = (double)value;
                        NativeControl.ScaleY = ScaleY;
                    }
                    break;
                case nameof(TranslationX):
                    if (!Equals(TranslationX, value))
                    {
                        TranslationX = (double)value;
                        NativeControl.TranslationX = TranslationX;
                    }
                    break;
                case nameof(TranslationY):
                    if (!Equals(TranslationY, value))
                    {
                        TranslationY = (double)value;
                        NativeControl.TranslationY = TranslationY;
                    }
                    break;
                case nameof(WidthRequest):
                    if (!Equals(WidthRequest, value))
                    {
                        WidthRequest = (double)value;
                        NativeControl.WidthRequest = WidthRequest;
                    }
                    break;
                case nameof(ZIndex):
                    if (!Equals(ZIndex, value))
                    {
                        ZIndex = (int)value;
                        NativeControl.ZIndex = ZIndex;
                    }
                    break;
                case nameof(OnFocused):
                    if (!Equals(OnFocused, value))
                    {
                        void NativeControlFocused(object sender, MC.FocusEventArgs e) => OnFocused.InvokeAsync(e);

                        OnFocused = (EventCallback<MC.FocusEventArgs>)value;
                        NativeControl.Focused -= NativeControlFocused;
                        NativeControl.Focused += NativeControlFocused;
                    }
                    break;
                case nameof(OnUnfocused):
                    if (!Equals(OnUnfocused, value))
                    {
                        void NativeControlUnfocused(object sender, MC.FocusEventArgs e) => OnUnfocused.InvokeAsync(e);

                        OnUnfocused = (EventCallback<MC.FocusEventArgs>)value;
                        NativeControl.Unfocused -= NativeControlUnfocused;
                        NativeControl.Unfocused += NativeControlUnfocused;
                    }
                    break;
                case nameof(OnSizeChanged):
                    if (!Equals(OnSizeChanged, value))
                    {
                        void NativeControlSizeChanged(object sender, System.EventArgs e) => OnSizeChanged.InvokeAsync(NativeControl.OnSize);

                        OnSizeChanged = (EventCallback)value;
                        NativeControl.SizeChanged -= NativeControlSizeChanged;
                        NativeControl.SizeChanged += NativeControlSizeChanged;
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
