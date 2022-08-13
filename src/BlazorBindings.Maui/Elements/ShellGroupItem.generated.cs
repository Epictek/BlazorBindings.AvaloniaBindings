// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellGroupItem : BaseShellItem
    {
        static ShellGroupItem()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.FlyoutDisplayOptions FlyoutDisplayOptions { get; set; }

        public new MC.ShellGroupItem NativeControl => (MC.ShellGroupItem)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ShellGroupItem();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(FlyoutDisplayOptions):
                    if (!Equals(FlyoutDisplayOptions, value))
                    {
                        FlyoutDisplayOptions = (MC.FlyoutDisplayOptions)value;
                        NativeControl.FlyoutDisplayOptions = FlyoutDisplayOptions;
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
