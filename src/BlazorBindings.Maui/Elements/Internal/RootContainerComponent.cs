// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Maui.Elements.Internal;

internal class RootContainerComponent : NativeControlComponentBase, INonPhysicalParent
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public EventCallback<object> OnElementAdded { get; set; }

    protected override RenderFragment GetChildContent() => ChildContent;

    public List<object> Elements { get; } = new();

    void IContainerElementHandler.AddChild(object child, int physicalSiblingIndex)
    {
        var index = Math.Min(physicalSiblingIndex, Elements.Count);
        Elements.Insert(index, child);
        OnElementAdded.InvokeAsync(child);
    }

    void IContainerElementHandler.RemoveChild(object child, int physicalSiblingIndex)
    {
        Elements.Remove(child);
    }
}
