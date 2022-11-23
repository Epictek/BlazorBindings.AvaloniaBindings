using BlazorBindings.Maui;
using FlyoutPageSample.Views;

namespace FlyoutPageSample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            return MauiApp.CreateBuilder()
                .UseMauiApp<BlazorBindingsApplication<MainPage>>()
                .UseMauiBlazorBindings()
                .Build();
        }
    }
}