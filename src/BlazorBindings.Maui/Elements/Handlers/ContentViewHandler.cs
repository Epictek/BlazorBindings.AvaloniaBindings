// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ContentViewHandler : TemplatedViewHandler, IMauiContainerElementHandler
    {
        public void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex)
        {
            var childAsView = newChild as MC.View;
            ContentViewControl.Content = childAsView;
        }

        public int GetChildIndex(MC.Element child)
        {
            return ContentViewControl.Content == child ? 0 : -1;
        }
    }
}
