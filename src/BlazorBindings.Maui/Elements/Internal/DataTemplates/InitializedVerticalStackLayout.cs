// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates
{
    internal class InitializedVerticalStackLayout : VerticalStackLayout
    {
        // ContentView was originally used here.
        // It was replaced with VerticalStackLayout as a workaround for bug
        // https://github.com/dotnet/maui/issues/5248.
        [Parameter] public new MC.VerticalStackLayout NativeControl { get; set; }

        protected override MC.VerticalStackLayout CreateNativeElement() => NativeControl;

        protected override void HandleParameter(string name, object value)
        {
            if (name == nameof(NativeControl))
            {
                NativeControl = (MC.VerticalStackLayout)value;
            }
            else
            {
                base.HandleParameter(name, value);
            }
        }
    }
}
