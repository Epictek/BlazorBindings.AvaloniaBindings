using Avalonia;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Themes.Fluent;
using BlazorBindings.Avalonia;
using System;

namespace BlazorBindings.Avalonia.HelloWorld;

public class App : BlazorBindingsApplicationMainPage
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        this.AttachDevTools();
    }
}
public class BlazorBindingsApplicationMainPage : BlazorBindingsApplication<MainPage>
{
    //public App()
    //{
    //    //Styles.Add(new FluentTheme());
    //    //Styles.Add(new StyleInclude(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null))//(Uri?)null)
    //    //{
    //    //    Source = new Uri("avares://Avalonia.Controls.DataGrid/Themes/Fluent.xaml")
    //    //});
    //}
}
