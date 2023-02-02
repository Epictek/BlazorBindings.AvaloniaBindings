using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ControlGallery.Views.ErrorBoundaries
{
    public class GlobalErrorBoundary : ErrorBoundaryBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ChildContent?.Invoke(builder);
        }

        protected override Task OnErrorAsync(Exception exception)
        {
            Application.Current.MainPage.DisplayAlert("Global Error Boundary", exception.Message, "OK");
            Recover();
            return Task.CompletedTask;
        }
    }
}
