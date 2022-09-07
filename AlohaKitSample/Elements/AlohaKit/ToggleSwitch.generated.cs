// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using AC = AlohaKit.Controls;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.AlohaKit
{
    public partial class ToggleSwitch : BlazorBindings.Maui.Elements.GraphicsView
    {
        static ToggleSwitch()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public bool HasShadow { get; set; }
        [Parameter] public bool IsOn { get; set; }
        [Parameter] public AC.ToggleSwitchDrawable ToggleSwitchDrawable { get; set; }

        public new AC.ToggleSwitch NativeControl => (AC.ToggleSwitch)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new AC.ToggleSwitch();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(HasShadow):
                    if (!Equals(HasShadow, value))
                    {
                        HasShadow = (bool)value;
                        NativeControl.HasShadow = HasShadow;
                    }
                    break;
                case nameof(IsOn):
                    if (!Equals(IsOn, value))
                    {
                        IsOn = (bool)value;
                        NativeControl.IsOn = IsOn;
                    }
                    break;
                case nameof(ToggleSwitchDrawable):
                    if (!Equals(ToggleSwitchDrawable, value))
                    {
                        ToggleSwitchDrawable = (AC.ToggleSwitchDrawable)value;
                        NativeControl.ToggleSwitchDrawable = ToggleSwitchDrawable;
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
