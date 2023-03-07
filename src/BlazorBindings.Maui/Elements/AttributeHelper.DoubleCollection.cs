// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements;

public partial class AttributeHelper
{
    private static readonly DoubleCollectionConverter _doubleCollectionConverter = new();

    public static DoubleCollection GetDoubleCollection(object value)
    {
        return value switch
        {
            null => null,
            DoubleCollection doubleCollection => doubleCollection,
            double[] doubleArray => (DoubleCollection)doubleArray,
            float[] floatArray => (DoubleCollection)floatArray,
            string => (DoubleCollection)_doubleCollectionConverter.ConvertFromInvariantString((string)value),
            _ => throw new ArgumentException("Cannot convert value to DoubleCollection.", nameof(value))
        };
    }
}
