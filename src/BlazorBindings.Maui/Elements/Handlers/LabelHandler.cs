// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class LabelHandler : ViewHandler, IMauiContainerElementHandler, IHandleChildContentText
    {
        private readonly TextSpanContainer _textSpanContainer = new();

        public void SetChild(MC.Element previousChild, MC.Element newChild, int physicalSiblingIndex)
        {
            var formattedString = GetFormattedString();
            ContainerHelper.SetChild(formattedString.Spans, previousChild, newChild, physicalSiblingIndex);
        }

        public int GetChildIndex(MC.Element child)
        {
            // There are two cases to consider:
            // 1. A Xamarin.Forms Label can have only 1 child (a FormattedString), so the child's index is always 0.
            // 2. But to simplify things, in MobileBlazorBindings a Label can contain a Span directly, so if the child
            //    is a Span, we have to compute its sibling index.

            return child switch
            {
                MC.Span span => LabelControl.FormattedText?.Spans.IndexOf(span) ?? -1,
                MC.FormattedString formattedString when LabelControl.FormattedText == formattedString => 0,
                _ => -1
            };
        }

        public void HandleText(int index, string text)
        {
            if (LabelControl.FormattedText != null)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    throw new InvalidOperationException("Cannot use both string content and Spans for Label.");
                }
            }
            else
            {
                LabelControl.Text = _textSpanContainer.GetUpdatedText(index, text);
            }
        }

        private MC.FormattedString GetFormattedString()
        {
            if (LabelControl.FormattedText == null)
            {
                LabelControl.FormattedText = new MC.FormattedString();
            }
            return LabelControl.FormattedText;
        }
    }
}
