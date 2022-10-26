// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellSection : ShellGroupItem, IMauiContainerElementHandler
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

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            ArgumentNullException.ThrowIfNull(child);

            MC.ShellContent contentToAdd = child switch
            {
                MC.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                MC.ShellContent childAsShellContent => childAsShellContent,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' doesn't support adding a child (child type is '{child.GetType().FullName}').")
            };

            // Ensure that there is non-null Content to avoid exceptions in Xamarin.Forms
            contentToAdd.Content ??= new MC.Page();

            if (NativeControl.Items.Count >= physicalSiblingIndex)
            {
                NativeControl.Items.Insert(physicalSiblingIndex, contentToAdd);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(IMauiContainerElementHandler.AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but NativeControl.Items.Count={NativeControl.Items.Count}");
                NativeControl.Items.Add(contentToAdd);
            }
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            var shellContent = GetContentForChild(child);
            return NativeControl.Items.IndexOf(shellContent);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            ArgumentNullException.ThrowIfNull(child);

            MC.ShellContent contentToRemove = GetContentForChild(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            NativeControl.Items.Remove(contentToRemove);
        }


        private MC.ShellContent GetContentForChild(MC.BindableObject child)
        {
            return child switch
            {
                MC.TemplatedPage childAsTemplatedPage => GetContentForTemplatePage(childAsTemplatedPage),
                MC.ShellContent childAsShellContent => childAsShellContent,
                _ => null
            };
        }

        private MC.ShellContent GetContentForTemplatePage(MC.TemplatedPage childAsTemplatedPage)
        {
            return NativeControl.Items.FirstOrDefault(content => content.Content == childAsTemplatedPage);
        }
    }
}
