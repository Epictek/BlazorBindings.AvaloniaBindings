// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TemplatedPage : Page
    {
        static TemplatedPage()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.TemplatedPage NativeControl => (MC.TemplatedPage)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.TemplatedPage();


        static partial void RegisterAdditionalHandlers();
    }
}
