// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using System;

namespace BlazorBindings.Maui
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseMauiBlazorBindings(this MauiAppBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            // Use factories for performance.
            builder.Services
                .AddSingleton<NavigationService>(svcs => new NavigationService(svcs))
                .AddSingleton<INavigationService>(services => services.GetRequiredService<NavigationService>())
                .AddSingleton<ShellNavigationManager>(svcs => new ShellNavigationManager(svcs.GetRequiredService<NavigationService>()))
                .AddScoped<MauiBlazorBindingsRenderer>(svcs => new MauiBlazorBindingsRenderer(svcs, svcs.GetRequiredService<ILoggerFactory>()));

            return builder;
        }
    }
}
