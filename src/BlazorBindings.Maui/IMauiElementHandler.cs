// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Maui;

public interface IMauiElementHandler : IElementHandler
{
    Microsoft.Maui.Controls.BindableObject ElementControl { get; }
}
