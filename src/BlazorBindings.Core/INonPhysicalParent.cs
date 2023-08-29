namespace BlazorBindings.Core;

public interface INonPhysicalParent : INonPhysicalHandler, IContainerElementHandler
{
    /// <summary>
    /// If this property is true, then renderer will pass children of this component to parent.
    /// This is useful if you want to apply some effects to children (e.g. attached properties),
    /// but still add them to parent element.
    /// </summary>
    internal bool ShouldAddChildrenToParent { get => false; }
}
