// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class WebView : View
    {
        static WebView()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public CookieContainer Cookies { get; set; }
        [Parameter] public MC.WebViewSource Source { get; set; }

        public new MC.WebView NativeControl => (MC.WebView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new MC.WebView();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(Cookies):
                    if (!Equals(Cookies, value))
                    {
                        Cookies = (CookieContainer)value;
                        NativeControl.Cookies = Cookies;
                    }
                    break;
                case nameof(Source):
                    if (!Equals(Source, value))
                    {
                        Source = (MC.WebViewSource)value;
                        NativeControl.Source = Source;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        static partial void RegisterAdditionalHandlers();
    }
}
