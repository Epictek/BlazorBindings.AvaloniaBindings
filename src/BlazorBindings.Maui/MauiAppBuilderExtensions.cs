// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using System;

namespace BlazorBindings.Maui
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseMauiBlazorBindings(this MauiAppBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.Services
                .AddSingleton<NavigationService>()
                .AddSingleton<INavigationService>(services => services.GetService<NavigationService>())
                .AddSingleton<ShellNavigationManager>()
                .AddScoped<MauiBlazorBindingsRenderer>();

            return builder;
        }
    }
}
