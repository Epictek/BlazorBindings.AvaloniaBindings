// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;

namespace BlazorBindings.Core;

public static class ElementHandlerRegistry
{
    internal static Dictionary<string, ElementHandlerFactory> ElementHandlers { get; } = new();

    public static void RegisterElementHandler<TComponent>(
        Func<NativeComponentRenderer, IElementHandler, TComponent, IElementHandler> factory) where TComponent : NativeControlComponentBase
    {
        ElementHandlers[typeof(TComponent).FullName] = (renderer, parent, component) => factory(renderer, parent, (TComponent)component);
    }

    public static void RegisterElementHandler<TComponent>(
        Func<NativeComponentRenderer, IElementHandler> factory) where TComponent : NativeControlComponentBase
    {
        ElementHandlers[typeof(TComponent).FullName] = (renderer, _, __) => factory(renderer);
    }

    public static void RegisterPropertyContentHandler<TComponent>(string propertyName,
        Func<NativeComponentRenderer, IElementHandler> factory) where TComponent : NativeControlComponentBase
    {
        var key = $"p-{typeof(TComponent).FullName}.{propertyName}";
        ElementHandlers[key] = (renderer, _, __) => factory(renderer);
    }

    public static void RegisterPropertyContentHandler<TComponent>(string propertyName,
        Func<NativeComponentRenderer, IElementHandler, IComponent, IElementHandler> factory) where TComponent : NativeControlComponentBase
    {
        var key = $"p-{typeof(TComponent).FullName}.{propertyName}";
        ElementHandlers[key] = (renderer, parent, component) => factory(renderer, parent, component);
    }
}
