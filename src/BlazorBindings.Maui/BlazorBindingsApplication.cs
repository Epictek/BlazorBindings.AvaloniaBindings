using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace BlazorBindings.Maui
{
    public class BlazorBindingsApplication<T> : Application where T : IComponent
    {
        public BlazorBindingsApplication(IServiceProvider services)
        {
            var renderer = services.GetRequiredService<MauiBlazorBindingsRenderer>();
            var task = renderer.AddComponent(typeof(T), this);
            AwaitVoid(task);

            static async void AwaitVoid(Task task) => await task;
        }
    }
}
