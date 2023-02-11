using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ControlGallery.Views.ErrorBoundaries
{
    public class ErrorBoundary : ErrorBoundaryBase
    {
        protected override Task OnErrorAsync(Exception exception)
        {
            return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (CurrentException != null)
            {
                ErrorContent?.Invoke(CurrentException).Invoke(builder);
            }
            else
            {
                ChildContent?.Invoke(builder);
            }
        }
    }
}
