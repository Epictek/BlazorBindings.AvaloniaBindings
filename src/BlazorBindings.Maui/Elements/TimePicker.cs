// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel;

namespace BlazorBindings.Maui.Elements
{
    public partial class TimePicker : View
    {
        [Parameter] public EventCallback<TimeSpan> TimeChanged { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(TimeChanged))
            {
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;
                return true;
            }
            else
            {
                return base.HandleAdditionalParameter(name, value);
            }
        }

        private void NativeControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NativeControl.Time))
            {
                TimeChanged.InvokeAsync(NativeControl.Time);
            }
        }
    }
}
