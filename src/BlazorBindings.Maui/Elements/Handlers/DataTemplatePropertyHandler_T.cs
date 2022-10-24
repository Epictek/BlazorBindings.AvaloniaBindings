// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.DataTemplates;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using IComponent = Microsoft.AspNetCore.Components.IComponent;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <remarks>Experimental API, subject to change.</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class DataTemplatePropertyHandler<TElementType, TItemType> : IMauiContainerElementHandler, INonChildContainerElement
    {
        private readonly DataTemplateItemsComponent<TItemType> _dataTemplateItemsComponent;
        private readonly Action<TElementType, DataTemplate> _setPropertyAction;

        public DataTemplatePropertyHandler(IComponent dataTemplateItemsComponent, Action<TElementType, DataTemplate> setPropertyAction)
        {
            _dataTemplateItemsComponent = (DataTemplateItemsComponent<TItemType>)dataTemplateItemsComponent;
            _setPropertyAction = setPropertyAction;
        }

        public void SetParent(object parentElement)
        {
            var parent = (TElementType)parentElement;
            var dataTemplate = new MbbDataTemplate<TItemType>(_dataTemplateItemsComponent);
            _setPropertyAction(parent, dataTemplate);
        }

        public void RemoveFromParent(object parentElement)
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex) { }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child) { }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child) => -1;

        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }

        MC.BindableObject IMauiElementHandler.ElementControl => null;
    }
}
