// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui
{
    /// <summary>
    /// Custom dispatcher for MAUI apps to ensure all UI work is done on the UI (main) thread.
    /// </summary>
    internal class MauiDeviceDispatcher : Microsoft.AspNetCore.Components.Dispatcher
    {
        private static IDispatcher Dispatcher => Application.Current.Dispatcher;

        public override bool CheckAccess()
        {
            return !Dispatcher.IsDispatchRequired;
        }

        public override Task InvokeAsync(Action workItem)
        {
            if (Dispatcher.IsDispatchRequired)
            {
                return Dispatcher.DispatchAsync(workItem);
            }
            else
            {
                workItem();
                return Task.CompletedTask;
            };
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            return Dispatcher.IsDispatchRequired ? Dispatcher.DispatchAsync(workItem) : workItem();
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return Dispatcher.IsDispatchRequired ? Dispatcher.DispatchAsync(workItem) : Task.FromResult(workItem());
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return Dispatcher.IsDispatchRequired ? Dispatcher.DispatchAsync(workItem) : workItem();
        }
    }
}
