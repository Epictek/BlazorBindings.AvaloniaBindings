// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BlazorBindings.Core
{
    public abstract class NativeControlComponentBase : IComponent
    {
        private readonly RenderFragment _renderFragment;
        private bool _hasPendingQueuedRender;
        private RenderHandle _renderHandle;
        private Exception _eventCallbackException;

        protected IElementHandler ElementHandler { get; private set; }

        public NativeControlComponentBase()
        {
            _renderFragment = builder =>
            {
                _hasPendingQueuedRender = false;
                BuildRenderTree(builder);
            };
        }

        public virtual Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            StateHasChanged();
            return Task.CompletedTask;
        }

        protected virtual void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (_eventCallbackException != null)
            {
                var oldException = _eventCallbackException;
                _eventCallbackException = null;
                ExceptionDispatchInfo.Throw(oldException);
            }

            builder.OpenElement(0, GetType().FullName);
            RenderAttributes(new AttributesBuilder(builder));

            var childContent = GetChildContent();
            if (childContent != null)
            {
                builder.AddContent(2, childContent);
            }

            int sequence = 3;
            RenderAdditionalElementContent(builder, ref sequence);

            builder.CloseElement();
        }

        protected virtual void RenderAttributes(AttributesBuilder builder)
        {
        }

        protected virtual void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
        }

        protected void StateHasChanged()
        {
            if (_hasPendingQueuedRender)
                return;

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

        protected Task InvokeEventCallback<T>(EventCallback<T> eventCallback, T value)
        {
            return _renderHandle.Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    await eventCallback.InvokeAsync(value);
                }
                catch (Exception ex)
                {
                    // Take a look here for the reasoning
                    // https://github.com/dotnet/aspnetcore/issues/44920
                    _eventCallbackException = ex;
                    StateHasChanged();
                }
            });
        }

        protected Task InvokeEventCallback(EventCallback eventCallback)
        {
            return _renderHandle.Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    await eventCallback.InvokeAsync();
                }
                catch (Exception ex)
                {
                    // Take a look here for the reasoning
                    // https://github.com/dotnet/aspnetcore/issues/44920
                    _eventCallbackException = ex;
                    StateHasChanged();
                }
            });
        }

        protected virtual RenderFragment GetChildContent() => null;

        internal void SetElementReference(IElementHandler elementHandler)
        {
            ElementHandler = elementHandler ?? throw new ArgumentNullException(nameof(elementHandler));
        }

        void IComponent.Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
        }
    }
}
