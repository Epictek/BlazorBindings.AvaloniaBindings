// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using MCS = Microsoft.Maui.Controls.Shapes;
using System;

namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public abstract partial class ShapeHandler : BlazorBindings.Maui.Elements.Handlers.ViewHandler
    {
        private static readonly MC.Stretch AspectDefaultValue = MCS.Shape.AspectProperty.DefaultValue is MC.Stretch value ? value : default;
        private static readonly double StrokeDashOffsetDefaultValue = MCS.Shape.StrokeDashOffsetProperty.DefaultValue is double value ? value : default;
        private static readonly MCS.PenLineCap StrokeLineCapDefaultValue = MCS.Shape.StrokeLineCapProperty.DefaultValue is MCS.PenLineCap value ? value : default;
        private static readonly MCS.PenLineJoin StrokeLineJoinDefaultValue = MCS.Shape.StrokeLineJoinProperty.DefaultValue is MCS.PenLineJoin value ? value : default;
        private static readonly double StrokeMiterLimitDefaultValue = MCS.Shape.StrokeMiterLimitProperty.DefaultValue is double value ? value : default;
        private static readonly double StrokeThicknessDefaultValue = MCS.Shape.StrokeThicknessProperty.DefaultValue is double value ? value : default;

        public ShapeHandler(NativeComponentRenderer renderer, MCS.Shape shapeControl) : base(renderer, shapeControl)
        {
            ShapeControl = shapeControl ?? throw new ArgumentNullException(nameof(shapeControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MCS.Shape ShapeControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MCS.Shape.Aspect):
                    ShapeControl.Aspect = (MC.Stretch)AttributeHelper.GetInt(attributeValue, (int)AspectDefaultValue);
                    break;
                case nameof(MCS.Shape.StrokeDashOffset):
                    ShapeControl.StrokeDashOffset = AttributeHelper.StringToDouble((string)attributeValue, StrokeDashOffsetDefaultValue);
                    break;
                case nameof(MCS.Shape.StrokeLineCap):
                    ShapeControl.StrokeLineCap = (MCS.PenLineCap)AttributeHelper.GetInt(attributeValue, (int)StrokeLineCapDefaultValue);
                    break;
                case nameof(MCS.Shape.StrokeLineJoin):
                    ShapeControl.StrokeLineJoin = (MCS.PenLineJoin)AttributeHelper.GetInt(attributeValue, (int)StrokeLineJoinDefaultValue);
                    break;
                case nameof(MCS.Shape.StrokeMiterLimit):
                    ShapeControl.StrokeMiterLimit = AttributeHelper.StringToDouble((string)attributeValue, StrokeMiterLimitDefaultValue);
                    break;
                case nameof(MCS.Shape.StrokeThickness):
                    ShapeControl.StrokeThickness = AttributeHelper.StringToDouble((string)attributeValue, StrokeThicknessDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
