// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        private static readonly BoundsTypeConverter _boundsTypeConverter = new();

        /// <summary>
        /// Helper method deserialize LayoutBounds <see cref="Rect"/> objects.
        /// The difference is that "10, 15" is not a valid Rect string, but could be a valid 
        /// LayoutBounds <see cref="Rect"/> string (with Height and Width set to Auto).
        /// </summary>
        public static Rect GetBoundsRect(object value, Rect defaultValueIfNull = default)
        {
            return value switch
            {
                null => defaultValueIfNull,
                Rect rect => rect,
                string rectangleAsString => (Rect)_boundsTypeConverter.ConvertFromInvariantString(rectangleAsString),
                _ => throw new ArgumentException("Cannot convert value to Rect.", nameof(value)),
            };
        }
    }
}
