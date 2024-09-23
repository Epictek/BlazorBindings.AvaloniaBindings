// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Runtime.ExceptionServices;

namespace BlazorBindings.Core;

public abstract class NativeControlComponentBase : IComponent
{
    private readonly RenderFragment _renderFragment;
    private bool _hasPendingQueuedRender;
    private RenderHandle _renderHandle;
    private Exception _eventCallbackException;
    private bool _initialized;

    public NativeControlComponentBase()
    {
        _renderFragment = builder =>
        {
            _hasPendingQueuedRender = false;
            BuildRenderTree(builder);
        };
    }

    protected virtual bool ShouldRender()
        => true;

    /// <summary>
    /// Method invoked when the component is ready to start, having received its
    /// initial parameters from its parent in the render tree.
    /// </summary>
    protected virtual void OnInitialized()
    {
    }

    /// <summary>
    /// Method invoked when the component is ready to start, having received its
    /// initial parameters from its parent in the render tree.
    ///
    /// Override this method if you will perform an asynchronous operation and
    /// want the component to refresh when that operation is completed.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
    protected virtual Task OnInitializedAsync()
        => Task.CompletedTask;

    /// <summary>
    /// Method invoked when the component has received parameters from its parent in
    /// the render tree, and the incoming values have been assigned to properties.
    /// </summary>
    protected virtual void OnParametersSet()
    {
    }

    /// <summary>
    /// Method invoked when the component has received parameters from its parent in
    /// the render tree, and the incoming values have been assigned to properties.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
    protected virtual Task OnParametersSetAsync()
        => Task.CompletedTask;

    public virtual Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        //StateHasChanged();
        //return Task.CompletedTask;

        if (!_initialized)
        {
            _initialized = true;

            return RunInitAndSetParametersAsync();
        }
        else
        {
            return CallOnParametersSetAsync();
        }
    }

    private async Task RunInitAndSetParametersAsync()
    {
        OnInitialized();
        var task = OnInitializedAsync();

        if (task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Canceled)
        {
            // Call state has changed here so that we render after the sync part of OnInitAsync has run
            // and wait for it to finish before we continue. If no async work has been done yet, we want
            // to defer calling StateHasChanged up until the first bit of async code happens or until
            // the end. Additionally, we want to avoid calling StateHasChanged if no
            // async work is to be performed.
            StateHasChanged();

            try
            {
                await task;
            }
            catch // avoiding exception filters for AOT runtime support
            {
                // Ignore exceptions from task cancellations.
                // Awaiting a canceled task may produce either an OperationCanceledException (if produced as a consequence of
                // CancellationToken.ThrowIfCancellationRequested()) or a TaskCanceledException (produced as a consequence of awaiting Task.FromCanceled).
                // It's much easier to check the state of the Task (i.e. Task.IsCanceled) rather than catch two distinct exceptions.
                if (!task.IsCanceled)
                {
                    throw;
                }
            }

            // Don't call StateHasChanged here. CallOnParametersSetAsync should handle that for us.
        }

        await CallOnParametersSetAsync();
    }

    private Task CallOnParametersSetAsync()
    {
        OnParametersSet();
        var task = OnParametersSetAsync();
        // If no async work is to be performed, i.e. the task has already ran to completion
        // or was canceled by the time we got to inspect it, avoid going async and re-invoking
        // StateHasChanged at the culmination of the async work.
        var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
            task.Status != TaskStatus.Canceled;

        // We always call StateHasChanged here as we want to trigger a rerender after OnParametersSet and
        // the synchronous part of OnParametersSetAsync has run.
        StateHasChanged();

        return shouldAwaitTask ?
            CallStateHasChangedOnAsyncCompletion(task) :
            Task.CompletedTask;
    }

    private async Task CallStateHasChangedOnAsyncCompletion(Task task)
    {
        try
        {
            await task;
        }
        catch // avoiding exception filters for AOT runtime support
        {
            // Ignore exceptions from task cancellations, but don't bother issuing a state change.
            if (task.IsCanceled)
            {
                return;
            }

            throw;
        }

        StateHasChanged();
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_eventCallbackException != null)
        {
            var oldException = _eventCallbackException;
            _eventCallbackException = null;
            ExceptionDispatchInfo.Throw(oldException);
        }

        var childContent = GetChildContent();
        var sequence = 0;
        if (childContent != null)
        {
            builder.AddContent(sequence++, childContent);
        }

        RenderAdditionalElementContent(builder, ref sequence);
    }

    protected virtual void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
    {
    }

    protected void StateHasChanged()
    {
        if (_hasPendingQueuedRender)
            return;

        if (!ShouldRender())
        {
            return;
        }

        try
        {
            _renderHandle.Render(_renderFragment);
        }
        finally
        {
            _hasPendingQueuedRender = false;
        }
    }

    protected Task InvokeAsync(Action workItem) => _renderHandle.Dispatcher.InvokeAsync(workItem);

    protected Task InvokeAsync(Func<Task> workItem) => _renderHandle.Dispatcher.InvokeAsync(workItem);

    protected Task InvokeEventCallbackAsync<T>(EventCallback<T> eventCallback, T value)
    {
        return _renderHandle.Dispatcher.InvokeAsync(() => HandleExceptionAsync(eventCallback.InvokeAsync(value)));
    }

    protected Task InvokeEventCallbackAsync(EventCallback eventCallback)
    {
        return _renderHandle.Dispatcher.InvokeAsync(() => HandleExceptionAsync(eventCallback.InvokeAsync()));
    }

    protected void InvokeEventCallback<T>(EventCallback<T> eventCallback, T value)
    {
        try
        {
            AwaitVoid(InvokeEventCallbackAsync(eventCallback, value));
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    protected void InvokeEventCallback(EventCallback eventCallback)
    {
        try
        {
            AwaitVoid(InvokeEventCallbackAsync(eventCallback));
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    protected virtual RenderFragment GetChildContent() => null;

    private async Task HandleExceptionAsync(Task task)
    {
        // Take a look here for the reasoning
        // https://github.com/dotnet/aspnetcore/issues/44920

        if (task.Exception != null)
        {
            // Developer experience for async exceptions is not great in Android. Therefore I try to 
            // throw an exception without awaiting if possible.
            // https://developercommunity.visualstudio.com/t/VS-doesnt-break-properly-on-async-excep/10263624
            HandleException(task.Exception.InnerException);
        }
        else
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }

    private void HandleException(Exception ex)
    {
        _eventCallbackException = ex;
        StateHasChanged();
    }

    private async void AwaitVoid(Task task)
    {
        // This async void method is needed to handle possible exceptions in the task.
        // EventHandlers are void methods. If we need to use async-await, we have to use async void method.
        // If we simply invoke task without awaiting, the exception will simply be ignored and missed.
        // async void, otoh, raises the exception to async context (which usually makes the process crash).

        // OTOH, exceptions from async void methods are bad during debug
        // https://developercommunity.visualstudio.com/t/VS-doesnt-break-properly-on-async-excep/10263624
        // Therefore we isolate async void here, and try throwing the exceptions without async void method
        // if they happened synchronously.

        try
        {
            await task;
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    void IComponent.Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }
}
