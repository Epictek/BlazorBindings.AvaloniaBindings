// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract class GroupableItemsView<T> : SelectableItemsView<T>
    {
        // Grouping is not supported at this moment
        // [Parameter] public bool? IsGrouped { get; set; }

        public new MC.GroupableItemsView NativeControl => (MC.GroupableItemsView)((Element)this).NativeControl;
    }
}
