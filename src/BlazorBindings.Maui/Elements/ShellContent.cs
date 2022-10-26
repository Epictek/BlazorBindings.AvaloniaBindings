// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellContent : BaseShellItem, IMauiContainerElementHandler
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
            NativeControl.Content = child;
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            return child == NativeControl.Content ? 0 : -1;
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            if (NativeControl.Content == child)
            {
                NativeControl.Content = null;
            }
        }
    }
}
