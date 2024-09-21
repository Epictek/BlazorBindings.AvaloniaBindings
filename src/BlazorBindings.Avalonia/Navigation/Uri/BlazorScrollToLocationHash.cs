using Microsoft.AspNetCore.Components.Routing;

namespace BlazorBindings.Avalonia.UriNavigation;

internal class BlazorScrollToLocationHash : IScrollToLocationHash
{
    public Task RefreshScrollPositionForHash(string locationAbsolute) => Task.CompletedTask;
}
