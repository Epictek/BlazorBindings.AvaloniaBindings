// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Collections.Generic;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class ListContentPropertyHandler<TElementType, TItemType> : IMauiContainerElementHandler, INonChildContainerElement where TItemType : class
    {
        private readonly Func<TElementType, IList<TItemType>> _listPropertyAccessor;
        private IList<TItemType> _propertyItems;
        private object _parent;

        public ListContentPropertyHandler(Func<TElementType, IList<TItemType>> listPropertyAccessor)
        {
            _listPropertyAccessor = listPropertyAccessor;
        }

        public void SetParent(object parentElement)
        {
            _parent = parentElement;
            _propertyItems = _listPropertyAccessor((TElementType)parentElement);
        }

        public void RemoveFromParent(object parentElement)
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        MC.Element IMauiElementHandler.ElementControl => _parent as MC.Element;
        object IElementHandler.TargetElement => _parent;

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex)
        {
            if (!(child is TItemType typedChild))
            {
                throw new NotSupportedException($"Cannot add item of type {child?.GetType().Name} to a {typeof(TItemType)} collection.");
            }

            _propertyItems.Insert(physicalSiblingIndex, typedChild);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child)
        {
            return _propertyItems.IndexOf(child as TItemType);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child)
        {
            _propertyItems.Remove(child as TItemType);
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
    }
}
