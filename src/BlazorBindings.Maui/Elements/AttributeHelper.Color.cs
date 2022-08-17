// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Graphics;
using System;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="Color" /> objects.
        /// </summary>
        public static string ColorToString(Color color)
        {
            if (color is null)
            {
                return null;
            }

            return color.ToRgbaHex(true);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Color" /> objects.
        /// </summary>
        public static Color GetString(object obj, Color defaultValueIfNull = null)
        {
            return obj switch
            {
                null => null,
                Color color => color,
                string colorAsString => Color.FromRgba(colorAsString),
                _ => throw new ArgumentException("Cannot convert parameter to Color.", nameof(obj))
            };
        }
    }
}
