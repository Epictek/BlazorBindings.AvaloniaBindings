// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FlexLayoutHandler : LayoutHandler
    {
        private static readonly XF.FlexAlignContent AlignContentDefaultValue = XF.FlexLayout.AlignContentProperty.DefaultValue is XF.FlexAlignContent value ? value : default;
        private static readonly XF.FlexAlignItems AlignItemsDefaultValue = XF.FlexLayout.AlignItemsProperty.DefaultValue is XF.FlexAlignItems value ? value : default;
        private static readonly XF.FlexDirection DirectionDefaultValue = XF.FlexLayout.DirectionProperty.DefaultValue is XF.FlexDirection value ? value : default;
        private static readonly XF.FlexJustify JustifyContentDefaultValue = XF.FlexLayout.JustifyContentProperty.DefaultValue is XF.FlexJustify value ? value : default;
        private static readonly XF.FlexPosition PositionDefaultValue = XF.FlexLayout.PositionProperty.DefaultValue is XF.FlexPosition value ? value : default;
        private static readonly XF.FlexWrap WrapDefaultValue = XF.FlexLayout.WrapProperty.DefaultValue is XF.FlexWrap value ? value : default;

        public FlexLayoutHandler(NativeComponentRenderer renderer, XF.FlexLayout flexLayoutControl) : base(renderer, flexLayoutControl)
        {
            FlexLayoutControl = flexLayoutControl ?? throw new ArgumentNullException(nameof(flexLayoutControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.FlexLayout FlexLayoutControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.FlexLayout.AlignContent):
                    FlexLayoutControl.AlignContent = (XF.FlexAlignContent)AttributeHelper.GetInt(attributeValue, (int)AlignContentDefaultValue);
                    break;
                case nameof(XF.FlexLayout.AlignItems):
                    FlexLayoutControl.AlignItems = (XF.FlexAlignItems)AttributeHelper.GetInt(attributeValue, (int)AlignItemsDefaultValue);
                    break;
                case nameof(XF.FlexLayout.Direction):
                    FlexLayoutControl.Direction = (XF.FlexDirection)AttributeHelper.GetInt(attributeValue, (int)DirectionDefaultValue);
                    break;
                case nameof(XF.FlexLayout.JustifyContent):
                    FlexLayoutControl.JustifyContent = (XF.FlexJustify)AttributeHelper.GetInt(attributeValue, (int)JustifyContentDefaultValue);
                    break;
                case nameof(XF.FlexLayout.Position):
                    FlexLayoutControl.Position = (XF.FlexPosition)AttributeHelper.GetInt(attributeValue, (int)PositionDefaultValue);
                    break;
                case nameof(XF.FlexLayout.Wrap):
                    FlexLayoutControl.Wrap = (XF.FlexWrap)AttributeHelper.GetInt(attributeValue, (int)WrapDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
