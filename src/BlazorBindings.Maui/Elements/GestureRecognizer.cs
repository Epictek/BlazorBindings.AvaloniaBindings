// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements;

public partial class GestureRecognizer : INonPhysicalChild
{
    void INonPhysicalChild.RemoveFromParent(object parentElement)
    {
        switch (parentElement)
        {
            case MC.View view:
                view.GestureRecognizers.Remove(NativeControl);
                break;
            case MC.GestureElement gestureElement:
                gestureElement.GestureRecognizers.Remove(NativeControl);
                break;
            default:
                throw new InvalidOperationException($"Gesture of type {NativeControl.GetType().Name} can't be removed from parent of type {parentElement.GetType().FullName}.");
        }
    }

    void INonPhysicalChild.SetParent(object parentElement)
    {
        switch (parentElement)
        {
            case MC.View view:
                view.GestureRecognizers.Add(NativeControl);
                break;
            case MC.GestureElement gestureElement:
                gestureElement.GestureRecognizers.Add(NativeControl);
                break;
            default:
                throw new InvalidOperationException($"Gesture of type {NativeControl.GetType().Name} can't be added to parent of type {parentElement.GetType().FullName}.");
        }
    }
}
