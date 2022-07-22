// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class AttributeHelper
    {
        private static readonly DoubleCollectionConverter _doubleCollectionConverter = new();

        public static string DoubleCollectionToString(DoubleCollection doubleCollection)
        {
            return _doubleCollectionConverter.ConvertToInvariantString(doubleCollection);
        }

        public static DoubleCollection StringToDoubleCollection(object doubleCollectionString)
        {
            return (DoubleCollection)_doubleCollectionConverter.ConvertFromInvariantString((string)doubleCollectionString);
        }
    }
}
