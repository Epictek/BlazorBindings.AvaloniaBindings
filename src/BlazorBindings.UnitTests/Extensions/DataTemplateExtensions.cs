using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;

namespace BlazorBindings.UnitTests.Extensions
{
    public static class DataTemplateExtensions
    {
        public static T CreateContent<T>(this DataTemplate self, object item, BindableObject container)
        {
            var content = (BindableObject)self.CreateContent(item, container);
            content.BindingContext = item;
            return (T)((Layout)content).Children[0];
        }
    }
}
