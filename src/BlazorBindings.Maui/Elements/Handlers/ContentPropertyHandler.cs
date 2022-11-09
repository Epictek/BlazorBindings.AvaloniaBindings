// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.ComponentModel;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <remarks>Experimental API, subject to change.</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ContentPropertyHandler<TElementType> : IMauiContainerElementHandler, INonChildContainerElement
    {
        private readonly Action<TElementType, MC.BindableObject> _setPropertyAction;
        private TElementType _parent;

        public ContentPropertyHandler(Action<TElementType, MC.BindableObject> setPropertyAction)
        {
            _setPropertyAction = setPropertyAction;
        }

        public void SetParent(object parentElement)
        {
            _parent = (TElementType)parentElement;
        }

        public void RemoveFromParent(object parentElement)
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            _setPropertyAction(_parent, child);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            return -1;
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            _setPropertyAction(_parent, null);
        }

        MC.BindableObject IMauiElementHandler.ElementControl => _parent as MC.BindableObject;

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
    }
}
