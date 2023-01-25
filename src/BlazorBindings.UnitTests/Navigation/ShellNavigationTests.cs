// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.UnitTests.Components;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.UnitTests.Navigation
{
    public class ShellNavigationTests
    {
        private readonly Maui.Navigation _navigationService;
        private readonly MC.INavigation _mauiNavigation;

        public ShellNavigationTests()
        {
            var shell = new MC.Shell { Items = { new MC.ContentPage { Title = "Root" } } };
            var sp = TestServiceProvider.Create();
            MC.Application.Current = new TestApplication(sp) { MainPage = shell };
            _navigationService = new Maui.Navigation(sp);
            _mauiNavigation = shell.Navigation;
        }

        [Test]
        public async Task NavigateToPageWithUrlParameters()
        {
            var title = "TestTitle123";

            await _navigationService.NavigateToAsync($"/test/path/{title}");

            var mauiPage = _mauiNavigation.NavigationStack.Last();
            Assert.That(mauiPage.Title, Is.EqualTo(title));
            PageWithUrl.ValidateContent(mauiPage);
        }

        [Test]
        public void ShouldFailIfRouteNotFound()
        {
            Assert.That(() => _navigationService.NavigateToAsync("/non/existing/route"),
                Throws.InvalidOperationException.With.Message.Contains("not registered"));
        }
    }
}
