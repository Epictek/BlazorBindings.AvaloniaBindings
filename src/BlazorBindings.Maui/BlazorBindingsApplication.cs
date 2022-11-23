using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;

namespace BlazorBindings.Maui
{
    public class BlazorBindingsApplication<T> : Application where T : IComponent
    {
        public BlazorBindingsApplication(IServiceProvider services)
        {
            var renderer = services.GetRequiredService<MauiBlazorBindingsRenderer>();
            _ = renderer.AddComponent<T>(this);
        }
    }
}
