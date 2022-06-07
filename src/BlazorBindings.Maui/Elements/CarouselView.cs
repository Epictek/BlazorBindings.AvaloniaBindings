using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC= Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class CarouselView<T> : ItemsView<T>
    {
        static CarouselView()
        {
            ElementHandlerRegistry.RegisterElementHandler<CollectionView<T>>(
                renderer => new CarouselViewHandler(renderer, new MC.CarouselView()));
        }

        public new MC.CarouselView NativeControl => (ElementHandler as CarouselViewHandler)?.CarouselViewControl;
    }
}
