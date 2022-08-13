// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorBindings.Maui.Elements
{
    public partial class Label : View, IHandleChildContentText
    {
        private TextSpanContainer _textSpanContainer;

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(ChildContent))
            {
                ChildContent = (RenderFragment)value;
                return true;
            }
            else
            {
                return base.HandleAdditionalParameter(name, value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        void IHandleChildContentText.HandleText(int index, string text)
        {
            if (NativeControl.FormattedText != null)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    throw new InvalidOperationException("Cannot use both string content and Spans for Label.");
                }
            }
            else
            {
                _textSpanContainer ??= new();
                NativeControl.Text = _textSpanContainer.GetUpdatedText(index, text);
            }
        }
    }
}
