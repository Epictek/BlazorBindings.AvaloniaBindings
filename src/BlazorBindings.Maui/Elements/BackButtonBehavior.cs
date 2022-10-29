using BlazorBindings.Maui.Elements.Input;
using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements
{
    public partial class BackButtonBehavior
    {
        [Parameter] public EventCallback Command { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(Command))
            {
                if (!Equals(Command, value))
                {
                    Command = (EventCallback)value;
                    NativeControl.Command = new EventCallbackCommand(Command);
                }
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }
    }
}
