// <auto-generated>
//     This code was generated by a BlazorBindings.Maui component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TemplatedPage : Page
    {
        static TemplatedPage()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.TemplatedPage NativeControl => (MC.TemplatedPage)((BindableObject)this).NativeControl;

        protected override MC.TemplatedPage CreateNativeElement() => new();


        static partial void RegisterAdditionalHandlers();
    }
}
