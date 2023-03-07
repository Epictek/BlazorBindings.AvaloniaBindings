// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;

namespace BlazorBindings.Maui;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMauiBlazorBindings(this MauiAppBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // Use factories for performance.
        builder.Services
            .AddSingleton<Navigation>(svcs => new Navigation(svcs))
            .AddSingleton<INavigation>(services => services.GetRequiredService<Navigation>())
            .AddSingleton<MauiBlazorBindingsRenderer>(svcs => new MauiBlazorBindingsRenderer(svcs, svcs.GetRequiredService<ILoggerFactory>()));

        return builder;
    }
}
