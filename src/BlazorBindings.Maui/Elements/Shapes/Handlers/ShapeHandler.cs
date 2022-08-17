namespace BlazorBindings.Maui.Elements.Shapes.Handlers
{
    public partial class ShapeHandler
    {
        public override bool ApplyAdditionalAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(Shape.StrokeDashArray):
                    ShapeControl.StrokeDashArray = AttributeHelper.StringToDoubleCollection(attributeValue);
                    return true;
                case nameof(Shape.StrokeColor):
                    ShapeControl.Stroke = AttributeHelper.GetString(attributeValue);
                    return true;
                case nameof(Shape.FillColor):
                    ShapeControl.Fill = AttributeHelper.GetString(attributeValue);
                    return true;
                default:
                    return base.ApplyAdditionalAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
