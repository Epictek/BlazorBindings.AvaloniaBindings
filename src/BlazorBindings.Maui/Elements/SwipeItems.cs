using Microsoft.AspNetCore.Components;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class SwipeItems : IMauiContainerElementHandler
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(ChildContent))
            {
                ChildContent = (RenderFragment)value;
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex)
        {
            if (child is not MC.ISwipeItem swipeItem)
                throw new ArgumentException($"Expected child to be of type {typeof(MC.ISwipeItem).FullName} but it is of type {child?.GetType().FullName}.", nameof(child));

            NativeControl.Insert(physicalSiblingIndex, swipeItem);
        }

        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child)
        {
            if (child is not MC.ISwipeItem swipeItem)
                throw new ArgumentException($"Expected child to be of type {typeof(MC.ISwipeItem).FullName} but it is of type {child?.GetType().FullName}.", nameof(child));

            return NativeControl.IndexOf(swipeItem);
        }

        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child)
        {
            if (child is not MC.ISwipeItem swipeItem)
                throw new ArgumentException($"Expected child to be of type {typeof(MC.ISwipeItem).FullName} but it is of type {child?.GetType().FullName}.", nameof(child));

            NativeControl.Remove(swipeItem);
        }
    }
}
