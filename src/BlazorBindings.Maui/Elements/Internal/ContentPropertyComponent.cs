using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Internal
{
    internal class ContentPropertyComponent<TControl> : NativeControlComponentBase, IMauiContainerElementHandler, INonChildContainerElement
    {
        private TControl _parent;

        [Parameter] public Action<TControl, MC.BindableObject> SetPropertyAction { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        public void SetParent(object parentElement)
        {
            _parent = (TControl)parentElement;
        }

        public void RemoveFromParent(object parentElement)
        {
            // Because this Handler is used internally only, this method is no-op.
        }

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            SetPropertyAction(_parent, child);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            return -1;
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            SetPropertyAction(_parent, null);
        }

        MC.BindableObject IMauiElementHandler.ElementControl => _parent as MC.BindableObject;

        // Because this is a 'fake' element, all matters related to physical trees
        // should be no-ops.

        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
    }
}
