// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BlazorBindings.Core
{
    public abstract class NativeControlComponentBase : ComponentBase
    {
        private Exception _eventCallbackException;

        public IElementHandler ElementHandler { get; private set; }

        public void SetElementReference(IElementHandler elementHandler)
        {
            ElementHandler = elementHandler ?? throw new ArgumentNullException(nameof(elementHandler));
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

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

        protected Task InvokeEventCallback<T>(EventCallback<T> eventCallback, T value)
        {
            return InvokeAsync(async () =>
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
            return InvokeAsync(async () =>
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
    }
}
