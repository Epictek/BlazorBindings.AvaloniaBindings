// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui
{
    public interface IMauiContainerElementHandler : IMauiElementHandler
    {
        void AddChild(MC.BindableObject child, int physicalSiblingIndex);
        void RemoveChild(MC.BindableObject child);
        int GetChildIndex(MC.BindableObject child);
    }
}
