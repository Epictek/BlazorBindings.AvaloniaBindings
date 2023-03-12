// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace ControlGallery;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp()
            .UseMauiBlazorBindings()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<IMediaPicker>(MediaPicker.Default);

        return builder.Build();
    }
}

public class App : BlazorBindingsApplication<AppShell>
{
    public App(IServiceProvider services) : base(services) { }

    public override Type WrapperComponentType => typeof(Root);
}