using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Internal;

internal class ListContentPropertyComponent<TControl, TItem> : NativeControlComponentBase, IMauiContainerElementHandler, INonChildContainerElement
    where TItem : class
{
    private TControl _parent;
    private IList<TItem> _propertyItems;

    [Parameter] public Func<TControl, IList<TItem>> ListPropertyAccessor { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }

    protected override RenderFragment GetChildContent() => ChildContent;

    public void SetParent(object parentElement)
    {
        _parent = (TControl)parentElement;
        _propertyItems = ListPropertyAccessor(_parent);
    }

    public void RemoveFromParent(object parentElement)
    {
        // Because this Handler is used internally only, this method is no-op.
    }

    MC.BindableObject IMauiElementHandler.ElementControl => _parent as MC.BindableObject;
    object IElementHandler.TargetElement => _parent;

    void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
    {
        if (child is not TItem typedChild)
            throw new NotSupportedException($"Cannot add item of type {child?.GetType().Name} to a {typeof(TItem)} collection.");

        _propertyItems.Insert(physicalSiblingIndex, typedChild);
    }

    int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
    {
        return _propertyItems.IndexOf(child as TItem);
    }

    void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
    {
        _propertyItems.Remove(child as TItem);
    }

    void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }
}
