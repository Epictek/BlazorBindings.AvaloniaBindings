using BlazorBindings.Maui;
using FlyoutPageSample.Views;

namespace FlyoutPageSample
{
    public partial class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<MainPage>(this);
        }
    }
}