// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class Label : View, IHandleChildContentText, IMauiContainerElementHandler
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
            if (Text != null && string.IsNullOrWhiteSpace(text))
                return;

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

        void IMauiContainerElementHandler.AddChild(MC.Element child, int physicalSiblingIndex)
        {
            var childAsSpan = child as MC.Span;

            var formattedString = NativeControl.FormattedText ??= new MC.FormattedString();
            if (physicalSiblingIndex <= formattedString.Spans.Count)
            {
                formattedString.Spans.Insert(physicalSiblingIndex, childAsSpan);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(IMauiContainerElementHandler.AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but Label.FormattedText.Spans.Count={NativeControl.FormattedText.Spans.Count}");
                formattedString.Spans.Add(childAsSpan);
            }
        }

        void IMauiContainerElementHandler.RemoveChild(MC.Element child)
        {
            var childAsSpan = child as MC.Span;
            NativeControl.FormattedText?.Spans.Remove(childAsSpan);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.Element child)
        {
            // There are two cases to consider:
            // 1. A Xamarin.Forms Label can have only 1 child (a FormattedString), so the child's index is always 0.
            // 2. But to simplify things, in MobileBlazorBindings a Label can contain a Span directly, so if the child
            //    is a Span, we have to compute its sibling index.

            return child switch
            {
                MC.Span span => NativeControl.FormattedText?.Spans.IndexOf(span) ?? -1,
                MC.FormattedString formattedString when NativeControl.FormattedText == formattedString => 0,
                _ => -1
            };
        }
    }
}
