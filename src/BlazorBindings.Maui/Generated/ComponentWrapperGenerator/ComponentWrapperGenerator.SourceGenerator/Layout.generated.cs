// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class Layout : View
    {
        static Layout()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<Layout>(nameof(ChildContent),
                _ => new ListContentPropertyHandler<MC.Layout, IView>(x => x.Children));
            RegisterAdditionalHandlers();
        }

        [Parameter] public bool CascadeInputTransparent { get; set; }
        [Parameter] public bool IgnoreSafeArea { get; set; }
        [Parameter] public bool IsClippedToBounds { get; set; }
        [Parameter] public Thickness Padding { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        public new MC.Layout NativeControl => (MC.Layout)((Element)this).NativeControl;


        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(CascadeInputTransparent):
                    if (!Equals(CascadeInputTransparent, value))
                    {
                        CascadeInputTransparent = (bool)value;
                        NativeControl.CascadeInputTransparent = CascadeInputTransparent;
                    }
                    break;
                case nameof(IgnoreSafeArea):
                    if (!Equals(IgnoreSafeArea, value))
                    {
                        IgnoreSafeArea = (bool)value;
                        NativeControl.IgnoreSafeArea = IgnoreSafeArea;
                    }
                    break;
                case nameof(IsClippedToBounds):
                    if (!Equals(IsClippedToBounds, value))
                    {
                        IsClippedToBounds = (bool)value;
                        NativeControl.IsClippedToBounds = IsClippedToBounds;
                    }
                    break;
                case nameof(Padding):
                    if (!Equals(Padding, value))
                    {
                        Padding = (Thickness)value;
                        NativeControl.Padding = Padding;
                    }
                    break;
                case nameof(ChildContent):
                    ChildContent = (RenderFragment)value;
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Layout), ChildContent);;
        }

        static partial void RegisterAdditionalHandlers();
    }
}
