// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public abstract partial class GradientBrushHandler : BrushHandler, IMauiContainerElementHandler
    {
        public void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex)
        {
            ContainerHelper.SetChild(GradientBrushControl.GradientStops, previousChild, newChild, physicalSiblingIndex);
        }

        public int GetChildIndex(MC.Element child)
        {
            if (!(child is MC.GradientStop gradientStopChild))
            {
                throw new ArgumentException($"GradientBrush support GradientStop child elements only, but {child?.GetType()} found instead.", nameof(child));
            }

            return GradientBrushControl.GradientStops.IndexOf(gradientStopChild);
        }
    }
}