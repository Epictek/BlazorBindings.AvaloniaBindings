// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <summary>
    /// Fake element handler, which is used as a root for a renderer to get native Xamarin.Forms elements
    /// from a Blazor component.
    /// </summary>
    /// <remarks>Experimental API, subject to change.</remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class RootContainerHandler : IMauiContainerElementHandler, INonChildContainerElement
    {
        private TaskCompletionSource<MC.BindableObject> _taskCompletionSource;

        public List<MC.BindableObject> Elements { get; } = new List<MC.BindableObject>();

        public Task WaitForElementAsync()
        {
            if (Elements.Count > 0)
            {
                return Task.CompletedTask;
            }

            _taskCompletionSource ??= new TaskCompletionSource<MC.BindableObject>();
            return _taskCompletionSource.Task;
        }

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            var index = Math.Min(physicalSiblingIndex, Elements.Count);
            Elements.Insert(index, child);
            _taskCompletionSource?.TrySetResult(child);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            Elements.Remove(child);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            return Elements.IndexOf(child);
        }

        // Because this is a 'fake' container element, all matters related to physical trees
        // should be no-ops.
        MC.BindableObject IMauiElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
        void INonPhysicalChild.SetParent(object parentElement) { }
        void INonPhysicalChild.RemoveFromParent(object parentElement) { }
    }
}
