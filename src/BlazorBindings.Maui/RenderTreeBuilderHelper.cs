// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.DataTemplates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Runtime.CompilerServices;

namespace BlazorBindings.Maui
{
    public static class RenderTreeBuilderHelper
    {
        public static void AddContentProperty(RenderTreeBuilder builder, int sequence, Type containingType, RenderFragment content,
            [CallerArgumentExpression("content")] string propertyName = null)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(containingType);

            if (content != null)
            {
                builder.OpenRegion(sequence);

                // Content properties are handled by separate handlers, therefore rendered as separate child elements.
                // Renderer does not support elements without parent components as for now,
                // therefore adding empty parent component to workaround that.
                builder.OpenComponent<PropertyWrapperComponent>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(2, GetElementName(containingType, propertyName));
                    builder.AddContent(3, content);
                    builder.CloseElement();
                }));
                builder.CloseComponent();

                builder.CloseRegion();
            }
        }

        public static void AddDataTemplateProperty<T>(RenderTreeBuilder builder, int sequence, Type containingType, RenderFragment<T> template,
            [CallerArgumentExpression("template")] string propertyName = null)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(containingType);

            if (template != null)
            {
                builder.OpenRegion(sequence);

                builder.OpenComponent<DataTemplateItemsComponent<T>>(0);
                builder.AddAttribute(1, nameof(DataTemplateItemsComponent<T>.ElementName), GetElementName(containingType, propertyName));
                builder.AddAttribute(2, nameof(DataTemplateItemsComponent<T>.Template), template);
                builder.CloseComponent();

                builder.CloseRegion();
            }
        }

        public static void AddDataTemplateProperty(RenderTreeBuilder builder, int sequence, Type containingType, RenderFragment template,
            [CallerArgumentExpression("template")] string propertyName = null)
        {
            // There's not much of a difference between non-generic DataTemplate and ControlTemplate.
            AddControlTemplateProperty(builder, sequence, containingType, template, propertyName);
        }

        public static void AddControlTemplateProperty(RenderTreeBuilder builder, int sequence, Type containingType, RenderFragment template,
            [CallerArgumentExpression("template")] string propertyName = null)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(containingType);

            if (template != null)
            {
                builder.OpenRegion(sequence);

                builder.OpenComponent<ControlTemplateItemsComponent>(0);
                builder.AddAttribute(1, nameof(ControlTemplateItemsComponent.ElementName), GetElementName(containingType, propertyName));
                builder.AddAttribute(2, nameof(ControlTemplateItemsComponent.Template), template);
                builder.CloseComponent();

                builder.CloseRegion();
            }
        }

        private static string GetElementName(Type containingType, string propertyName)
        {
            return $"p-{containingType.FullName}.{propertyName}";
        }
    }
}
