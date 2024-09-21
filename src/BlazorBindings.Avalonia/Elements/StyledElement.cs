using Avalonia.Controls;

namespace BlazorBindings.Avalonia.Elements;

public partial class StyledElement
{
    protected override bool HandleAdditionalParameter(string name, object value)
    {
        if (name == "class")
        {
            NativeControl.Classes.Replace(Classes.Parse((string)value));
            return true;
        }

        return base.HandleAdditionalParameter(name, value);
    }
}
