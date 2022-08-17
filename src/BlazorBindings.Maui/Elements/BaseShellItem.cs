// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class BaseShellItem : NavigableElement, IMauiElementHandler
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<BaseShellItem>(nameof(ItemTemplate),
                (renderer, _, component) => new DataTemplatePropertyHandler<MC.BaseShellItem, MC.BaseShellItem>(component,
                    (shellItem, dataTemplate) => MC.Shell.SetItemTemplate(shellItem, dataTemplate)));
        }

        [Parameter] public RenderFragment<MC.BaseShellItem> ItemTemplate { get; set; }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);

            RenderTreeBuilderHelper.AddDataTemplateProperty(builder, sequence++, typeof(BaseShellItem), ItemTemplate);
        }

        void IMauiElementHandler.SetParent(MC.Element parent)
        {
            if (NativeControl.Parent == null)
            {
                // The Parent should already be set
                throw new InvalidOperationException("Shouldn't need to set parent here...");
            }
        }
    }
}
