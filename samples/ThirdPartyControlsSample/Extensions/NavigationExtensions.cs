using BlazorBindings.Maui;
using CommunityToolkit.Maui.Views;

namespace ThirdPartyControlsSample.Extensions;

public static class NavigationExtensions
{
    public static async Task<object> ShowPopupAsync<T>(this Navigation navigationService)
    {
        var popup = await navigationService.BuildElement<Popup>(typeof(T), null);
        return await Application.Current.MainPage.ShowPopupAsync(popup);
    }
}
