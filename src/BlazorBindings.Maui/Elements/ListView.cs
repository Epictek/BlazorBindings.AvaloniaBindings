using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class ListView<T>
    {
        [Parameter] public RenderFragment<T> ItemTemplate { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(ItemTemplate))
            {
                ItemTemplate = (RenderFragment<T>)value;
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }

        protected override void RenderAdditionalPartialElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalPartialElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddSyncDataTemplateProperty<MC.ItemsView<MC.Cell>, T>(builder, sequence++, ItemTemplate, (x, template) => x.ItemTemplate = template);
        }
    }
}
