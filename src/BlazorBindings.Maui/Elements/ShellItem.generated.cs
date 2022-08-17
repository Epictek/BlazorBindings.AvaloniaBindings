// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ShellItem : ShellGroupItem
    {
        static ShellItem()
        {
            RegisterAdditionalHandlers();
        }

        public new MC.ShellItem NativeControl => (MC.ShellItem)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.ShellItem();


        static partial void RegisterAdditionalHandlers();
    }
}
