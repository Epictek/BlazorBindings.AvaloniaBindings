using BlazorBindings.Maui.ComponentGenerator;
using Microsoft.Maui.Controls.Maps;

[assembly: GenerateComponent(typeof(Map),
    Exclude = new[] { nameof(Map.ItemsSource), nameof(Map.ItemTemplate), nameof(Map.ItemTemplateSelector) })]
[assembly: GenerateComponent(typeof(Pin))]