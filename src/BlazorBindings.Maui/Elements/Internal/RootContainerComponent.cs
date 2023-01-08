// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Internal
{
    internal class RootContainerComponent : NativeControlComponentBase, IMauiContainerElementHandler, INonChildContainerElement
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        protected override RenderFragment GetChildContent() => ChildContent;

        public List<MC.BindableObject> Elements { get; } = new List<MC.BindableObject>();

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            var index = Math.Min(physicalSiblingIndex, Elements.Count);
            Elements.Insert(index, child);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            Elements.Remove(child);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            return Elements.IndexOf(child);
        }

        // Because this is a 'fake' container element, all matters related to physical trees
        // should be no-ops.
        MC.BindableObject IMauiElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
        void INonPhysicalChild.SetParent(object parentElement) { }
        void INonPhysicalChild.RemoveFromParent(object parentElement) { }
    }
}
