// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Core
{
    /// <summary>
    /// Utilities needed by the system to manage native controls. Implementations
    /// of native rendering systems have their own quirks in terms of dealing with
    /// parent/child relationships, so each must implement this given the constraints
    /// and requirements of their systems.
    /// </summary>
    public abstract class ElementManager
    {
        public abstract void SetChildElement(IElementHandler parentHandler, IElementHandler previousChildHandler, IElementHandler newChildHandler, int physicalSiblingIndex);
        public abstract int GetChildElementIndex(IElementHandler parentHandler, IElementHandler childHandler);
        public abstract bool IsParented(IElementHandler handler);
        public abstract bool IsParentOfChild(IElementHandler parentHandler, IElementHandler childHandler);
    }
}
