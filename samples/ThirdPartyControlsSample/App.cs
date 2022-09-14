// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using ThirdPartyControlsSample.Pages;
using BlazorBindings.Maui;

namespace ThirdPartyControlsSample
{
    public class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<AppShell>(this);
        }
    }
}
