// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TabbedPage : Page
    {
        static TabbedPage()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public Color BarBackgroundColor { get; set; }
        [Parameter] public Color BarTextColor { get; set; }
        [Parameter] public Color SelectedTabColor { get; set; }
        [Parameter] public Color UnselectedTabColor { get; set; }

        public new MC.TabbedPage NativeControl => (MC.TabbedPage)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.TabbedPage();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(BarBackgroundColor):
                    if (!Equals(BarBackgroundColor, value))
                    {
                        BarBackgroundColor = (Color)value;
                        NativeControl.BarBackgroundColor = BarBackgroundColor;
                    }
                    break;
                case nameof(BarTextColor):
                    if (!Equals(BarTextColor, value))
                    {
                        BarTextColor = (Color)value;
                        NativeControl.BarTextColor = BarTextColor;
                    }
                    break;
                case nameof(SelectedTabColor):
                    if (!Equals(SelectedTabColor, value))
                    {
                        SelectedTabColor = (Color)value;
                        NativeControl.SelectedTabColor = SelectedTabColor;
                    }
                    break;
                case nameof(UnselectedTabColor):
                    if (!Equals(UnselectedTabColor, value))
                    {
                        UnselectedTabColor = (Color)value;
                        NativeControl.UnselectedTabColor = UnselectedTabColor;
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
