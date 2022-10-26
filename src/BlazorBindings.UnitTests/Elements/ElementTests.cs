// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui.Elements;
using NUnit.Framework;

namespace BlazorBindings.UnitTests.Elements
{
    public class ElementTests
    {
        [Test]
        public void NativeControlShouldReturnInstance_Editor()
            => Assert.That(new Editor().NativeControl, Is.Not.Null);

        [Test]
        public void NativeControlShouldReturnInstance_Label()
            => Assert.That(new Label().NativeControl, Is.Not.Null);

        [Test]
        public void NativeControlShouldReturnInstance_CollectionView()
            => Assert.That(new CollectionView<string>().NativeControl, Is.Not.Null);

        [Test]
        public void NativeControlShouldReturnInstance_ContentPage()
            => Assert.That(new ContentPage().NativeControl, Is.Not.Null);

        [Test]
        public void NativeControlShouldReturnNullWhenHandlerNotInitialized_TapGestureRecognizer()
            => Assert.That(new TapGestureRecognizer().NativeControl, Is.Not.Null);
    }
}
