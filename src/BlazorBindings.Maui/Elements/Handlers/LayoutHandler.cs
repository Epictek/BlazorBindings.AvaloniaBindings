// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract partial class LayoutHandler : ViewHandler, IMauiContainerElementHandler
    {
        public virtual void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex)
        {
            ContainerHelper.SetChild(LayoutControl.Children, previousChild, newChild, physicalSiblingIndex);
        }

        public virtual int GetChildIndex(MC.Element child)
        {
            var childAsView = child as IView;
            return LayoutControl.Children.IndexOf(childAsView);
        }
    }
}
