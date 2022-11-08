// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;

namespace BlazorBindings.Maui.Elements
{
    public partial class RefreshView : ContentView
    {
        [Parameter] public EventCallback OnRefreshing { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(OnRefreshing))
            {
                if (!Equals(OnRefreshing, value))
                {
                    async void NativeControlRefreshing(object sender, EventArgs e)
                    {
                        try
                        {
                            await InvokeEventCallback(OnRefreshing);
                        }
                        finally
                        {
                            NativeControl.IsRefreshing = false;
                        }
                    }

                    OnRefreshing = (EventCallback)value;
                    NativeControl.Refreshing -= NativeControlRefreshing;
                    NativeControl.Refreshing += NativeControlRefreshing;
                }
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }
    }
}
