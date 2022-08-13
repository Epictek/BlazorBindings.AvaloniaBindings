// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class CarouselView<T> : ItemsView<T>
    {
        [Parameter] public bool IsBounceEnabled { get; set; }
        [Parameter] public bool IsScrollAnimated { get; set; }
        [Parameter] public bool IsSwipeEnabled { get; set; }
        [Parameter] public MC.LinearItemsLayout ItemsLayout { get; set; }
        [Parameter] public bool Loop { get; set; }
        [Parameter] public Thickness PeekAreaInsets { get; set; }
        [Parameter] public int Position { get; set; }
        [Parameter] public EventCallback<int> PositionChanged { get; set; }
        [Parameter] public EventCallback<T> CurrentItemChanged { get; set; }
        [Parameter] public T CurrentItem { get; set; }

        public new MC.CarouselView NativeControl => (MC.CarouselView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.CarouselView();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(IsBounceEnabled):
                    if (!Equals(IsBounceEnabled, value))
                    {
                        IsBounceEnabled = (bool)value;
                        NativeControl.IsBounceEnabled = IsBounceEnabled;
                    }
                    break;
                case nameof(IsScrollAnimated):
                    if (!Equals(IsScrollAnimated, value))
                    {
                        IsScrollAnimated = (bool)value;
                        NativeControl.IsScrollAnimated = IsScrollAnimated;
                    }
                    break;
                case nameof(IsSwipeEnabled):
                    if (!Equals(IsSwipeEnabled, value))
                    {
                        IsSwipeEnabled = (bool)value;
                        NativeControl.IsSwipeEnabled = IsSwipeEnabled;
                    }
                    break;
                case nameof(ItemsLayout):
                    if (!Equals(ItemsLayout, value))
                    {
                        ItemsLayout = (MC.LinearItemsLayout)value;
                        NativeControl.ItemsLayout = ItemsLayout;
                    }
                    break;
                case nameof(Loop):
                    if (!Equals(Loop, value))
                    {
                        Loop = (bool)value;
                        NativeControl.Loop = Loop;
                    }
                    break;
                case nameof(PeekAreaInsets):
                    if (!Equals(PeekAreaInsets, value))
                    {
                        PeekAreaInsets = (Thickness)value;
                        NativeControl.PeekAreaInsets = PeekAreaInsets;
                    }
                    break;
                case nameof(Position):
                    if (!Equals(Position, value))
                    {
                        Position = (int)value;
                        NativeControl.Position = Position;
                    }
                    break;
                case nameof(CurrentItem):
                    if (!Equals(CurrentItem, value))
                    {
                        CurrentItem = (T)value;
                        NativeControl.CurrentItem = CurrentItem;
                    }
                    break;

                case nameof(PositionChanged):
                    if (!Equals(PositionChanged, value))
                    {
                        void NativeControlPositionChanged(object sender, PositionChangedEventArgs e) => PositionChanged.InvokeAsync(NativeControl.Position);

                        PositionChanged = (EventCallback<int>)value;
                        NativeControl.PositionChanged -= NativeControlPositionChanged;
                        NativeControl.PositionChanged += NativeControlPositionChanged;
                    }
                    break;

                case nameof(CurrentItemChanged):
                    if (!Equals(CurrentItemChanged, value))
                    {
                        void NativeControlCurrentItemChanged(object sender, CurrentItemChangedEventArgs e) => CurrentItemChanged.InvokeAsync((T)NativeControl.CurrentItem);

                        CurrentItemChanged = (EventCallback<T>)value;
                        NativeControl.CurrentItemChanged -= NativeControlCurrentItemChanged;
                        NativeControl.CurrentItemChanged += NativeControlCurrentItemChanged;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }
    }
}
