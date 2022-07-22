// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class RadialGradientBrushHandler : GradientBrushHandler
    {
        private static readonly Point CenterDefaultValue = MC.RadialGradientBrush.CenterProperty.DefaultValue is Point value ? value : default;
        private static readonly double RadiusDefaultValue = MC.RadialGradientBrush.RadiusProperty.DefaultValue is double value ? value : default;

        public RadialGradientBrushHandler(NativeComponentRenderer renderer, MC.RadialGradientBrush radialGradientBrushControl) : base(renderer, radialGradientBrushControl)
        {
            RadialGradientBrushControl = radialGradientBrushControl ?? throw new ArgumentNullException(nameof(radialGradientBrushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.RadialGradientBrush RadialGradientBrushControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.RadialGradientBrush.Center):
                    RadialGradientBrushControl.Center = AttributeHelper.StringToPoint(attributeValue, CenterDefaultValue);
                    break;
                case nameof(MC.RadialGradientBrush.Radius):
                    RadialGradientBrushControl.Radius = AttributeHelper.StringToDouble((string)attributeValue, RadiusDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
