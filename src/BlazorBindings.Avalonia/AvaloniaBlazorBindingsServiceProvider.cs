using BlazorBindings.Avalonia.UriNavigation;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorBindings.Avalonia;

// We cannot add Navigation services to common ServiceProvider as it leads to conflicts
// with Blazor navigation for hybrid apps.
// Therefore this wrapper is created to override those services for MBB cases only.
// Any better approach?...
internal class AvaloniaBlazorBindingsServiceProvider : IServiceProvider
{
    private NavigationManager _navigationManager;
    private INavigationInterception _navigationInterception;
    private IScrollToLocationHash _scrollToLocationHash;
    private Func<IServiceProvider> _serviceProviderFactory;
    private IServiceProvider _serviceProvider;

    public AvaloniaBlazorBindingsServiceProvider(Func<IServiceProvider> serviceProviderFactory)
    {
        _serviceProviderFactory = serviceProviderFactory;
    }

    private IServiceProvider ServiceProvider => _serviceProvider ??= _serviceProviderFactory();

    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(NavigationManager))
            return _navigationManager ??= new BlazorNavigationManager();

        if (serviceType == typeof(INavigationInterception))
            return _navigationInterception ??= new BlazorNavigationInterception();

        if (serviceType == typeof(IScrollToLocationHash))
            return _scrollToLocationHash ??= new BlazorScrollToLocationHash();

        return ServiceProvider.GetService(serviceType);
    }
}
