// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.Maui.Controls;
using System;

namespace BlazorBindings.Maui
{
    internal class MauiBlazorBindingsElementManager : ElementManager<IMauiElementHandler>
    {
        protected override bool IsParented(IMauiElementHandler handler)
        {
            return handler.IsParented();
        }

        protected override void SetChildElement(IMauiElementHandler parentHandler, IMauiElementHandler previousChildHandler, IMauiElementHandler newChildHandler, int physicalSiblingIndex)
        {
            // Application is special-cased.
            if (parentHandler.ElementControl is Application parentAsApp)
            {
                if (newChildHandler?.ElementControl is Page childControlAsPage)
                {
                    //MainPage may already be set, but it is safe to replace it.
                    parentAsApp.MainPage = childControlAsPage;
                }
                else
                {
                    throw new InvalidOperationException($"Application MainPage must be a Page; cannot set {parentAsApp.GetType().FullName}'s MainPage to {newChildHandler?.ElementControl?.GetType().FullName}");
                }
                return;
            }

            Element elementToRemove;
            if (previousChildHandler is INonPhysicalChild nonPhysicalPreviousChild)
            {
                nonPhysicalPreviousChild.Remove();
                elementToRemove = null;
            }
            else
            {
                elementToRemove = previousChildHandler?.ElementControl;
            }

            Element elementToAdd;
            if (newChildHandler is INonPhysicalChild nonPhysicalNewChild)
            {
                // If the child is a non-child container then we shouldn't try to add it to a parent.
                // This is used in cases such as ModalContainer, which exists for the purposes of Blazor
                // markup and is not represented in the Xamarin.Forms control hierarchy.

                nonPhysicalNewChild.SetParent(parentHandler.ElementControl);
                elementToAdd = null;
            }
            else
            {
                elementToAdd = newChildHandler?.ElementControl;
            }

            if (elementToRemove is null && elementToAdd is null)
                return;

            if (parentHandler is not IMauiContainerElementHandler parent)
            {
                throw new NotSupportedException($"Handler of type '{parentHandler.GetType().FullName}' representing element type " +
                    $"'{parentHandler.ElementControl?.GetType().FullName ?? "<null>"}' doesn't support adding a child.");
            }

            parent.SetChild(elementToRemove, elementToAdd, physicalSiblingIndex);

            if (parentHandler is not INonChildContainerElement)
            {
                // Notify the child handler that its parent was set. This is needed for cases
                // where the parent/child are a conceptual relationship and not represented
                // by the Xamarin.Forms control hierarchy.
                newChildHandler?.SetParent(parentHandler.ElementControl);
            }
        }

        protected override int GetChildElementIndex(IMauiElementHandler parentHandler, IMauiElementHandler childHandler)
        {
            return parentHandler is IMauiContainerElementHandler containerHandler
                ? containerHandler.GetChildIndex(childHandler.ElementControl)
                : -1;
        }

        protected override bool IsParentOfChild(IMauiElementHandler parentHandler, IMauiElementHandler childHandler)
        {
            return childHandler.IsParentedTo(parentHandler.ElementControl);
        }
    }
}
