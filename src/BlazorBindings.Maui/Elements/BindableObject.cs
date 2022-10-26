// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract class BindableObject : NativeControlComponentBase, IMauiElementHandler
    {
        private MC.BindableObject _nativeControl;

        public MC.BindableObject NativeControl => _nativeControl ??= CreateNativeElement();

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                HandleParameter(parameterValue.Name, parameterValue.Value);
            }

            return base.SetParametersAsync(ParameterView.Empty);
        }

        protected sealed override void RenderAttributes(AttributesBuilder builder)
        {
            // Since we set attributes directly in SetParametersAsync, this method is empty and sealed.
        }

        protected virtual void HandleParameter(string name, object value)
        {
            if (HandleAdditionalParameter(name, value))
                return;

            if (AttachedPropertyRegistry.AttachedPropertyHandlers.TryGetValue(name, out var handler))
            {
                handler(NativeControl, value);
                return;
            }

            throw new NotImplementedException($"{GetType().FullName} doesn't recognize parameter '{name}'");
        }

        protected virtual bool HandleAdditionalParameter(string name, object value) => false;

        protected abstract MC.BindableObject CreateNativeElement();

        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
        }

        MC.BindableObject IMauiElementHandler.ElementControl => NativeControl;

        object IElementHandler.TargetElement => NativeControl;
    }
}
