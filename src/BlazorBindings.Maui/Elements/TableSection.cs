using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class TableSection
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(ChildContent))
            {
                ChildContent = (RenderFragment)value;
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddListContentProperty<MC.TableSection, MC.Cell>(builder, sequence++, ChildContent, x => x);
        }
    }
}
