// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Button : IHandleChildContentText
    {
        private TextSpanContainer _textSpanContainer;

        [Parameter] public RenderFragment ChildContent { get; set; }

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

        public void HandleText(int index, string text)
        {
            NativeControl.Text = (_textSpanContainer ??= new()).GetUpdatedText(index, text);
        }
    }
}
