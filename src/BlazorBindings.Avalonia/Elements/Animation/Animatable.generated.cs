// <auto-generated>
//     This code was generated by a BlazorBindings.Avalonia component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using AA = Avalonia.Animation;
using BlazorBindings.Avalonia.Elements;

#pragma warning disable CA2252

namespace BlazorBindings.Avalonia.Elements.Animation
{
    /// <summary>
    /// Base class for all animatable objects.
    /// </summary>
    public partial class Animatable : AvaloniaObject
    {
        static Animatable()
        {
            RegisterAdditionalHandlers();
        }

        /// <summary>
        /// Gets or sets the property transitions for the control.
        /// </summary>
        [Parameter] public AA.Transitions Transitions { get; set; }

        public new AA.Animatable NativeControl => (AA.Animatable)((AvaloniaObject)this).NativeControl;

        protected override AA.Animatable CreateNativeElement() => new();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Transitions):
                    if (!Equals(Transitions, value))
                    {
                        Transitions = (AA.Transitions)value;
                        NativeControl.Transitions = Transitions;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        static partial void RegisterAdditionalHandlers();
    }
}
