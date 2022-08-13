// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public partial class Slider : View
    {
        [Parameter] public EventCallback OnDragCompleted { get; set; }
        [Parameter] public EventCallback OnDragStarted { get; set; }
        [Parameter] public EventCallback<double> ValueChanged { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(OnDragCompleted):
                    NativeControl.DragCompleted -= NativeControl_DragCompleted;
                    NativeControl.DragCompleted += NativeControl_DragCompleted;
                    return true;
                case nameof(OnDragStarted):
                    NativeControl.DragStarted -= NativeControl_DragStarted;
                    NativeControl.DragStarted += NativeControl_DragStarted;
                    return true;
                case nameof(ValueChanged):
                    NativeControl.ValueChanged -= NativeControl_ValueChanged;
                    NativeControl.ValueChanged += NativeControl_ValueChanged;
                    return true;

                default:
                    return base.HandleAdditionalParameter(name, value);

            }
        }

        private void NativeControl_ValueChanged(object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
        {
            ValueChanged.InvokeAsync(NativeControl.Value);
        }

        private void NativeControl_DragStarted(object sender, System.EventArgs e)
        {
            OnDragStarted.InvokeAsync();
        }

        private void NativeControl_DragCompleted(object sender, System.EventArgs e)
        {
            OnDragCompleted.InvokeAsync();
        }
    }
}
