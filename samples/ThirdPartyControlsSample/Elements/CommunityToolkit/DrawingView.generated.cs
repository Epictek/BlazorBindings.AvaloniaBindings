// <auto-generated>
//     This code was generated by a BlazorBindings.Maui component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using CMV = CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.CommunityToolkit
{
    public partial class DrawingView : BlazorBindings.Maui.Elements.View
    {
        static DrawingView()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Action<ICanvas, RectF> DrawAction { get; set; }
        [Parameter] public bool? IsMultiLineModeEnabled { get; set; }
        [Parameter] public Color LineColor { get; set; }
        [Parameter] public ObservableCollection<IDrawingLine> Lines { get; set; }
        [Parameter] public float? LineWidth { get; set; }
        [Parameter] public bool? ShouldClearOnFinish { get; set; }
        [Parameter] public EventCallback<DrawingLineCompletedEventArgs> OnDrawingLineCompleted { get; set; }

        public new CMV.DrawingView NativeControl => (CMV.DrawingView)((BindableObject)this).NativeControl;

        protected override CMV.DrawingView CreateNativeElement() => new();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(DrawAction):
                    if (!Equals(DrawAction, value))
                    {
                        DrawAction = (Action<ICanvas, RectF>)value;
                        NativeControl.DrawAction = DrawAction;
                    }
                    break;
                case nameof(IsMultiLineModeEnabled):
                    if (!Equals(IsMultiLineModeEnabled, value))
                    {
                        IsMultiLineModeEnabled = (bool?)value;
                        NativeControl.IsMultiLineModeEnabled = IsMultiLineModeEnabled ?? (bool)CMV.DrawingView.IsMultiLineModeEnabledProperty.DefaultValue;
                    }
                    break;
                case nameof(LineColor):
                    if (!Equals(LineColor, value))
                    {
                        LineColor = (Color)value;
                        NativeControl.LineColor = LineColor;
                    }
                    break;
                case nameof(Lines):
                    if (!Equals(Lines, value))
                    {
                        Lines = (ObservableCollection<IDrawingLine>)value;
                        NativeControl.Lines = Lines;
                    }
                    break;
                case nameof(LineWidth):
                    if (!Equals(LineWidth, value))
                    {
                        LineWidth = (float?)value;
                        NativeControl.LineWidth = LineWidth ?? (float)CMV.DrawingView.LineWidthProperty.DefaultValue;
                    }
                    break;
                case nameof(ShouldClearOnFinish):
                    if (!Equals(ShouldClearOnFinish, value))
                    {
                        ShouldClearOnFinish = (bool?)value;
                        NativeControl.ShouldClearOnFinish = ShouldClearOnFinish ?? (bool)CMV.DrawingView.ShouldClearOnFinishProperty.DefaultValue;
                    }
                    break;
                case nameof(OnDrawingLineCompleted):
                    if (!Equals(OnDrawingLineCompleted, value))
                    {
                        void NativeControlDrawingLineCompleted(object sender, DrawingLineCompletedEventArgs e) => InvokeAsync(() => OnDrawingLineCompleted.InvokeAsync(e));

                        OnDrawingLineCompleted = (EventCallback<DrawingLineCompletedEventArgs>)value;
                        NativeControl.DrawingLineCompleted -= NativeControlDrawingLineCompleted;
                        NativeControl.DrawingLineCompleted += NativeControlDrawingLineCompleted;
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
