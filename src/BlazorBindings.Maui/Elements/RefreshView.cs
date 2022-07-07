// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public partial class RefreshView : ContentView
    {
        [Parameter] public EventCallback OnRefreshing { get; set; }
        [Parameter] public EventCallback<bool> IsRefreshingChanged { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (OnRefreshing.HasDelegate)
            {
                builder.AddAttribute("onrefreshing", OnRefreshing);
            }

            if (IsRefreshingChanged.HasDelegate)
            {
                builder.AddAttribute("onisrefreshingchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, e => IsRefreshingChanged.InvokeAsync((bool)e.Value)));
            }
        }
    }
}
