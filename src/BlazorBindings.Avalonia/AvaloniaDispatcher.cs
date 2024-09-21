// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Avalonia.Threading;

namespace BlazorBindings.Avalonia;

/// <summary>
/// Custom dispatcher for Avalonia apps to ensure all UI work is done on the UI (main) thread.
/// </summary>
internal class AvaloniaDispatcher : Microsoft.AspNetCore.Components.Dispatcher
{
    private static global::Avalonia.Threading.Dispatcher Dispatcher => global::Avalonia.Threading.Dispatcher.UIThread;

    public override bool CheckAccess()
    {
        return Dispatcher.CheckAccess();
    }

    public override async Task InvokeAsync(Action workItem)
    {
        if (!CheckAccess())
        {
            await Dispatcher.InvokeAsync(workItem);
            return;
        }
        else
        {
            workItem();
        };
    }

    public override async Task InvokeAsync(Func<Task> workItem)
    {
        if (!CheckAccess())
        {
            await Dispatcher.InvokeAsync(workItem);
        }
        else
        {
            await workItem();
        }
    }

    public override async Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
    {
        return !CheckAccess() ? await Dispatcher.InvokeAsync(workItem) : workItem();
    }

    public override async Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
    {
        return !CheckAccess() ? await Dispatcher.InvokeAsync(workItem) : await workItem();
    }
}
