// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Grid : Layout
    {
        static Grid()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double ColumnSpacing { get; set; }
        [Parameter] public double RowSpacing { get; set; }

        public new MC.Grid NativeControl => (MC.Grid)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.Grid();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(ColumnSpacing):
                    if (!Equals(ColumnSpacing, value))
                    {
                        ColumnSpacing = (double)value;
                        NativeControl.ColumnSpacing = ColumnSpacing;
                    }
                    break;
                case nameof(RowSpacing):
                    if (!Equals(RowSpacing, value))
                    {
                        RowSpacing = (double)value;
                        NativeControl.RowSpacing = RowSpacing;
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
