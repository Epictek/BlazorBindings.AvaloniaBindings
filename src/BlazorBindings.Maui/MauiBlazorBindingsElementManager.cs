// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui
{
    internal class MauiBlazorBindingsElementManager : ElementManager<IMauiElementHandler>
    {
        protected override void AddChildElement(
            IMauiElementHandler parentHandler,
            IMauiElementHandler childHandler,
            int physicalSiblingIndex)
        {
            if (childHandler is INonPhysicalChild nonPhysicalChild)
            {
                // If the child is a non-child container then we shouldn't try to add it to a parent.
                // This is used in cases such as ModalContainer, which exists for the purposes of Blazor
                // markup and is not represented in the Xamarin.Forms control hierarchy.

                nonPhysicalChild.SetParent(parentHandler.ElementControl);
                return;
            }

            if (parentHandler is not IMauiContainerElementHandler parent)
            {
                throw new NotSupportedException($"Handler of type '{parentHandler.GetType().FullName}' representing element type " +
                    $"'{parentHandler.ElementControl?.GetType().FullName ?? "<null>"}' doesn't support adding a child " +
                    $"(child type is '{childHandler.ElementControl?.GetType().FullName}').");
            }

            parent.AddChild(childHandler.ElementControl, physicalSiblingIndex);
        }

        protected override int GetChildElementIndex(IMauiElementHandler parentHandler, IMauiElementHandler childHandler)
        {
            return parentHandler is IMauiContainerElementHandler containerHandler
                ? containerHandler.GetChildIndex(childHandler.ElementControl)
                : -1;
        }

        protected override void RemoveChildElement(IMauiElementHandler parentHandler, IMauiElementHandler childHandler)
        {
            if (childHandler is INonPhysicalChild nonPhysicalChild)
            {
                nonPhysicalChild.Remove();
            }
            else if (parentHandler is IMauiContainerElementHandler parent)
            {
                parent.RemoveChild(childHandler.ElementControl);
            }
            else
            {
                throw new NotSupportedException($"Handler of type '{parentHandler.GetType().FullName}' representing element type " +
                    $"'{parentHandler.ElementControl?.GetType().FullName ?? "<null>"}' doesn't support removing a child " +
                    $"(child type is '{childHandler.ElementControl?.GetType().FullName}').");
            }
        }
    }
}
