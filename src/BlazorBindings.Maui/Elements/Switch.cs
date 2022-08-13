// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public partial class Switch : View
    {
        [Parameter] public EventCallback<bool> IsToggledChanged { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(IsToggledChanged))
            {
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                NativeControl.PropertyChanged += NativeControl_PropertyChanged; ;
                return true;
            }
            else
            {
                return base.HandleAdditionalParameter(name, value);
            }
        }

        private void NativeControl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NativeControl.IsToggled))
            {
                IsToggledChanged.InvokeAsync(NativeControl.IsToggled);
            }
        }
    }
}
