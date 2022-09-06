// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Layouts;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class FlexLayout : Layout
    {
        static FlexLayout()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public FlexAlignContent AlignContent { get; set; }
        [Parameter] public FlexAlignItems AlignItems { get; set; }
        [Parameter] public FlexDirection Direction { get; set; }
        [Parameter] public FlexJustify JustifyContent { get; set; }
        [Parameter] public FlexPosition Position { get; set; }
        [Parameter] public FlexWrap Wrap { get; set; }

        public new MC.FlexLayout NativeControl => (MC.FlexLayout)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.FlexLayout();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(AlignContent):
                    if (!Equals(AlignContent, value))
                    {
                        AlignContent = (FlexAlignContent)value;
                        NativeControl.AlignContent = AlignContent;
                    }
                    break;
                case nameof(AlignItems):
                    if (!Equals(AlignItems, value))
                    {
                        AlignItems = (FlexAlignItems)value;
                        NativeControl.AlignItems = AlignItems;
                    }
                    break;
                case nameof(Direction):
                    if (!Equals(Direction, value))
                    {
                        Direction = (FlexDirection)value;
                        NativeControl.Direction = Direction;
                    }
                    break;
                case nameof(JustifyContent):
                    if (!Equals(JustifyContent, value))
                    {
                        JustifyContent = (FlexJustify)value;
                        NativeControl.JustifyContent = JustifyContent;
                    }
                    break;
                case nameof(Position):
                    if (!Equals(Position, value))
                    {
                        Position = (FlexPosition)value;
                        NativeControl.Position = Position;
                    }
                    break;
                case nameof(Wrap):
                    if (!Equals(Wrap, value))
                    {
                        Wrap = (FlexWrap)value;
                        NativeControl.Wrap = Wrap;
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
