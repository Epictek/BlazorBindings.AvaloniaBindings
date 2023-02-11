using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls;

namespace BlazorBindings.UnitTests
{
    public static class TestProperties
    {
        public static readonly BindableProperty ComponentProperty = BindableProperty.Create("Component", typeof(IComponent), typeof(TestProperties));
    }
}
