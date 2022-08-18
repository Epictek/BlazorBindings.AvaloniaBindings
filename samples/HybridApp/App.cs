using BlazorBindings.Maui;

namespace HybridApp
{
    public partial class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<MainPage>(this);
        }
    }
}