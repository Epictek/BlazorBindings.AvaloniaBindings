// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;
using System;
using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class RadioButtonHandler : TemplatedViewHandler
    {
        private static readonly XF.Color BorderColorDefaultValue = XF.RadioButton.BorderColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly double BorderWidthDefaultValue = XF.RadioButton.BorderWidthProperty.DefaultValue is double value ? value : default;
        private static readonly double CharacterSpacingDefaultValue = XF.RadioButton.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly int CornerRadiusDefaultValue = XF.RadioButton.CornerRadiusProperty.DefaultValue is int value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.RadioButton.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.RadioButton.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.RadioButton.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly string GroupNameDefaultValue = XF.RadioButton.GroupNameProperty.DefaultValue is string value ? value : default;
        private static readonly bool IsCheckedDefaultValue = XF.RadioButton.IsCheckedProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.Color TextColorDefaultValue = XF.RadioButton.TextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.TextTransform TextTransformDefaultValue = XF.RadioButton.TextTransformProperty.DefaultValue is XF.TextTransform value ? value : default;

        public RadioButtonHandler(NativeComponentRenderer renderer, XF.RadioButton radioButtonControl) : base(renderer, radioButtonControl)
        {
            RadioButtonControl = radioButtonControl ?? throw new ArgumentNullException(nameof(radioButtonControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.RadioButton RadioButtonControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.RadioButton.BorderColor):
                    RadioButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue, BorderColorDefaultValue);
                    break;
                case nameof(XF.RadioButton.BorderWidth):
                    RadioButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, BorderWidthDefaultValue);
                    break;
                case nameof(XF.RadioButton.CharacterSpacing):
                    RadioButtonControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(XF.RadioButton.CornerRadius):
                    RadioButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, CornerRadiusDefaultValue);
                    break;
                case nameof(XF.RadioButton.FontAttributes):
                    RadioButtonControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.RadioButton.FontFamily):
                    RadioButtonControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.RadioButton.FontSize):
                    RadioButtonControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.RadioButton.GroupName):
                    RadioButtonControl.GroupName = (string)attributeValue ?? GroupNameDefaultValue;
                    break;
                case nameof(XF.RadioButton.IsChecked):
                    RadioButtonControl.IsChecked = AttributeHelper.GetBool(attributeValue, IsCheckedDefaultValue);
                    break;
                case nameof(XF.RadioButton.TextColor):
                    RadioButtonControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(XF.RadioButton.TextTransform):
                    RadioButtonControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
