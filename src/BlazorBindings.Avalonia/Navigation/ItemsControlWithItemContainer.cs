using Avalonia.Controls;

namespace BlazorBindings.Avalonia.Navigation;

public class ItemsControlWithItemContainer : ItemsControl
{
    protected override bool NeedsContainerOverride(object item, int index, out object recycleKey)
    {
        recycleKey = DefaultRecycleKey;
        return true;
    }
}