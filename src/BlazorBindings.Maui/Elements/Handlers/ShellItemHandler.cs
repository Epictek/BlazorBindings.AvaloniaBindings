// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellItemHandler : ShellGroupItemHandler, IMauiContainerElementHandler
    {
        public void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex)
        {
            MC.ShellSection sectionToAdd = newChild switch
            {
                MC.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                MC.ShellContent childAsShellContent => childAsShellContent,  // Implicit conversion
                MC.ShellSection childAsShellSection => childAsShellSection,
                null => null,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{newChild.GetType().FullName}').")
            };

            MC.ShellSection sectionToRemove = GetSectionForElement(previousChild);

            ContainerHelper.SetChild(ShellItemControl.Items, sectionToRemove, sectionToAdd, physicalSiblingIndex);
        }

        public override void SetParent(MC.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }

        public int GetChildIndex(MC.Element child)
        {
            var section = GetSectionForElement(child);
            return ShellItemControl.Items.IndexOf(section);
        }

        private MC.ShellSection GetSectionForElement(MC.Element child)
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
            return ShellItemControl.Items.FirstOrDefault(section => section.Items.Contains(shellContent));
        }

        private MC.ShellSection GetSectionForTemplatedPage(MC.TemplatedPage templatedPage)
        {
            return ShellItemControl.Items
                .FirstOrDefault(section => section.Items.Any(contect => contect.Content == templatedPage));
        }
    }
}
