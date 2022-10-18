// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class TabbedPage : Page, IMauiContainerElementHandler
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(ChildContent))
            {
                ChildContent = (RenderFragment)value;
                return true;
            }
            else
            {
                return base.HandleAdditionalParameter(name, value);
            }
        }

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsPage = child as MC.Page;

            if (physicalSiblingIndex <= NativeControl.Children.Count)
            {
                NativeControl.Children.Insert(physicalSiblingIndex, childAsPage);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(NativeControl)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but TabbedPageControl.Children.Count={NativeControl.Children.Count}");
                NativeControl.Children.Add(childAsPage);
            }
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child)
        {
            return NativeControl.Children.IndexOf(child as MC.Page);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child)
        {
            NativeControl.Children.Remove(child as MC.Page);
        }
    }
}
