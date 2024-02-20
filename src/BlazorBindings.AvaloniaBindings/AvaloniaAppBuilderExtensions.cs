﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Logging;
using AvaloniaAppBuilder = global::Avalonia.AppBuilder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BlazorBindings.AvaloniaBindings.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorBindings.AvaloniaBindings;

public static class AvaloniaAppBuilderExtensions
{
    public static AvaloniaAppBuilder UseAvaloniaBlazorBindings(this AvaloniaAppBuilder builder, Action<IServiceCollection> configureServices)
    {   
        ArgumentNullException.ThrowIfNull(builder);

        // Use factories for performance.
        builder.AfterSetup(b =>
        {
            IServiceProvider serviceProvider = null;

            var services = new ServiceCollection();

            configureServices?.Invoke(services);
            
            RegisterBlazorServices(services);

            serviceProvider = services.BuildServiceProvider();

            b.With(serviceProvider);


            ((IAvaloniaBlazorApplication)b.Instance).Initialize(serviceProvider);
        });

        return builder;
    }

    public static void RegisterBlazorServices(IServiceCollection services)
    {
        //.TryAddSingleton<Navigation>(svcs => new Navigation(svcs.GetRequiredService<AvaloniaBlazorBindingsServiceProvider>()))
        //.TryAddSingleton<INavigation>(services => services.GetRequiredService<Navigation>())
        //services.TryAddSingleton(new AvaloniaBlazorBindingsServiceProvider(serviceProviderGetter));
        services.TryAddSingleton<IServiceProvider>(svcs => svcs.GetRequiredService<AvaloniaBlazorBindingsServiceProvider>());
        services.TryAddSingleton(svcs => new AvaloniaBlazorBindingsRenderer(svcs.GetRequiredService<AvaloniaBlazorBindingsServiceProvider>(), svcs.GetRequiredService<ILoggerFactory>()));
        services.TryAddSingleton<INavigation>(svcs => new BlazorNavigation(svcs.GetRequiredService<AvaloniaBlazorBindingsServiceProvider>()));
        //services.TryAddSingleton(svcs => ((IAvaloniaBlazorApplication)Avalonia.Application.Current).Navigation);

        services.AddLogging();
    }
}
