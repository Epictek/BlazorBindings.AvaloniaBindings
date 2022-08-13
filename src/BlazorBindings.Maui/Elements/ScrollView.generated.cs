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
    public partial class ScrollView : BlazorBindings.Maui.Elements.Compatibility.Layout
    {
        static ScrollView()
        {
            RegisterAdditionalHandlers();

            ElementHandlerRegistry.RegisterPropertyContentHandler<ScrollView>(nameof(ChildContent),
                _ => new ContentPropertyHandler<MC.ScrollView>((x, value) => x.Content = (MC.View)value));
        }

        [Parameter] public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        [Parameter] public ScrollOrientation Orientation { get; set; }
        [Parameter] public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MC.ScrolledEventArgs> OnScrolled { get; set; }

        public new MC.ScrollView NativeControl => (MC.ScrollView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ScrollView();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(HorizontalScrollBarVisibility):
                    if (!Equals(HorizontalScrollBarVisibility, value))
                    {
                        HorizontalScrollBarVisibility = (ScrollBarVisibility)value;
                        NativeControl.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
                    }
                    break;
                case nameof(Orientation):
                    if (!Equals(Orientation, value))
                    {
                        Orientation = (ScrollOrientation)value;
                        NativeControl.Orientation = Orientation;
                    }
                    break;
                case nameof(VerticalScrollBarVisibility):
                    if (!Equals(VerticalScrollBarVisibility, value))
                    {
                        VerticalScrollBarVisibility = (ScrollBarVisibility)value;
                        NativeControl.VerticalScrollBarVisibility = VerticalScrollBarVisibility;
                    }
                    break;
                case nameof(ChildContent):
                    ChildContent = (RenderFragment)value;
                    break;
                case nameof(OnScrolled):
                    if (!Equals(OnScrolled, value))
                    {
                        void NativeControlScrolled(object sender, MC.ScrolledEventArgs e) => OnScrolled.InvokeAsync(e);

                        OnScrolled = (EventCallback<MC.ScrolledEventArgs>)value;
                        NativeControl.Scrolled -= NativeControlScrolled;
                        NativeControl.Scrolled += NativeControlScrolled;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(ScrollView), ChildContent);;
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
        static partial void RegisterAdditionalHandlers();
    }
}
