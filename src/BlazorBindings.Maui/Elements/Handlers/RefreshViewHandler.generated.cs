// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class RefreshViewHandler : ContentViewHandler
    {
        private static readonly bool IsRefreshingDefaultValue = MC.RefreshView.IsRefreshingProperty.DefaultValue is bool value ? value : default;
        private static readonly Color RefreshColorDefaultValue = MC.RefreshView.RefreshColorProperty.DefaultValue is Color value ? value : default;

        public RefreshViewHandler(NativeComponentRenderer renderer, MC.RefreshView refreshViewControl) : base(renderer, refreshViewControl)
        {
            RefreshViewControl = refreshViewControl ?? throw new ArgumentNullException(nameof(refreshViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.RefreshView RefreshViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.RefreshView.IsRefreshing):
                    RefreshViewControl.IsRefreshing = AttributeHelper.GetBool(attributeValue, IsRefreshingDefaultValue);
                    break;
                case nameof(MC.RefreshView.RefreshColor):
                    RefreshViewControl.RefreshColor = AttributeHelper.StringToColor((string)attributeValue, RefreshColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
