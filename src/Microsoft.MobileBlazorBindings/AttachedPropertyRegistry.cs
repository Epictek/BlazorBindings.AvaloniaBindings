// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public static class AttachedPropertyRegistry
    {
        internal static readonly Dictionary<string, Action<Element, object>> AttachedPropertyHandlers = new Dictionary<string, Action<Element, object>>();

        public static void RegisterAttachedPropertyHandler(string propertyName, Action<Element, object> handler)
        {
            AttachedPropertyHandlers[propertyName] = handler;
        }
    }
}
