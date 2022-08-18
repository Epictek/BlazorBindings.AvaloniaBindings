// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace BlazorBindings.Maui.Elements
{
    public partial class AttributeHelper
    {
        private static readonly PointCollectionConverter _pointCollectionConverter = new();

        public static PointCollection StringToPointCollection(object pointCollectionString)
        {
            return (PointCollection)_pointCollectionConverter.ConvertFromInvariantString((string)pointCollectionString);
        }
    }
}
