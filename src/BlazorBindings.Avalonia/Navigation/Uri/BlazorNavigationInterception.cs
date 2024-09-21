using Microsoft.AspNetCore.Components.Routing;

namespace BlazorBindings.Avalonia.UriNavigation;

internal class BlazorNavigationInterception : INavigationInterception
{
    public Task EnableNavigationInterceptionAsync() => Task.CompletedTask;
}
