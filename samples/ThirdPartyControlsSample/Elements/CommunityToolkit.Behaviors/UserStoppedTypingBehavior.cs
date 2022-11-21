using BlazorBindings.Maui.Elements.Input;
using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Maui.Elements.CommunityToolkit.Behaviors
{
    public partial class UserStoppedTypingBehavior
    {
        [Parameter] public EventCallback<string> Command { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(Command))
            {
                if (!Equals(Command, value))
                {
                    Command = (EventCallback<string>)value;
                    NativeControl.Command = Command.HasDelegate
                        ? new EventCallbackCommand(value => InvokeEventCallback(Command, (string)value))
                        : null;
                }
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }
    }
}
