// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellItem : ShellGroupItem, IMauiContainerElementHandler
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
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            MC.ShellSection sectionToAdd = child switch
            {
                MC.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                MC.ShellContent childAsShellContent => childAsShellContent,  // Implicit conversion
                MC.ShellSection childAsShellSection => childAsShellSection,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' doesn't support adding a child (child type is '{child.GetType().FullName}').")
            };

            if (NativeControl.Items.Count >= physicalSiblingIndex)
            {
                NativeControl.Items.Insert(physicalSiblingIndex, sectionToAdd);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(IMauiContainerElementHandler.AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but NativeControl.Items.Count={NativeControl.Items.Count}");
                NativeControl.Items.Add(sectionToAdd);
            }
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            var section = GetSectionForElement(child);
            return NativeControl.Items.IndexOf(section);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            var sectionToRemove = GetSectionForElement(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            NativeControl.Items.Remove(sectionToRemove);
        }

        private MC.ShellSection GetSectionForElement(MC.BindableObject child)
        {
            return child switch
            {
                MC.TemplatedPage childAsTemplatedPage => GetSectionForTemplatedPage(childAsTemplatedPage),
                MC.ShellContent childAsShellContent => GetSectionForContent(childAsShellContent),
                MC.ShellSection childAsShellSection => childAsShellSection,
                _ => null
            };
        }

        private MC.ShellSection GetSectionForContent(MC.ShellContent shellContent)
        {
            return NativeControl.Items.FirstOrDefault(section => section.Items.Contains(shellContent));
        }

        private MC.ShellSection GetSectionForTemplatedPage(MC.TemplatedPage templatedPage)
        {
            return NativeControl.Items
                .FirstOrDefault(section => section.Items.Any(contect => contect.Content == templatedPage));
        }
    }
}
