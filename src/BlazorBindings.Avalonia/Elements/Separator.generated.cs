// <auto-generated>
//     This code was generated by a BlazorBindings.Avalonia component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>


using BlazorBindings.Avalonia.Elements.Primitives;

#pragma warning disable CA2252

namespace BlazorBindings.Avalonia.Elements
{
    /// <summary>
    /// A separator control.
    /// </summary>
    public partial class Separator : TemplatedControl
    {
        static Separator()
        {
            RegisterAdditionalHandlers();
        }

        public new AC.Separator NativeControl => (AC.Separator)((AvaloniaObject)this).NativeControl;

        protected override AC.Separator CreateNativeElement() => new();


        static partial void RegisterAdditionalHandlers();
    }
}
