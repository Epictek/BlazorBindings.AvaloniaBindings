// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MCS = Microsoft.Maui.Controls.Shapes;
using System;

namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public partial class LineHandler : ShapeHandler
    {
        private static readonly double X1DefaultValue = MCS.Line.X1Property.DefaultValue is double value ? value : default;
        private static readonly double X2DefaultValue = MCS.Line.X2Property.DefaultValue is double value ? value : default;
        private static readonly double Y1DefaultValue = MCS.Line.Y1Property.DefaultValue is double value ? value : default;
        private static readonly double Y2DefaultValue = MCS.Line.Y2Property.DefaultValue is double value ? value : default;

        public LineHandler(NativeComponentRenderer renderer, MCS.Line lineControl) : base(renderer, lineControl)
        {
            LineControl = lineControl ?? throw new ArgumentNullException(nameof(lineControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MCS.Line LineControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MCS.Line.X1):
                    LineControl.X1 = AttributeHelper.StringToDouble((string)attributeValue, X1DefaultValue);
                    break;
                case nameof(MCS.Line.X2):
                    LineControl.X2 = AttributeHelper.StringToDouble((string)attributeValue, X2DefaultValue);
                    break;
                case nameof(MCS.Line.Y1):
                    LineControl.Y1 = AttributeHelper.StringToDouble((string)attributeValue, Y1DefaultValue);
                    break;
                case nameof(MCS.Line.Y2):
                    LineControl.Y2 = AttributeHelper.StringToDouble((string)attributeValue, Y2DefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
