// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class VisualElement : NavigableElement
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<VisualElement>(nameof(Background),
                _ => new ContentPropertyHandler<MC.VisualElement>((visualElement, contentElement) => visualElement.Background = (MC.Brush)contentElement));
        }

        [Parameter] public RenderFragment Background { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(Background))
            {
                Background = (RenderFragment)value;
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(VisualElement), Background);
        }
    }
}
