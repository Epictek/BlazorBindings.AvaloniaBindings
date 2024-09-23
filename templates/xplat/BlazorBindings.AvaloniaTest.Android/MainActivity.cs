using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using BlazorBindings.Avalonia;

namespace BlazorBindings.AvaloniaTest.Android;

[Activity(
    Label = "BlazorBindings.AvaloniaTest.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseAvaloniaBlazorBindings(service => {});
    }
}
