// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    /// <summary>
    /// Fake element handler, which is used as a root for a renderer to get native Xamarin.Forms elements
    /// from a Blazor component.
    /// </summary>
    internal class RootContainerHandler : IMauiContainerElementHandler, INonChildContainerElement
    {
        private TaskCompletionSource<MC.Element> _taskCompletionSource;

        public List<MC.Element> Elements { get; } = new List<MC.Element>();

        public Task WaitForElementAsync()
        {
            if (Elements.Count > 0)
            {
                return Task.CompletedTask;
            }

            _taskCompletionSource ??= new TaskCompletionSource<MC.Element>();
            return _taskCompletionSource.Task;
        }

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var index = Math.Min(physicalSiblingIndex, Elements.Count);
            Elements.Insert(index, child);
            _taskCompletionSource?.TrySetResult(child);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child)
        {
            Elements.Remove(child);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child)
        {
            return Elements.IndexOf(child);
        }

        // Because this is a 'fake' container element, all matters related to physical trees
        // should be no-ops.
        MC.Element IMauiElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
        void INonPhysicalChild.SetParent(object parentElement) { }
        void INonPhysicalChild.RemoveFromParent(object parentElement) { }
    }
}
