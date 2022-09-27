using Microsoft.Maui.Automation.Driver;
using NUnit.Framework;

namespace BlazorBindings.Maui.UITests
{
    public class Class1
    {
        [Test]
        public async Task Test()
        {
            var driver = new AppDriverBuilder()
                .AppFilename("C:\\Users\\master\\source\\repos\\MobileBlazorBindings\\samples\\ControlGallery\\bin\\Debug\\net6.0-android\\com.companyname.controlgallery-Signed.apk")
                .Device("pixel_3a_-_api_31")
                .Build();

            await driver.Start();

            var screenshot = "C:\\Users\\master\\source\\repos\\MobileBlazorBindings\\BlazorBindings.Maui.UITests\\test.png";
            await driver.Screenshot(screenshot);
        }
    }
}