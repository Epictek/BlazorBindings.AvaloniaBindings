// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics;
using MC = Microsoft.Maui.Controls;


namespace BlazorBindings.Maui.Elements
{
    public abstract partial class GradientBrush : Brush, IMauiContainerElementHandler
    {
#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

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

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            if (child is not MC.GradientStop gradientStopChild)
            {
                throw new ArgumentException($"GradientBrush support GradientStop child elements only, but {child?.GetType()} found instead.", nameof(child));
            }

            if (physicalSiblingIndex <= NativeControl.GradientStops.Count)
            {
                NativeControl.GradientStops.Insert(physicalSiblingIndex, gradientStopChild);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(IMauiContainerElementHandler.AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but GradientBrushControl.GradientStops.Count={NativeControl.GradientStops}");
                NativeControl.GradientStops.Add(gradientStopChild);
            }
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            if (child is not MC.GradientStop gradientStopChild)
            {
                throw new ArgumentException($"GradientBrush support GradientStop child elements only, but {child?.GetType()} found instead.", nameof(child));
            }

            return NativeControl.GradientStops.IndexOf(gradientStopChild);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            if (child is not MC.GradientStop gradientStopChild)
            {
                throw new ArgumentException($"GradientBrush support GradientStop child elements only, but {child?.GetType()} found instead.", nameof(child));
            }

            NativeControl.GradientStops.Remove(gradientStopChild);
        }
    }
}
