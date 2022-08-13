// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class CollectionView<T> : GroupableItemsView<T>
    {
        public new MC.CollectionView NativeControl => (MC.CollectionView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.CollectionView();
    }
}
