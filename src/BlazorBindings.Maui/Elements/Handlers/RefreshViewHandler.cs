// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class RefreshViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onrefreshing",
                setId: id => RefreshingEventHandlerId = id,
                clearId: id => { if (RefreshingEventHandlerId == id) { RefreshingEventHandlerId = 0; } });
            RefreshViewControl.Refreshing += (s, e) =>
            {
                renderer.Dispatcher.InvokeAsync(async () =>
                {
                    // We wait a bit in case event handler wasn't set so far
                    // (to handle when RefreshView has IsRefreshing=true initial value).
                    await Task.Delay(1);

                    if (RefreshingEventHandlerId != default)
                    {
                        await renderer.DispatchEventAsync(RefreshingEventHandlerId, null, e);
                        RefreshViewControl.IsRefreshing = false;
                    }
                });
            };

            ConfigureEvent(
                eventName: "onisrefreshingchanged",
                setId: id => IsRefreshingChangedEventHandlerId = id,
                clearId: id => { if (IsRefreshingChangedEventHandlerId == id) { IsRefreshingChangedEventHandlerId = 0; } });
            RefreshViewControl.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(RefreshViewControl.IsRefreshing) && IsRefreshingChangedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(IsRefreshingChangedEventHandlerId, null,
                        new ChangeEventArgs { Value = RefreshViewControl.IsRefreshing }));
                }
            };
        }

        public ulong RefreshingEventHandlerId { get; set; }
        public ulong IsRefreshingChangedEventHandlerId { get; set; }
    }
}
