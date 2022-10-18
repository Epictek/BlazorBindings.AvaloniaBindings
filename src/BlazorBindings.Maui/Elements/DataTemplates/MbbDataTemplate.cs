// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates
{
    internal class MbbDataTemplate<T> : MC.DataTemplate
    {
        public MbbDataTemplate(DataTemplateItemsComponent<T> dataTemplateItemsContainer)
            : base(dataTemplateItemsContainer.AddTemplateRoot)
        {
        }
    }

    internal class MbbDataTemplate : MC.DataTemplate
    {
        // There's not much of a difference between non-generic DataTemplate and ControlTemplate.
        public MbbDataTemplate(ControlTemplateItemsComponent dataTemplateItemsContainer)
            : base(dataTemplateItemsContainer.AddTemplateRoot)
        {
        }
    }
}
