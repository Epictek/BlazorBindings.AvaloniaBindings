// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using M = Microsoft.Maui;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract class ItemsView<T> : View
    {
        static ItemsView()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<ItemsView<T>>(nameof(ItemTemplate),
                (renderer, _, component) => new DataTemplatePropertyHandler<MC.ItemsView, T>(component,
                    (itemsView, dataTemplate) => itemsView.ItemTemplate = dataTemplate));

            ElementHandlerRegistry.RegisterPropertyContentHandler<ItemsView<T>>(nameof(EmptyView),
                renderer => new ContentPropertyHandler<MC.ItemsView>(
                    (itemsView, valueElement) => itemsView.EmptyView = valueElement));
        }

        [Parameter] public M.ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public RenderFragment<T> ItemTemplate { get; set; }
        [Parameter] public RenderFragment EmptyView { get; set; }
        [Parameter] public IEnumerable<T> ItemsSource { get; set; }
        [Parameter] public MC.ItemsUpdatingScrollMode? ItemsUpdatingScrollMode { get; set; }
        [Parameter] public int? RemainingItemsThreshold { get; set; }
        [Parameter] public M.ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

        [Parameter] public EventCallback OnRemainingItemsThresholdReached { get; set; }
        [Parameter] public EventCallback<MC.ItemsViewScrolledEventArgs> OnScrolled { get; set; }
        [Parameter] public EventCallback<MC.ScrollToRequestEventArgs> OnScrollToRequested { get; set; }

        public new MC.ItemsView NativeControl => (MC.ItemsView)((Element)this).NativeControl;

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
                        VerticalScrollBarVisibility = (ScrollBarVisibility)value;
                        NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility ?? (ScrollBarVisibility)MC.ItemsView.VerticalScrollBarVisibilityProperty.DefaultValue;
                    }
                    break;
                case nameof(ItemTemplate):
                    ItemTemplate = (RenderFragment<T>)value;
                    break;
                case nameof(EmptyView):
                    EmptyView = (RenderFragment)value;
                    break;

                case nameof(OnRemainingItemsThresholdReached):
                    if (!Equals(OnRemainingItemsThresholdReached, value))
                    {
                        void NativeControlRemainingItemsThresholdReached(object sender, EventArgs e) => OnRemainingItemsThresholdReached.InvokeAsync();

                        OnRemainingItemsThresholdReached = (EventCallback)value;
                        NativeControl.RemainingItemsThresholdReached -= NativeControlRemainingItemsThresholdReached;
                        NativeControl.RemainingItemsThresholdReached += NativeControlRemainingItemsThresholdReached;
                    }
                    break;

                case nameof(OnScrolled):
                    if (!Equals(OnScrolled, value))
                    {
                        void NativeControlScrolled(object sender, MC.ItemsViewScrolledEventArgs e) => OnScrolled.InvokeAsync(e);

                        OnScrolled = (EventCallback<MC.ItemsViewScrolledEventArgs>)value;
                        NativeControl.Scrolled -= NativeControlScrolled;
                        NativeControl.Scrolled += NativeControlScrolled;
                    }
                    break;

                case nameof(OnScrollToRequested):
                    if (!Equals(OnScrollToRequested, value))
                    {
                        void NativeControlScrollToRequested(object sender, MC.ScrollToRequestEventArgs e) => OnScrollToRequested.InvokeAsync(e);

                        OnScrollToRequested = (EventCallback<MC.ScrollToRequestEventArgs>)value;
                        NativeControl.ScrollToRequested -= NativeControlScrollToRequested;
                        NativeControl.ScrollToRequested += NativeControlScrollToRequested;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            RenderTreeBuilderHelper.AddDataTemplateProperty(builder, sequence++, typeof(ItemsView<T>), ItemTemplate);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ItemsView<T>), EmptyView);
        }
    }
}
