// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        private static readonly BoundsTypeConverter _boundsTypeConverter = new BoundsTypeConverter();

        /// <summary>
        /// Helper method to serialize <see cref="Rect" /> objects.
        /// </summary>
        public static string RectToString(Rectangle rectangle)
        {
            var (x, y, w, h) = rectangle;
            // Use 0.##### format to disallow exponential notation, as it cannot be parsed afterwards.
            return FormattableString.Invariant($"{x:0.#####},{y:0.#####},{w:0.#####},{h:0.#####}");
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Rect" /> objects.
        /// </summary>
        public static Rectangle StringToRect(object rectangleString, Rectangle defaultValueIfNull = default)
        {
            return StringToBoundsRect(rectangleString, defaultValueIfNull);
        }

        /// <summary>
        /// Helper method deserialize LayoutBounds <see cref="Rect"/> objects.
        /// The difference is that "10, 15" is not a valid Rect string, but could be a valid 
        /// LayoutBounds <see cref="Rect"/> string (with Height and Width set to Auto).
        /// </summary>
        public static Rectangle StringToBoundsRect(object rectangleString, Rectangle defaultValueIfNull = default)
        {
            if (rectangleString is null)
            {
                return defaultValueIfNull;
            }
            if (!(rectangleString is string rectangleAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(rectangleString));
            }

            return (Rectangle)_boundsTypeConverter.ConvertFromInvariantString(rectangleAsString);
        }
    }
}
