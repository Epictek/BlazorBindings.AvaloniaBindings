// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    /// <summary>
    /// The only purpose of this type is to wrap content property handler, since currently renderer does not allow 
    /// handlers without corresponding component.
    /// </summary>
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Class is used generically.
    internal class PropertyWrapperComponent : NativeControlComponentBase
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                if (parameterValue.Name == nameof(ChildContent))
                {
                    ChildContent = (RenderFragment)parameterValue.Value;
                }
                else
                {
                    throw new InvalidOperationException(
                        $"Object of type 'PropertyWrapperComponent' does not have a property " +
                        $"matching the name '{parameterValue.Name}'.");
                }
            }

            return base.SetParametersAsync(ParameterView.Empty);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ChildContent(builder);
        }
    }
}
