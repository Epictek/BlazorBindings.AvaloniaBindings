// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract class SelectableItemsView<T> : StructuredItemsView<T>
    {
        //Changing source at run time is valid behavior so do not make read only
#pragma warning disable CA2227 // Collection properties should be read only.
        [Parameter] public IList<object> SelectedItems { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
        [Parameter] public object SelectedItem { get; set; }
        [Parameter] public MC.SelectionMode SelectionMode { get; set; }

        [Parameter] public EventCallback<MC.SelectionChangedEventArgs> OnSelectionChanged { get; set; }
        [Parameter] public EventCallback<IList<object>> SelectedItemsChanged { get; set; }
        [Parameter] public EventCallback<object> SelectedItemChanged { get; set; }

        public new MC.SelectableItemsView NativeControl => (MC.SelectableItemsView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.SelectableItemsView();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(SelectedItems):
                    if (!Equals(SelectedItems, value))
                    {
                        SelectedItems = (IList<object>)value;
                        NativeControl.SelectedItems = SelectedItems;
                    }
                    break;
                case nameof(SelectedItem):
                    if (!Equals(SelectedItem, value))
                    {
                        SelectedItem = value;
                        NativeControl.SelectedItem = SelectedItem;
                    }
                    break;
                case nameof(SelectionMode):
                    if (!Equals(SelectionMode, value))
                    {
                        SelectionMode = (MC.SelectionMode)value;
                        NativeControl.SelectionMode = SelectionMode;
                    }
                    break;

                case nameof(OnSelectionChanged):
                    if (!Equals(OnSelectionChanged, value))
                    {
                        void NativeControlSelectionChanged(object sender, SelectionChangedEventArgs e) => OnSelectionChanged.InvokeAsync(e);

                        OnSelectionChanged = (EventCallback<MC.SelectionChangedEventArgs>)value;
                        NativeControl.SelectionChanged -= NativeControlSelectionChanged;
                        NativeControl.SelectionChanged += NativeControlSelectionChanged;
                    }
                    break;

                case nameof(SelectedItemsChanged):
                    if (!Equals(SelectedItemsChanged, value))
                    {
                        void NativeControlSelectionChanged(object sender, SelectionChangedEventArgs e) => SelectedItemsChanged.InvokeAsync(NativeControl.SelectedItems);

                        SelectedItemsChanged = (EventCallback<IList<object>>)value;
                        NativeControl.SelectionChanged -= NativeControlSelectionChanged;
                        NativeControl.SelectionChanged += NativeControlSelectionChanged;
                    }
                    break;

                case nameof(SelectedItemChanged):
                    if (!Equals(SelectedItemChanged, value))
                    {
                        void NativeControlSelectionChanged(object sender, SelectionChangedEventArgs e) => SelectedItemChanged.InvokeAsync(NativeControl.SelectedItem);

                        SelectedItemChanged = (EventCallback<object>)value;
                        NativeControl.SelectionChanged -= NativeControlSelectionChanged;
                        NativeControl.SelectionChanged += NativeControlSelectionChanged;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }
    }
}
