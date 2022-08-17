// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellSection : ShellGroupItem
    {
        static ShellSection()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.ShellSection NativeControl => (MC.ShellSection)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ShellSection();


        static partial void RegisterAdditionalHandlers();
    }
}
