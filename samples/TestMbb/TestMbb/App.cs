using BlazorBindings.Maui;

namespace TestMbb
{
    public partial class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<MainPage>(this);
        }
    }
}