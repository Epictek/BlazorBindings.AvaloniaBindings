// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui
{
    public interface IMauiContainerElementHandler : IMauiElementHandler
    {
        void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex);
        int GetChildIndex(MC.Element child);
    }
}
