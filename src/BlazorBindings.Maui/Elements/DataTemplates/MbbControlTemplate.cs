using Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates
{
    internal class MbbControlTemplate : ControlTemplate
    {
        public MbbControlTemplate(ControlTemplateItemsComponent controlTemplateItemsContainer)
            : base(controlTemplateItemsContainer.AddTemplateRoot)
        {
        }
    }
}
