// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;
using HybridAppBarcodeScanner.Native.Pages;

namespace HybridAppBarcodeScanner
{
    public class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            _ = renderer.AddComponent<MainPage>(this);
        }
    }
}
