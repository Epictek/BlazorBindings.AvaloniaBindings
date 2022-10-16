using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBindings.Maui
{
    public interface INavigationService
    {
        Task NavigateToAsync(string uri, Dictionary<string, object> parameters = null);
        Task PopAsync(bool animated = true);
        Task PopModalAsync(bool animated = true);
        Task PushAsync(RenderFragment renderFragment, bool animated = true);
        Task PushAsync<T>(Dictionary<string, object> arguments = null, bool animated = true) where T : IComponent;
        Task PushModalAsync(RenderFragment renderFragment, bool animated = true);
        Task PushModalAsync<T>(Dictionary<string, object> arguments = null, bool animated = true) where T : IComponent;
    }
}