using BlazorBindings.Maui;
using BlazorBindings.UnitTests.Components;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.UnitTests
{
    public class MauiBlazorBindingsRendererTests
    {
        private readonly MauiBlazorBindingsRenderer _renderer = TestBlazorBindingsRenderer.Create();

        public MauiBlazorBindingsRendererTests()
        {
            MC.Application.Current = new TestApplication();
        }

        [TestCase(typeof(MC.ContentView))]
        [TestCase(typeof(MC.ContentPage))]
        [TestCase(typeof(MC.ScrollView))]
        [TestCase(typeof(MC.StackLayout))]
        [TestCase(typeof(MC.VerticalStackLayout))]
        [TestCase(typeof(MC.HorizontalStackLayout))]
        public async Task RenderToExistingControl_NonPageContent(Type containerType)
        {
            var control = (MC.Element)Activator.CreateInstance(containerType);

            await _renderer.AddComponent<NonPageContent>(control);

            var content = GetChildContent(control);
            NonPageContent.ValidateContent(content);
        }

        [TestCase(typeof(MC.Application))]
        [TestCase(typeof(MC.FlyoutPage))]
        [TestCase(typeof(MC.TabbedPage))]
        [TestCase(typeof(MC.Shell))]
        [TestCase(typeof(MC.ShellContent))]
        public async Task RenderToExistingControl_PageContent(Type containerType)
        {
            var control = (MC.Element)Activator.CreateInstance(containerType);

            await _renderer.AddComponent<PageContent>(control);

            var content = GetChildContent(control);
            PageContent.ValidateContent(content);
        }

        private static MC.Element GetChildContent(MC.Element container)
        {
            return container switch
            {
                MC.Application app => app.MainPage,
                MC.ContentPage contentPage => contentPage.Content,
                MC.ContentView contentView => contentView.Content,
                MC.ScrollView scrollView => scrollView.Content,
                MC.StackBase stackBase => (MC.Element)stackBase.Children[0],
                MC.FlyoutPage flyoutPage => flyoutPage.Detail,
                MC.TabbedPage tabbedPage => tabbedPage.Children[0],
                MC.Shell shell => (MC.Element)shell.Items[0].Items[0].Items[0].Content,
                MC.ShellContent shellContent => (MC.Element)shellContent.Content,
                _ => throw new NotSupportedException("Unexpected parent type.")
            };
        }
    }
}
