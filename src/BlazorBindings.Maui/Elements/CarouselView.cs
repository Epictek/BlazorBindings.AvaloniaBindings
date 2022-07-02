// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class CarouselView<T> : ItemsView<T>
    {
        private T currentItem;
        private bool _currentItemSet;

        static CarouselView()
        {
            ElementHandlerRegistry.RegisterElementHandler<CarouselView<T>>(
                renderer => new CarouselViewHandler(renderer, new MC.CarouselView()));
        }

        [Parameter] public bool? IsBounceEnabled { get; set; }
        [Parameter] public bool? IsScrollAnimated { get; set; }
        [Parameter] public bool? IsSwipeEnabled { get; set; }
        [Parameter] public MC.LinearItemsLayout ItemsLayout { get; set; }
        [Parameter] public bool? Loop { get; set; }
        [Parameter] public Thickness? PeekAreaInsets { get; set; }
        [Parameter] public int? Position { get; set; }
        [Parameter] public EventCallback<int> PositionChanged { get; set; }
        [Parameter] public EventCallback<T> CurrentItemChanged { get; set; }

        [Parameter]
        public T CurrentItem
        {
            get => currentItem;
            set
            {
                // When T is struct type (e.g. int), we need to be able to understand
                // if the property was set to a default value (e.g. 0) or it was not set at all.
                _currentItemSet = true;
                currentItem = value;
            }
        }

        public new MC.CarouselView NativeControl => (ElementHandler as CarouselViewHandler)?.CarouselViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsBounceEnabled != null)
            {
                builder.AddAttribute(nameof(IsBounceEnabled), IsBounceEnabled.Value);
            }
            if (IsScrollAnimated != null)
            {
                builder.AddAttribute(nameof(IsScrollAnimated), IsScrollAnimated.Value);
            }
            if (IsSwipeEnabled != null)
            {
                builder.AddAttribute(nameof(IsSwipeEnabled), IsSwipeEnabled.Value);
            }
            if (ItemsLayout != null)
            {
                builder.AddAttribute(nameof(ItemsLayout), AttributeHelper.ObjectToDelegate(ItemsLayout));
            }
            if (Loop != null)
            {
                builder.AddAttribute(nameof(Loop), Loop.Value);
            }
            if (PeekAreaInsets != null)
            {
                builder.AddAttribute(nameof(PeekAreaInsets), AttributeHelper.ThicknessToString(PeekAreaInsets.Value));
            }
            if (Position != null)
            {
                builder.AddAttribute(nameof(Position), Position.Value);
            }
            if (_currentItemSet && CurrentItem != null)
            {
                builder.AddAttribute(nameof(CurrentItem), AttributeHelper.ObjectToDelegate(CurrentItem)); ;
            }
            if (PositionChanged.HasDelegate)
            {
                builder.AddAttribute("onPositionChanged", EventCallback.Factory.Create<MC.PositionChangedEventArgs>(this,
                    args => PositionChanged.InvokeAsync(args.CurrentPosition)));
            }
            if (CurrentItemChanged.HasDelegate)
            {
                builder.AddAttribute("onCurrentItemChanged", EventCallback.Factory.Create<MC.CurrentItemChangedEventArgs>(this,
                    args => CurrentItemChanged.InvokeAsync((T)args.CurrentItem)));
            }
        }
    }
}
