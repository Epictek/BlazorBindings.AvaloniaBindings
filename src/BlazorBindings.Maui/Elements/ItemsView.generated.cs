// <auto-generated>
//     This code was generated by a BlazorBindings.Maui component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable CA2252

namespace BlazorBindings.Maui.Elements
{
    /// <summary>
    /// A <see cref="T:Microsoft.Maui.Controls.View" /> that serves as a base class for views that contain a templated list of items.
    /// </summary>
    public abstract partial class ItemsView<T> : View
    {
        static ItemsView()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public IEnumerable<T> ItemsSource { get; set; }
        [Parameter] public MC.ItemsUpdatingScrollMode? ItemsUpdatingScrollMode { get; set; }
        [Parameter] public int? RemainingItemsThreshold { get; set; }
        [Parameter] public ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }
        [Parameter] public RenderFragment EmptyView { get; set; }
        [Parameter] public RenderFragment<T> ItemTemplate { get; set; }
        [Parameter] public RenderFragment<T> ItemTemplateSelector { get; set; }
        [Parameter] public EventCallback<MC.ScrollToRequestEventArgs> OnScrollToRequested { get; set; }
        [Parameter] public EventCallback<MC.ItemsViewScrolledEventArgs> OnScrolled { get; set; }
        [Parameter] public EventCallback OnRemainingItemsThresholdReached { get; set; }

        public new MC.ItemsView NativeControl => (MC.ItemsView)((BindableObject)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(HorizontalScrollBarVisibility):
                    if (!Equals(HorizontalScrollBarVisibility, value))
                    {
                        HorizontalScrollBarVisibility = (ScrollBarVisibility?)value;
                        NativeControl.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility ?? (ScrollBarVisibility)MC.ItemsView.HorizontalScrollBarVisibilityProperty.DefaultValue;
                    }
                    break;
                case nameof(ItemsSource):
                    if (!Equals(ItemsSource, value))
                    {
                        ItemsSource = (IEnumerable<T>)value;
                        NativeControl.ItemsSource = ItemsSource;
                    }
                    break;
                case nameof(ItemsUpdatingScrollMode):
                    if (!Equals(ItemsUpdatingScrollMode, value))
                    {
                        ItemsUpdatingScrollMode = (MC.ItemsUpdatingScrollMode?)value;
                        NativeControl.ItemsUpdatingScrollMode = ItemsUpdatingScrollMode ?? (MC.ItemsUpdatingScrollMode)MC.ItemsView.ItemsUpdatingScrollModeProperty.DefaultValue;
                    }
                    break;
                case nameof(RemainingItemsThreshold):
                    if (!Equals(RemainingItemsThreshold, value))
                    {
                        RemainingItemsThreshold = (int?)value;
                        NativeControl.RemainingItemsThreshold = RemainingItemsThreshold ?? (int)MC.ItemsView.RemainingItemsThresholdProperty.DefaultValue;
                    }
                    break;
                case nameof(VerticalScrollBarVisibility):
                    if (!Equals(VerticalScrollBarVisibility, value))
                    {
                        VerticalScrollBarVisibility = (ScrollBarVisibility?)value;
                        NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility ?? (ScrollBarVisibility)MC.ItemsView.VerticalScrollBarVisibilityProperty.DefaultValue;
                    }
                    break;
                case nameof(EmptyView):
                    EmptyView = (RenderFragment)value;
                    break;
                case nameof(ItemTemplate):
                    ItemTemplate = (RenderFragment<T>)value;
                    break;
                case nameof(ItemTemplateSelector):
                    ItemTemplateSelector = (RenderFragment<T>)value;
                    break;
                case nameof(OnScrollToRequested):
                    if (!Equals(OnScrollToRequested, value))
                    {
                        void NativeControlScrollToRequested(object sender, MC.ScrollToRequestEventArgs e) => InvokeEventCallback(OnScrollToRequested, e);

                        OnScrollToRequested = (EventCallback<MC.ScrollToRequestEventArgs>)value;
                        NativeControl.ScrollToRequested -= NativeControlScrollToRequested;
                        NativeControl.ScrollToRequested += NativeControlScrollToRequested;
                    }
                    break;
                case nameof(OnScrolled):
                    if (!Equals(OnScrolled, value))
                    {
                        void NativeControlScrolled(object sender, MC.ItemsViewScrolledEventArgs e) => InvokeEventCallback(OnScrolled, e);

                        OnScrolled = (EventCallback<MC.ItemsViewScrolledEventArgs>)value;
                        NativeControl.Scrolled -= NativeControlScrolled;
                        NativeControl.Scrolled += NativeControlScrolled;
                    }
                    break;
                case nameof(OnRemainingItemsThresholdReached):
                    if (!Equals(OnRemainingItemsThresholdReached, value))
                    {
                        void NativeControlRemainingItemsThresholdReached(object sender, EventArgs e) => InvokeEventCallback(OnRemainingItemsThresholdReached);

                        OnRemainingItemsThresholdReached = (EventCallback)value;
                        NativeControl.RemainingItemsThresholdReached -= NativeControlRemainingItemsThresholdReached;
                        NativeControl.RemainingItemsThresholdReached += NativeControlRemainingItemsThresholdReached;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty<MC.ItemsView>(builder, sequence++, EmptyView, (x, value) => x.EmptyView = (object)value);
            RenderTreeBuilderHelper.AddDataTemplateProperty<MC.ItemsView, T>(builder, sequence++, ItemTemplate, (x, template) => x.ItemTemplate = template);
            RenderTreeBuilderHelper.AddDataTemplateSelectorProperty<MC.ItemsView, T>(builder, sequence++, ItemTemplateSelector, (x, template) => x.ItemTemplate = template);
        }

        static partial void RegisterAdditionalHandlers();
    }
}
