// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Core;

public interface INonPhysicalHandler : IElementHandler
{
    object IElementHandler.TargetElement => null;
}
