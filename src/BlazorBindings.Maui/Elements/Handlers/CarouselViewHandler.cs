// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.Maui;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class CarouselViewHandler : ItemsViewHandler
    {
        private static readonly bool IsBounceEnabledDefaultValue = MC.CarouselView.IsBounceEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsScrollAnimatedDefaultValue = MC.CarouselView.IsScrollAnimatedProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsSwipeEnabledDefaultValue = MC.CarouselView.IsSwipeEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly MC.LinearItemsLayout ItemsLayoutDefaultValue = MC.CarouselView.ItemsLayoutProperty.DefaultValue is MC.LinearItemsLayout value ? value : default;
        private static readonly bool LoopDefaultValue = MC.CarouselView.LoopProperty.DefaultValue is bool value ? value : default;
        private static readonly Thickness PeekAreaInsetsDefaultValue = MC.CarouselView.PeekAreaInsetsProperty.DefaultValue is Thickness value ? value : default;
        private static readonly int PositionDefaultValue = MC.CarouselView.PositionProperty.DefaultValue is int value ? value : default;

        public CarouselViewHandler(NativeComponentRenderer renderer, MC.CarouselView carouselViewControl) : base(renderer, carouselViewControl)
        {
            CarouselViewControl = carouselViewControl ?? throw new ArgumentNullException(nameof(carouselViewControl));

            Initialize(renderer);
        }

        public MC.CarouselView CarouselViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.CarouselView.IsBounceEnabled):
                    CarouselViewControl.IsBounceEnabled = AttributeHelper.GetBool(attributeValue, IsBounceEnabledDefaultValue);
                    break;
                case nameof(MC.CarouselView.IsScrollAnimated):
                    CarouselViewControl.IsScrollAnimated = AttributeHelper.GetBool(attributeValue, IsScrollAnimatedDefaultValue);
                    break;
                case nameof(MC.CarouselView.IsSwipeEnabled):
                    CarouselViewControl.IsSwipeEnabled = AttributeHelper.GetBool(attributeValue, IsSwipeEnabledDefaultValue);
                    break;
                case nameof(MC.CarouselView.ItemsLayout):
                    CarouselViewControl.ItemsLayout = AttributeHelper.DelegateToObject(attributeValue, ItemsLayoutDefaultValue);
                    break;
                case nameof(MC.CarouselView.Loop):
                    CarouselViewControl.Loop = AttributeHelper.GetBool(attributeValue, LoopDefaultValue);
                    break;
                case nameof(MC.CarouselView.PeekAreaInsets):
                    CarouselViewControl.PeekAreaInsets = AttributeHelper.StringToThickness(attributeValue, PeekAreaInsetsDefaultValue);
                    break;
                case nameof(MC.CarouselView.Position):
                    CarouselViewControl.Position = AttributeHelper.GetInt(attributeValue, PositionDefaultValue);
                    break;
                case nameof(MC.CarouselView.CurrentItem):
                    CarouselViewControl.CurrentItem = AttributeHelper.DelegateToObject<object>(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onPositionChanged",
                setId: id => PositionChangedEventHandlerId = id,
                clearId: id => { if (PositionChangedEventHandlerId == id) { PositionChangedEventHandlerId = 0; } });
            CarouselViewControl.PositionChanged += (s, e) =>
            {
                if (PositionChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(PositionChangedEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "onCurrentItemChanged",
                setId: id => CurrentItemChangedEventHandlerId = id,
                clearId: id => { if (CurrentItemChangedEventHandlerId == id) { CurrentItemChangedEventHandlerId = 0; } });
            CarouselViewControl.CurrentItemChanged += (s, e) =>
            {
                if (CurrentItemChangedEventHandlerId != default && e.CurrentItem != null)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(CurrentItemChangedEventHandlerId, null, e));
                }
            };
        }

        public ulong PositionChangedEventHandlerId { get; set; }
        public ulong CurrentItemChangedEventHandlerId { get; set; }
    }
}
