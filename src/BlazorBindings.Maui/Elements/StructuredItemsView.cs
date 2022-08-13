// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract class StructuredItemsView<T> : ItemsView<T>
    {
        static StructuredItemsView()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<StructuredItemsView<T>>(nameof(Header),
                _ => new ContentPropertyHandler<MC.StructuredItemsView>((itemsView, valueElement) => itemsView.Header = valueElement));

            ElementHandlerRegistry.RegisterPropertyContentHandler<StructuredItemsView<T>>(nameof(Footer),
                _ => new ContentPropertyHandler<MC.StructuredItemsView>((itemsView, valueElement) => itemsView.Footer = valueElement));
        }

        [Parameter] public MC.ItemSizingStrategy ItemSizingStrategy { get; set; }
        [Parameter] public MC.IItemsLayout ItemsLayout { get; set; }
        [Parameter] public RenderFragment Header { get; set; }
        [Parameter] public RenderFragment Footer { get; set; }

        public new MC.StructuredItemsView NativeControl => (MC.StructuredItemsView)((Element)this).NativeControl;

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(ItemSizingStrategy):
                    if (!ItemSizingStrategy.Equals(value))
                    {
                        ItemSizingStrategy = (MC.ItemSizingStrategy)value;
                        NativeControl.ItemSizingStrategy = ItemSizingStrategy;
                    }
                    break;
                case nameof(ItemsLayout):
                    if (!Equals(ItemsLayout, value))
                    {
                        ItemsLayout = (MC.IItemsLayout)value;
                        NativeControl.ItemsLayout = ItemsLayout;
                    }
                    break;
                case nameof(Header):
                    Header = (RenderFragment)value;
                    break;
                case nameof(Footer):
                    Footer = (RenderFragment)value;
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);

            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(StructuredItemsView<T>), Header);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(StructuredItemsView<T>), Footer);
        }
    }
}
