using Microsoft.Maui.Layouts;

namespace BenchmarkCode
{
    public class MovingCircle : BoxView
    {
        private readonly double radius;

        public MovingCircle(double radius, Color color)
        {
            this.radius = radius;
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.PositionProportional);
            Color = color;
            CornerRadius = radius;
        }

        public void SetPosition(double x, double y)
        {
            AbsoluteLayout.SetLayoutBounds(this, new Rect(x, y, radius, radius));
        }
    }
}
