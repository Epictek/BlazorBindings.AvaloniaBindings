// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class RefreshView : ContentView
    {
        static RefreshView()
        {
            ElementHandlerRegistry.RegisterElementHandler<RefreshView>(
                renderer => new RefreshViewHandler(renderer, new MC.RefreshView()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public bool? IsRefreshing { get; set; }
        [Parameter] public Color RefreshColor { get; set; }

        public new MC.RefreshView NativeControl => (ElementHandler as RefreshViewHandler)?.RefreshViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsRefreshing != null)
            {
                builder.AddAttribute(nameof(IsRefreshing), IsRefreshing.Value);
            }
            if (RefreshColor != null)
            {
                builder.AddAttribute(nameof(RefreshColor), AttributeHelper.ColorToString(RefreshColor));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
