// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates;

// While this element _is_ physical, we don't need it to be added to parents,
// as they should track provided NativeControl instead.
internal class InitializedContentView : ContentView, INonPhysicalHandler
{
    [Parameter] public new MC.ContentView NativeControl { get; set; }

    protected override MC.ContentView CreateNativeElement() => NativeControl;

    protected override void HandleParameter(string name, object value)
    {
        if (name == nameof(NativeControl))
        {
            NativeControl = (MC.ContentView)value;
        }
        else
        {
            base.HandleParameter(name, value);
        }
    }
}
