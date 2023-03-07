// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;

namespace BlazorBindings.Maui.Elements;

public static partial class AttributeHelper
{
    public static BindingBase GetBinding<T, TResult>(Func<T, TResult> bindingFunc)
    {
        return new TypedBinding<T, TResult>((item) => (bindingFunc(item), true), null, null);
    }
}
