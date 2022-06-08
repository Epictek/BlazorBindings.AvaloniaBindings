using BlazorBindings.Maui;

namespace Benchmark
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiBlazorBindings();

            return builder.Build();
        }
    }
}