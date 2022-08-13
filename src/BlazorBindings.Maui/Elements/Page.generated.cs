// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Page : VisualElement
    {
        static Page()
        {
            RegisterAdditionalHandlers();

            ElementHandlerRegistry.RegisterPropertyContentHandler<Page>(nameof(MenuBarItems),
                _ => new ListContentPropertyHandler<MC.Page, MC.MenuBarItem>(x => x.MenuBarItems));
            ElementHandlerRegistry.RegisterPropertyContentHandler<Page>(nameof(ToolbarItems),
                _ => new ListContentPropertyHandler<MC.Page, MC.ToolbarItem>(x => x.ToolbarItems));
        }

        [Parameter] public MC.ImageSource BackgroundImageSource { get; set; }
        [Parameter] public MC.ImageSource IconImageSource { get; set; }
        [Parameter] public bool IsBusy { get; set; }
        [Parameter] public Thickness Padding { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment MenuBarItems { get; set; }
        [Parameter] public RenderFragment ToolbarItems { get; set; }
        [Parameter] public EventCallback OnAppearing { get; set; }
        [Parameter] public EventCallback OnDisappearing { get; set; }

        public new MC.Page NativeControl => (MC.Page)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Page();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(BackgroundImageSource):
                    if (!Equals(BackgroundImageSource, value))
                    {
                        BackgroundImageSource = (MC.ImageSource)value;
                        NativeControl.BackgroundImageSource = BackgroundImageSource;
                    }
                    break;
                case nameof(IconImageSource):
                    if (!Equals(IconImageSource, value))
                    {
                        IconImageSource = (MC.ImageSource)value;
                        NativeControl.IconImageSource = IconImageSource;
                    }
                    break;
                case nameof(IsBusy):
                    if (!Equals(IsBusy, value))
                    {
                        IsBusy = (bool)value;
                        NativeControl.IsBusy = IsBusy;
                    }
                    break;
                case nameof(Padding):
                    if (!Equals(Padding, value))
                    {
                        Padding = (Thickness)value;
                        NativeControl.Padding = Padding;
                    }
                    break;
                case nameof(Title):
                    if (!Equals(Title, value))
                    {
                        Title = (string)value;
                        NativeControl.Title = Title;
                    }
                    break;
                case nameof(MenuBarItems):
                    MenuBarItems = (RenderFragment)value;
                    break;
                case nameof(ToolbarItems):
                    ToolbarItems = (RenderFragment)value;
                    break;
                case nameof(OnAppearing):
                    if (!Equals(OnAppearing, value))
                    {
                        void NativeControlAppearing(object sender, EventArgs e) => OnAppearing.InvokeAsync();

                        OnAppearing = (EventCallback)value;
                        NativeControl.Appearing -= NativeControlAppearing;
                        NativeControl.Appearing += NativeControlAppearing;
                    }
                    break;
                case nameof(OnDisappearing):
                    if (!Equals(OnDisappearing, value))
                    {
                        void NativeControlDisappearing(object sender, EventArgs e) => OnDisappearing.InvokeAsync();

                        OnDisappearing = (EventCallback)value;
                        NativeControl.Disappearing -= NativeControlDisappearing;
                        NativeControl.Disappearing += NativeControlDisappearing;
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
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Page), MenuBarItems);
            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(Page), ToolbarItems);;
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
        static partial void RegisterAdditionalHandlers();
    }
}
