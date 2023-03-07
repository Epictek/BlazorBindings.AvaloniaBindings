using Microsoft.Maui.Controls;

namespace BlazorBindings.Maui;

public class BlazorBindingsApplication<T> : Application where T : IComponent
{
    public BlazorBindingsApplication(IServiceProvider services)
    {
        var renderer = services.GetRequiredService<MauiBlazorBindingsRenderer>();

        if (WrapperComponentType != null)
        {
            var navigation = services.GetService<INavigation>();
            (navigation as Navigation)?.SetWrapperComponentType(WrapperComponentType);
        }

        var (componentType, parameters) = GetComponentToRender();
        var task = renderer.AddComponent(componentType, this, parameters);
        AwaitVoid(task);

        static async void AwaitVoid(Task task) => await task;
    }

    public virtual Type WrapperComponentType { get; }

    private (Type ComponentType, Dictionary<string, object> Parameters) GetComponentToRender()
    {
        if (WrapperComponentType is null)
        {
            return (typeof(T), null);
        }
        else
        {
            var parameters = new Dictionary<string, object>
            {
                ["ChildContent"] = RenderFragments.FromComponentType(typeof(T))
            };
            return (WrapperComponentType, parameters);
        }
    }
}
