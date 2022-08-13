// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class TabBar : ShellItem
    {
        static TabBar()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.TabBar NativeControl => (MC.TabBar)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.TabBar();


        static partial void RegisterAdditionalHandlers();
    }
}
