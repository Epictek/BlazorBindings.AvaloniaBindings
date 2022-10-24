// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace BlazorBindings.Maui
{
    public static class AttachedPropertyRegistry
    {
        internal static readonly Dictionary<string, Action<BindableObject, object>> AttachedPropertyHandlers = new();

        public static void RegisterAttachedPropertyHandler(string propertyName, Action<BindableObject, object> handler)
        {
            AttachedPropertyHandlers[propertyName] = handler;
        }
    }
}
