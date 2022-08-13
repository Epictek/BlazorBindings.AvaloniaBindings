// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ToolbarItem : MenuItem
    {
        static ToolbarItem()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.ToolbarItemOrder Order { get; set; }
        [Parameter] public int Priority { get; set; }

        public new MC.ToolbarItem NativeControl => (MC.ToolbarItem)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ToolbarItem();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Order):
                    if (!Equals(Order, value))
                    {
                        Order = (MC.ToolbarItemOrder)value;
                        NativeControl.Order = Order;
                    }
                    break;
                case nameof(Priority):
                    if (!Equals(Priority, value))
                    {
                        Priority = (int)value;
                        NativeControl.Priority = Priority;
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
