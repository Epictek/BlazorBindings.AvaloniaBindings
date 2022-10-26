// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;
using BlazorBindings.UnitTests.Components;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.UnitTests.Navigation
{
    public class ShellNavigationTests
    {
        private static readonly Guid _testGuid = Guid.NewGuid();

        private readonly NavigationService _navigationService;
        private readonly MC.INavigation _mauiNavigation;

        public ShellNavigationTests()
        {
            var shell = new MC.Shell { Items = { new MC.ContentPage { Title = "Root" } } };
            var sp = TestServiceProvider.Create();
            MC.Application.Current = new TestApplication(sp) { MainPage = shell };
            _navigationService = new NavigationService(sp);
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

        public static IEnumerable<TestCaseData> TryParseTestData
        {
            get
            {
                yield return new TestCaseData("s", typeof(string), "s", true).SetArgDisplayNames("Parse valid string");
                yield return new TestCaseData("5", typeof(int), 5, true).SetArgDisplayNames("Parse valid int");
                yield return new TestCaseData("5", typeof(int?), 5, true).SetArgDisplayNames("Parse valid int?");
                yield return new TestCaseData("invalid text", typeof(int), 0, false).SetArgDisplayNames("Parse invalid int");
                yield return new TestCaseData("2020-05-20", typeof(DateTime), new DateTime(2020, 05, 20), true).SetArgDisplayNames("Parse valid date");
                yield return new TestCaseData("invalid text", typeof(DateTime), new DateTime(), false).SetArgDisplayNames("Parse invalid date");
                yield return new TestCaseData(_testGuid.ToString(), typeof(Guid), _testGuid, true).SetArgDisplayNames("Parse valid GUID");
                yield return new TestCaseData(_testGuid.ToString(), typeof(Guid?), _testGuid, true).SetArgDisplayNames("Parse valid GUID?");
                yield return new TestCaseData("invalid text", typeof(Guid), new Guid(), false).SetArgDisplayNames("Parse invalid GUID");
                yield return new TestCaseData("{'value': '5'}", typeof(object), null, false).SetArgDisplayNames("Parse POCO should find null operation");
            }
        }

        [TestCaseSource(nameof(TryParseTestData))]
        public void TryParseTest(string s, Type type, object expectedResult, bool expectedSuccess)
        {
            var success = NavigationService.TryParse(type, s, out var result);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedResult, result);
                Assert.AreEqual(expectedSuccess, success);
            });
        }
    }
}
