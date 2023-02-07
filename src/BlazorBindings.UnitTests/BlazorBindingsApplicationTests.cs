using BlazorBindings.Maui;
using BlazorBindings.UnitTests.Components;
using Microsoft.AspNetCore.Components;
using NUnit.Framework;

namespace BlazorBindings.UnitTests
{
    public class BlazorBindingsApplicationTests
    {
        [Test]
        public void SetsTheMainPage_ContentPage()
        {
            var application = CreateApplication<PageContent>();
            PageContent.ValidateContent(application.MainPage);
        }


        private BlazorBindingsApplication<T> CreateApplication<T>() where T : IComponent
        {
            return new BlazorBindingsApplication<T>(TestServiceProvider.Create());
        }
    }
}
