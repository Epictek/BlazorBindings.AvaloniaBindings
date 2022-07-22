// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MCS = Microsoft.Maui.Controls.Shapes;
using System;

namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public partial class PolylineHandler : ShapeHandler
    {
        private static readonly MCS.FillRule FillRuleDefaultValue = MCS.Polyline.FillRuleProperty.DefaultValue is MCS.FillRule value ? value : default;

        public PolylineHandler(NativeComponentRenderer renderer, MCS.Polyline polylineControl) : base(renderer, polylineControl)
        {
            PolylineControl = polylineControl ?? throw new ArgumentNullException(nameof(polylineControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MCS.Polyline PolylineControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MCS.Polyline.FillRule):
                    PolylineControl.FillRule = (MCS.FillRule)AttributeHelper.GetInt(attributeValue, (int)FillRuleDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
