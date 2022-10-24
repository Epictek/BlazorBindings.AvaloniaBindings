// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.DataTemplates;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class ControlTemplatePropertyHandler<TElementType> : IMauiContainerElementHandler, INonChildContainerElement
    {
        private readonly ControlTemplateItemsComponent _controlTemplateItemsComponent;
        private readonly Action<TElementType, ControlTemplate> _setPropertyAction;

        public ControlTemplatePropertyHandler(IComponent controlTemplateItemsComponent, Action<TElementType, ControlTemplate> setPropertyAction)
        {
            _controlTemplateItemsComponent = (ControlTemplateItemsComponent)controlTemplateItemsComponent;
            _setPropertyAction = setPropertyAction;
        }

        public void SetParent(object parentElement)
        {
            var parent = (TElementType)parentElement;
            var controlTemplate = new MbbControlTemplate(_controlTemplateItemsComponent);
            _setPropertyAction(parent, controlTemplate);
        }

        public void RemoveFromParent(object parentElement)
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex) { }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child) { }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child) => -1;

        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }

        MC.Element IMauiElementHandler.ElementControl => null;
    }
}
