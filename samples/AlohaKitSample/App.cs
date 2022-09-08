// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using AlohaKitSample.Pages;
using BlazorBindings.Maui;

namespace AlohaKitSample
{
    public class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<RatingPage>(this);
        }
    }
}
