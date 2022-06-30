// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellSectionHandler : ShellGroupItemHandler, IMauiContainerElementHandler
    {
        public virtual void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex)
        {
            MC.ShellContent contentToAdd = newChild switch
            {
                MC.TemplatedPage childAsTemplatedPage => childAsTemplatedPage,  // Implicit conversion
                MC.ShellContent childAsShellContent => childAsShellContent,
                null => null,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{newChild.GetType().FullName}').")
            };

            if (contentToAdd is not null)
            {
                // Ensure that there is non-null Content to avoid exceptions in Maui
                contentToAdd.Content ??= new MC.Page();
            }

            MC.ShellContent contentToRemove = GetContentForChild(previousChild);

            ContainerHelper.SetChild(ShellSectionControl.Items, contentToRemove, contentToAdd, physicalSiblingIndex);
        }

        public int GetChildIndex(MC.Element child)
        {
            var shellContent = GetContentForChild(child);
            return ShellSectionControl.Items.IndexOf(shellContent);
        }

        public override void SetParent(MC.Element parent)
        {
            if (ElementControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }

        private MC.ShellContent GetContentForChild(MC.Element child)
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
            return ShellSectionControl.Items.FirstOrDefault(content => content.Content == childAsTemplatedPage);
        }
    }
}
