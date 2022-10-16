// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;
using BlazorBindings.UnitTests.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Dispatching;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.UnitTests.Navigation
{
    [TestFixture(nameof(MC.Shell))]
    [TestFixture(nameof(MC.NavigationPage))]
    public class NonShellNavigationTests
    {
        private readonly NavigationService _navigationService;
        private readonly MC.INavigation _mauiNavigation;
        private readonly MC.Page _rootPage;

        public NonShellNavigationTests(string root)
        {
            var mainPage = root == nameof(MC.Shell)
                ? (MC.Page)new MC.Shell { Items = { new MC.ContentPage { Title = "Root" } } }
                : new MC.NavigationPage(new MC.ContentPage { Title = "Root" });

            var sp = TestServiceProvider.Create();
            MC.Application.Current = new TestApplication(sp) { MainPage = mainPage };

            var ctx = MC.Application.Current.Handler.MauiContext;
            var dsp = ctx.Services.GetService<IDispatcher>();

            _navigationService = new NavigationService(sp);
            _mauiNavigation = mainPage.Navigation;
            _rootPage = _mauiNavigation.NavigationStack[0];
        }

        [Test]
        public async Task PushAsyncWithParameters()
        {
            var par1 = "Test6543";
            var par2 = 3242342;

            await _navigationService.PushAsync<PageContentWithParameters>(new()
            {
                [nameof(PageContentWithParameters.Par1)] = par1,
                [nameof(PageContentWithParameters.Par2)] = par2
            });

            var navigatedPage = _mauiNavigation.NavigationStack.Last();
            PageContentWithParameters.ValidateContent(navigatedPage, par1, par2);
        }

        [Test]
        public async Task PushAsyncWithParameters_RenderFragment()
        {
            var par1 = "Test6543";
            var par2 = 3242342;
            var renderFragment = NavigationRenderFragments.PageWithParameters(par1, par2);

            await _navigationService.PushAsync(renderFragment);

            var navigatedPage = _mauiNavigation.NavigationStack.Last();
            PageContentWithParameters.ValidateContent(navigatedPage, par1, par2);
        }

        [Test]
        public async Task PushModalAsyncWithParameters()
        {
            var par1 = "Test6543";
            var par2 = 3242342;

            await _navigationService.PushModalAsync<PageContentWithParameters>(new()
            {
                [nameof(PageContentWithParameters.Par1)] = par1,
                [nameof(PageContentWithParameters.Par2)] = par2
            });

            var navigatedPage = _mauiNavigation.ModalStack.Last();
            PageContentWithParameters.ValidateContent(navigatedPage, par1, par2);
        }

        [Test]
        public async Task PushModalAsyncWithParameters_RenderFragment()
        {
            var par1 = "Test6543";
            var par2 = 3242342;
            var renderFragment = NavigationRenderFragments.PageWithParameters(par1, par2);

            await _navigationService.PushModalAsync(renderFragment);

            var navigatedPage = _mauiNavigation.ModalStack.Last();
            PageContentWithParameters.ValidateContent(navigatedPage, par1, par2);
        }

        [Test]
        public async Task PopAsync()
        {
            await _navigationService.PushAsync<PageContent>();

            Assert.That(_mauiNavigation.NavigationStack, Has.Count.EqualTo(2));

            await _navigationService.PopAsync();

            Assert.That(_mauiNavigation.NavigationStack, Has.Count.EqualTo(1));
            Assert.That(_mauiNavigation.NavigationStack[0], Is.EqualTo(_rootPage));
        }

        [Test]
        public async Task PopToRootAsync()
        {
            await _navigationService.PushAsync<PageContent>();
            await _navigationService.PushAsync<PageContentWithParameters>();

            Assert.That(_mauiNavigation.NavigationStack, Has.Count.EqualTo(3));

            await _navigationService.PopToRootAsync();

            Assert.That(_mauiNavigation.NavigationStack, Has.Count.EqualTo(1));
            Assert.That(_mauiNavigation.NavigationStack[0], Is.EqualTo(_rootPage));
        }

        [Test]
        public async Task PopModalAsync()
        {
            await _navigationService.PushModalAsync<PageContent>();

            Assert.That(_mauiNavigation.ModalStack, Has.Count.EqualTo(1));

            await _navigationService.PopModalAsync();

            Assert.That(_mauiNavigation.ModalStack, Has.Count.EqualTo(0));
        }
    }
}
