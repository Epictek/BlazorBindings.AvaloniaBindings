#if (CommunityToolkitChosen)
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
#endif
using Avalonia.Markup.Xaml;
using BlazorBindings.Avalonia;

namespace BlazorBindings.AvaloniaTest;

public class App : BlazorBindingsApplicationMainPage
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();
    }
}


public class BlazorBindingsApplicationMainPage : BlazorBindingsApplication<Main>
{
    protected BlazorBindingsApplicationMainPage()
    {
    }
}