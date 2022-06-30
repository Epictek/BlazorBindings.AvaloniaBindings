// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace BlazorBindings.Core
{
    /// <summary>
    /// Utility intermediate class to make it easier to strongly-type a derived <see cref="ElementManager"/>.
    /// </summary>
    /// <typeparam name="TElementType"></typeparam>
    public abstract class ElementManager<TElementType> : ElementManager
    {
        private static TElementType ConvertToType(IElementHandler elementHandler, string parameterName)
        {
            return elementHandler switch
            {
                TElementType elementType => elementType,
                null => default,
                _ => throw new ArgumentException($"Expected parameter value of type '{elementHandler.GetType().FullName}' to be convertible to type '{typeof(TElementType).FullName}'.", parameterName)
            };
        }

        public sealed override void SetChildElement(IElementHandler parentHandler, IElementHandler previousChildHandler, IElementHandler newChildHandler, int physicalSiblingIndex)
        {
            SetChildElement(ConvertToType(parentHandler, nameof(parentHandler)), ConvertToType(previousChildHandler, nameof(previousChildHandler)), ConvertToType(newChildHandler, nameof(newChildHandler)), physicalSiblingIndex);
        }

        public sealed override int GetChildElementIndex(IElementHandler parentHandler, IElementHandler childHandler)
        {
            return GetChildElementIndex(ConvertToType(parentHandler, nameof(parentHandler)), ConvertToType(childHandler, nameof(childHandler)));
        }

        public sealed override bool IsParented(IElementHandler handler)
        {
            return IsParented(ConvertToType(handler, nameof(handler)));
        }

        public sealed override bool IsParentOfChild(IElementHandler parentHandler, IElementHandler childHandler)
        {
            return IsParentOfChild(ConvertToType(parentHandler, nameof(parentHandler)), ConvertToType(childHandler, nameof(childHandler)));
        }

        protected abstract void SetChildElement(TElementType parentHandler, TElementType previousChildHandler, TElementType newChildHandler, int physicalSiblingIndex);
        protected abstract int GetChildElementIndex(TElementType parentHandler, TElementType childHandler);
        protected abstract bool IsParented(TElementType handler);
        protected abstract bool IsParentOfChild(TElementType parentHandler, TElementType childHandler);
    }
}
