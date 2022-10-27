// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindingsXaminals.Models;

namespace BlazorBindingsXaminals.Data
{
    public static class Animals
    {
        public static Animal[] Items { get; } = BearData.Bears.ToArray();
    }
}
