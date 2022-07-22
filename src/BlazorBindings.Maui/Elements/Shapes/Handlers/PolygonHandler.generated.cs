// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MCS = Microsoft.Maui.Controls.Shapes;
using System;

namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public partial class PolygonHandler : ShapeHandler
    {
        private static readonly MCS.FillRule FillRuleDefaultValue = MCS.Polygon.FillRuleProperty.DefaultValue is MCS.FillRule value ? value : default;

        public PolygonHandler(NativeComponentRenderer renderer, MCS.Polygon polygonControl) : base(renderer, polygonControl)
        {
            PolygonControl = polygonControl ?? throw new ArgumentNullException(nameof(polygonControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MCS.Polygon PolygonControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MCS.Polygon.FillRule):
                    PolygonControl.FillRule = (MCS.FillRule)AttributeHelper.GetInt(attributeValue, (int)FillRuleDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
