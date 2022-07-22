// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MCS = Microsoft.Maui.Controls.Shapes;
using System;

namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public partial class RectangleHandler : ShapeHandler
    {
        private static readonly double RadiusXDefaultValue = MCS.Rectangle.RadiusXProperty.DefaultValue is double value ? value : default;
        private static readonly double RadiusYDefaultValue = MCS.Rectangle.RadiusYProperty.DefaultValue is double value ? value : default;

        public RectangleHandler(NativeComponentRenderer renderer, MCS.Rectangle rectangleControl) : base(renderer, rectangleControl)
        {
            RectangleControl = rectangleControl ?? throw new ArgumentNullException(nameof(rectangleControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MCS.Rectangle RectangleControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MCS.Rectangle.RadiusX):
                    RectangleControl.RadiusX = AttributeHelper.StringToDouble((string)attributeValue, RadiusXDefaultValue);
                    break;
                case nameof(MCS.Rectangle.RadiusY):
                    RectangleControl.RadiusY = AttributeHelper.StringToDouble((string)attributeValue, RadiusYDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
