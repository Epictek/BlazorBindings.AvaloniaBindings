using BlazorBindings.Avalonia.Navigation;

namespace BlazorBindings.Avalonia;

public interface IAvaloniaBlazorApplication
{
    IServiceProvider ServiceProvider { get; }

    void Initialize(IServiceProvider serviceProvider);

    AvaloniaNavigation Navigation { get; }
}
