// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class GraphicsView : View
    {
        static GraphicsView()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public IDrawable Drawable { get; set; }

        public new MC.GraphicsView NativeControl => (MC.GraphicsView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.GraphicsView();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Drawable):
                    if (!Equals(Drawable, value))
                    {
                        Drawable = (IDrawable)value;
                        NativeControl.Drawable = Drawable;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        static partial void RegisterAdditionalHandlers();
    }
}
