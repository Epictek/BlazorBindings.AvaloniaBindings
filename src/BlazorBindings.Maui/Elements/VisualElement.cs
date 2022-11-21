using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class VisualElement
    {
        static partial void RegisterAdditionalHandlers()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<VisualElement>(nameof(Behaviors),
                (renderer, parent, component) => new ListContentPropertyHandler<MC.VisualElement, MC.Behavior>(x => x.Behaviors));
        }

        // This property is defined manually to allow to override it's behavior (see Shell).
        /// <summary>
        /// Gets or sets the color which will fill the background of a VisualElement.
        /// </summary>
        [Parameter] public Color BackgroundColor { get; set; }

        [Parameter] public RenderFragment Behaviors { get; set; }

        protected override bool HandleAdditionalParameter(string name, object value)
        {
            if (name == nameof(BackgroundColor))
            {
                if (!Equals(BackgroundColor, value))
                {
                    BackgroundColor = (Color)value;
                    NativeControl.BackgroundColor = BackgroundColor;
                }
                return true;
            }
            else if (name == nameof(Behaviors))
            {
                Behaviors = (RenderFragment)value;
                return true;
            }

            return base.HandleAdditionalParameter(name, value);
        }

        protected override void RenderAdditionalPartialElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalPartialElementContent(builder, ref sequence);

            RenderTreeBuilderHelper.AddContentProperty(builder, sequence++, typeof(VisualElement), Behaviors);
        }
    }
}
