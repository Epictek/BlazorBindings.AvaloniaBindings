using Microsoft.Maui.Controls;

namespace BlazorBindings.UnitTests.Extensions
{
    public static class TemplatedViewExtensions
    {
        public static T GetTemplateRoot<T>(this TemplatedView view)
        {
            return (T)((Layout)view.Children[0]).Children[0];
        }
    }
}
