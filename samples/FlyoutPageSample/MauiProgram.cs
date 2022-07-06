using BlazorBindings.Maui;

namespace FlyoutPageSample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            return MauiApp.CreateBuilder()
                .UseMauiApp<App>()
                .UseMauiBlazorBindings()
                .Build();
        }
    }
}