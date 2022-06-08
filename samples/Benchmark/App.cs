// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;

namespace Benchmark
{
    public class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<MainPage>(this);
        }
    }
}
