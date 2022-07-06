// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Core
{
    internal delegate IElementHandler ElementHandlerFactory(NativeComponentRenderer renderer, IElementHandler parentHandler, IComponent component);
}