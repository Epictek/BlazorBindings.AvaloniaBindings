// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBindings.Maui
{
    [Obsolete]
    public class ShellNavigationManager //: NavigationManager I would have liked to inherit from NavigationManager but I can't work out what URIs to initialize it with
    {
        private readonly NavigationService _navigationService;

        public ShellNavigationManager(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void NavigateTo(string uri, Dictionary<string, object> parameters = null)
        {
            _ = NavigateToAsync(uri, parameters);
        }

        public Task NavigateToAsync(string uri, Dictionary<string, object> parameters = null)
        {
            return _navigationService.NavigateToAsync(uri, parameters);
        }
    }
}
