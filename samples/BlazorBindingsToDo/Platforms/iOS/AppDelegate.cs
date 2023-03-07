// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Foundation;

namespace BlazorBindingsToDo;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}