// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class NavigableElement : Element
    {
        static NavigableElement()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public string @class { get; set; }
        [Parameter] public string StyleClass { get; set; }

        public new MC.NavigableElement NativeControl => (MC.NavigableElement)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(@class):
                    if (!Equals(@class, value))
                    {
                        @class = (string)value;
                        NativeControl.@class = AttributeHelper.GetStringList(@class);
                    }
                    break;
                case nameof(StyleClass):
                    if (!Equals(StyleClass, value))
                    {
                        StyleClass = (string)value;
                        NativeControl.StyleClass = AttributeHelper.GetStringList(StyleClass);
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
        static partial void RegisterAdditionalHandlers();
    }
}
