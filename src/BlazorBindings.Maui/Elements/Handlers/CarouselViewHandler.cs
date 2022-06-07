// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class CarouselViewHandler : ItemsViewHandler
    {
        public CarouselViewHandler(NativeComponentRenderer renderer, MC.CarouselView carouselViewControl) : base(renderer, carouselViewControl)
        {
            CarouselViewControl = carouselViewControl ?? throw new ArgumentNullException(nameof(carouselViewControl));
        }

        public MC.CarouselView CarouselViewControl { get; }
    }
}
