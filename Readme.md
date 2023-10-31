# BlazorBindings.Avalonia

[![Nuget](https://img.shields.io/nuget/v/BlazorBindings.Avalonia)](https://www.nuget.org/packages/BlazorBindings.Avalonia/)

## What is it?

Avalonia Blazor Bindings enable developers to build Avalonia apps using C# and .NET for Android, iOS, Windows, macOS, and Tizen using familiar web programming patterns. This means you can use the Blazor programming model and Razor syntax to define UI components and behaviors of an application. The UI components that are included are based on Avalonia UI controls, which results in beautiful mobile apps.

Here is a sample Counter component that renders native UI, which may look familiar to Blazor developers, that increments a value on each button press:

```xml
<StackPanel>
    <Label FontSize="30">You pressed @count times </Label>
    <Button Text="+1" OnClick="@HandleClick" />
</StackPanel>

@code {
    int count;

    void HandleClick()
    {
        count++;
    }
}
```

Notice that the Blazor model is present with code sitting side by side the user interface markup that leverages Razor syntax with mobile specific components. This will feel very natural for any web developer that has ever used Razor syntax in the past. Now with the Experimental Avalonia Blazor Bindings you can leverage your existing web skills and knowledge to build Avalonia apps using C# and .NET for Android, iOS, Windows, macOS, and Tizen.

<!--Here is the code above running in the Android Emulator:

<img src="https://devblogs.microsoft.com/aspnet/wp-content/uploads/sites/16/2020/01/blazor-android-counter-2.gif" alt="Clicking increment button in Android emulator" width="300" height="533" class="aligncenter size-full wp-image-23061" />
-->

## About this repository

This repository is a fork of Deamescapers's [Experimental MobileBlazorBindings](https://github.com/DreamEscaper/MobileBlazorBindings), which I decided to fork and maintain separately. If at any point of time Avalonia developers decide to push that repository moving forward, I'll gladly contribute all of my changes to the original repository. 

# Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant
to clarify expected behavior in our community.

For more information, see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

Thank you!

 [1]: https://dotnet.microsoft.com/download/dotnet-core/3.1
