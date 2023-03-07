using BlazorBindings.UnitTests.Components;

namespace BlazorBindings.UnitTests;

public class MauiBlazorBindingsRendererTests
{
    private readonly TestBlazorBindingsRenderer _renderer = (TestBlazorBindingsRenderer)TestBlazorBindingsRenderer.Create();
    private readonly MC.Application _application = new TestApplication();

    public MauiBlazorBindingsRendererTests()
    {
        MC.Application.Current = _application;
    }

    [Test]
    public async Task RenderToApplication_PageContent()
    {
        await _renderer.AddComponent<PageContent>(_application);

        var content = _application.MainPage;
        PageContent.ValidateContent(content);
    }

    [Test]
    public void ShouldThrowExceptionIfHappenedDuringSyncRender()
    {
        void action() => _ = _renderer.AddComponent<ComponentWithException>(_application);

        Assert.That(action, Throws.InvalidOperationException.With.Message.EqualTo("Should fail here."));
    }

    [Test]
    public async Task RendererShouldHandleAsyncExceptions()
    {
        _renderer.ThrowExceptions = false;

        await _renderer.AddComponent<PageWithButtonWithExceptionOnClick>(_application);
        var button = (MC.Button)((MC.ContentPage)_application.MainPage).Content;
        button.SendClicked();

        Assert.That(() => _renderer.Exceptions, Is.Not.Empty.After(1000, 10));
        Assert.That(_renderer.Exceptions[0].Message, Is.EqualTo("HandleExceptionTest"));
    }

    [Test]
    public async Task RenderedComponentShouldBeAbleToReplaceMainPage()
    {
        await _renderer.AddComponent(typeof(SwitchablePages), _application);

        Assert.That(_application.MainPage.Title, Is.EqualTo("Page1"));

        var switchButton = (MC.Button)((MC.ContentPage)_application.MainPage).Content;
        switchButton.SendClicked();

        Assert.That(_application.MainPage.Title, Is.EqualTo("Page2"));

        switchButton = (MC.Button)((MC.ContentPage)_application.MainPage).Content;
        switchButton.SendClicked();

        Assert.That(_application.MainPage.Title, Is.EqualTo("Page1"));
    }
}
