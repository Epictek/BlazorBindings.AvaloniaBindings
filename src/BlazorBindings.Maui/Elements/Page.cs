using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class Page
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.BackButtonTitle",
                (element, value) => MC.NavigationPage.SetBackButtonTitle(element, value?.ToString()));
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.HasBackButton",
                (element, value) => MC.NavigationPage.SetHasBackButton((MC.Page)element, AttributeHelper.GetBool(value)));
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.HasNavigationBar",
                (element, value) => MC.NavigationPage.SetHasNavigationBar(element, AttributeHelper.GetBool(value)));
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NavigationPage.IconColor",
                (element, value) => MC.NavigationPage.SetIconColor(element, AttributeHelper.GetColor(value)));
        }
    }
}
