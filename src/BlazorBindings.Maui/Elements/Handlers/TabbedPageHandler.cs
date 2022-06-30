// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class TabbedPageHandler : PageHandler, IMauiContainerElementHandler
    {
        public void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex)
        {
            ContainerHelper.SetChild(TabbedPageControl.Children, previousChild, newChild, physicalSiblingIndex);
        }

        public int GetChildIndex(MC.Element child)
        {
            return TabbedPageControl.Children.IndexOf(child as MC.Page);
        }
    }
}
