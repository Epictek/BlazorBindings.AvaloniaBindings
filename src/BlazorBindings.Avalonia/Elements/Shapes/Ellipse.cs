// <auto-generated>
//     This code was generated by a BlazorBindings.Avalonia component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using ACS = Avalonia.Controls.Shapes;
using BlazorBindings.AvaloniaBindings.Elements;
using Microsoft.AspNetCore.Components.Rendering;

#pragma warning disable CA2252

namespace BlazorBindings.AvaloniaBindings.Elements.Shapes
{
    public partial class Ellipse : Shape
    {
        [Parameter] new public Action<Ellipse> Attached { get; set; }
    }

    public static class EllipseExtensions
    {
        public static Ellipse ToolTip_TipExtension(this Ellipse self, string text)
        {
            self.AttachedProperties["ToolTip.Tip"] = text;

            return self;
        }
    }
}
