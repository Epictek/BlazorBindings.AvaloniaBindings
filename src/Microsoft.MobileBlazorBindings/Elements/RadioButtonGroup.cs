using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class RadioButtonGroup<T> : StackLayout
    {
        static RadioButtonGroup()
        {
            ElementHandlerRegistry.RegisterElementHandler<RadioButtonGroup<T>>(renderer =>
                new RadioButtonGroupHandler(renderer, new XF.StackLayout()));
        }

        [Parameter] public T SelectedValue { get; set; }
        [Parameter] public EventCallback<T> SelectedValueChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (SelectedValue != null)
            {
                builder.AddAttribute(nameof(SelectedValue), AttributeHelper.ObjectToDelegate(SelectedValue));
            }
            if (SelectedValueChanged.HasDelegate)
            {
                builder.AddAttribute("onselectedvaluechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleSelectedValueChanged));
            }

            base.RenderAttributes(builder);
        }

        private Task HandleSelectedValueChanged(ChangeEventArgs args)
        {
            return SelectedValueChanged.InvokeAsync((T)args.Value);
        }
    }
}
