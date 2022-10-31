using BlazorBindings.Maui.Elements.Input;
using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public partial class BackButtonBehavior
    {
        [Parameter] public EventCallback OnBackButtonPressed { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(OnBackButtonPressed))
            {
                if (!Equals(OnBackButtonPressed, value))
                {
                    OnBackButtonPressed = (EventCallback)value;
                    NativeControl.Command = new EventCallbackCommand(OnBackButtonPressed);
                }
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }
    }
}
