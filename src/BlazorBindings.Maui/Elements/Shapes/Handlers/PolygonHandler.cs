namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public partial class PolygonHandler
    {
        public override bool ApplyAdditionalAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(Polygon.Points):
                    PolygonControl.Points = AttributeHelper.StringToPointCollection(attributeValue);
                    return true;
                default:
                    return base.ApplyAdditionalAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
