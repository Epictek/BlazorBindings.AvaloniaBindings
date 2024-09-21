# 🪢 BlazorBindings.Avalonia

[![Nuget](https://img.shields.io/nuget/v/BlazorBindings.Avalonia)](https://www.nuget.org/packages/BlazorBindings.Avalonia/)

## ⏱️ TL;DR
- Use <a href="https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor">⚡ Blazor</a> syntax for <a href="https://avaloniaui.net/">Avalonia</a> apps
- 😎 Simpler syntax than XAML
- 🪄 IntelliSense support
- Get free <a href="https://devblogs.microsoft.com/dotnet/introducing-net-hot-reload/">🔥 Hot Reload</a> support on-top
- Still  🧪 experimental

## 🤔 What Is It?

This library enables developers to build **native <a href="https://avaloniaui.net/">Avalonia</a> apps** using the .NET's **<a href="https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor">Blazor</a> UI model**.  
This means you can use the **Blazor syntax** to write and use Avalonia UI components and pages. If you used Blazor or Razor in the past, this will look very familiar.

This library **wraps native Avalonia's UI controls** and exposes them as **Blazor components**, so
- 🚫 ***no* hybrid HTML stuff**, but 
- 🤩 **real Avalonia UI controls**

As Avalonia is **cross-platform**, this 
- enables you to write beautiful 💻 **desktop, 📱 mobile and 🌐 web apps**
- **for every major platform** out there (yes, also 🐧 Linux)
- with the **same 🏁 pixel-perfect look on every platform**

And as this library builds on-top of the same foundation as the regular Blazor implementation, **Visual Studio's  🪄 IntelliSense works out-of-the-box**!

## 🔬 Example: Counter Component
This is an example on how you use the Blazor UI model to create a **component** (aka. "Blazor UI control").

This is `Counter.razor`, a **Counter** Blazor UI component that **renders native Avalonia UI controls**.  
This component
- shows a `Label` stating how often the `Button` beneath was pressed,
- shows a `CheckBox` to toggle the visibility of the `Button`, and
- the `Button` that increments the value on each button press.

```razor
<StackPanel>
    <Label FontSize="30">You pressed @count times </Label>
    <CheckBox @bind-IsChecked="showButton">Button visible</CheckBox>
    @if (showButton)
    {
        <Button Text="+1" OnClick="@HandleClick" />
    }
</StackPanel>

@code {
    int count;
    bool showButton = true;

    void HandleClick()
    {
        count++;
    }
}
```

The UI markup uses the **Blazor/Razor syntax** with **Avalonia specific wrapper components** `StackPanel`, `Label`, `CheckBox` and `Button`. This is followed by **C# code** in the **`@code` section** which defines the variables and the click-handler method that increments the counter [^1].  

### ↔️ Binding
 For ➡️ **1-way binding**, Blazor only requires the `@<variable-name>` expression that automatically updates - here the `Label`'s text on every counter update.  
 The **`@bind-` prefix** is used only if ↔️ **2-way binding** is required like on the `CheckBox` here.

 For more advanced bindings and a more complete picture please have a look at the <a href="https://learn.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-8.0">official Blazor documents</a>.

### ⤵️ Conditionals
This code also showcases the use of a regualar `if` statement that adds or removes the Button from the UI tree.
 

> [!NOTE]
> Unlike **XAML**, there is ***no* verbose and complex data-binding syntax** but just a **straight-forward use of variables and methods**.
> Also, Blazor supports real conditionals that allows you to actually add and remove parts of the UI from the UI tree, while XAML only supports hiding.

## 🔬 Example: MainPage View
This is an example on how you use the Blazor UI model to create a **page**.

This is `MainPage.razor` page shows the current time and embedds the previous `Counter.razor` component.

```razor
@page "/"
<StackPanel>
    <Label FontSize="30" Text="@time"></Label>
    <Counter />
</StackPanel>

@code {
    string time = DateTime.Now.ToString();
}
```

As you might already noted, this looks very familiar like a standard component - and this is by design. Only the name and the `@page "/"` declaration give hints that this should be used as a page.  
The `"/"` part is a route. It is useful if you want use routing in your application and paths like this can be used for navigating from one page to another.

> [!TIP]
> For a (somewhat) complete example please look at the `MainPage.razor` and `SubPage.razor` pages in `BlazorBindings.Avalonia.HelloWorld` sample.

## ⚡ Blazor
Blazor was originally a technology for interactive web apps. But the authors imagined from the start that it could also be used on-top of any UI framework. This architecture allows us to use Blazor to drive Avalonia controls.

### 🔥 Sweet Extra: Hot Reload
As this library builds on the standard Blazor building blocks, this comes with free support of **<a href="https://devblogs.microsoft.com/dotnet/introducing-net-hot-reload/">Hot Reload</a>**. This means you can make code or UI changes while your app is running.

To see how Hot Reload in action, here's a video of how well it integrates in .NET applications which also in general applies to the support in this library:  

<a href="https://www.youtube.com/watch?v=H5vVVyrqdH8">📺 Hot Reload in .NET 6 In 10 Minutes or Less</a>

## 📦 Using This Repository
### 🛠️ Building
- Open `BlazorBindings.Avalonia.sln` in Visual Studio 2022
- Build solution

### 🪛 (Re-)Generate Blazor Wrappers
Just run `BlazorBindings.Avalonia.ComponentGenerator` - all wrapper classes in `BlazorBindings.Avalonia` get updated.

#### 🌟 Register A New Avalonia Control With The Generator
- Open `src/BlazorBindings.Avalonia/AttributeInfo.cs`
- Add new `GenerateComponent` attribute for new UI controls that are not yet supported
- Run the generator

```csharp
// Generate `Button` wrapper without further special customizations
[assembly: GenerateComponent(typeof(Button))]

// Generate `ContentControl` wrapper with 2 properties marked as accepting Blazor templates aka. `RenderFragment`s.
[assembly: GenerateComponent(typeof(ContentControl),
    ContentProperties = new[]
    {
        nameof(ContentControl.Content),
        nameof(ContentControl.ContentTemplate)
    })]
```

### ✍️ Blazorize Your Own Avalonia Controls
If you use **3rd party Avalonia controls** or have **self-made Avalonia controls**, you can write a Blazor wrapper class yourself **by hand** - you don't need the generator for this.

1) Ensure the **Avalonia base class** of your component is **already blazorized** - if not, handle that one first following these steps
2) Create a class **named like your Avalonia control**, eg. `Button`
3) **Inherit** it from the ***Blazor* component equivalent** your Avalonia control inherits from
4) **Add properties** for each Avalonia property named as in Avalonia
5) Add the **`[Parameter]` attribute** to the property
6) Use the **actual property type** like `Thickness` but not `StyledProperty<Thickness>` - although if it is a **template property** like `ContentControl`'s `Content` property then use **`RenderFragment`** as its type
7) Add a `CreateNativeElement()` method that returns a new Avalonia control that this Blazor component should wrap
8) Override `HandleParameter(string name, object value)` to map the native value of a property to its Blazor counterpart and also set it on the native control
9) If you have a `RenderFragment` or attached properties, please follow the tips below 


> [!TIP]
> If you have a `RenderFragment` property, you also must override `RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)`.  
> Please refer to this library's components also using `RenderFragment`s like `ContentControl` or `ItemsControl` to see what `RenderTreeBuilderHelper` method you should call.

> [!TIP]
> If you have attached properties, you can register them by adding them to the static constructor.  
> Please refer to this library's components also using them like `Grid` or `Canvas`, especially the `RegisterAdditionalHandlers()` method found in `<component-name>.generated.attachments.cs`.

#### 🔌 Example: Blazorize Avalonia's Button
This simplified example is taken from this repository's generated `Button` Blazor component.

We use the `AC` namespace alias for `Avalonia.Controls` to make it easier to differenciate between `Avalonia.Controls.Button` and the current ***Blazor*** `Button` class we create. So all types prefixed with `AC` are the native Avalonia types.

```csharp
using System.Windows.Input;
using AC = Avalonia.Controls;

/// <summary>
/// A standard button control.
/// </summary>
public partial class Button : ContentControl
{
    static Button()
    {
        RegisterAdditionalHandlers();
    }

    /// <summary>
    /// Gets or sets a value indicating how the <see cref="T:Avalonia.Controls.Button" /> should react to clicks.
    /// </summary>
    [Parameter] public AC.ClickMode? ClickMode { get; set; }

    ...

    [Parameter] public EventCallback<global::Avalonia.Interactivity.RoutedEventArgs> OnClick { get; set; }

    public new AC.Button NativeControl => (AC.Button)((AvaloniaObject)this).NativeControl;

    protected override AC.Button CreateNativeElement() => new();

    protected override void HandleParameter(string name, object value)
    {
        switch (name)
        {
            case nameof(ClickMode):
                if (!Equals(ClickMode, value))
                {
                    ClickMode = (AC.ClickMode?)value;
                    NativeControl.ClickMode = ClickMode ?? (AC.ClickMode)AC.Button.ClickModeProperty.GetDefaultValue(AC.Button.ClickModeProperty.OwnerType);
                }
                break;
            
            ...

            case nameof(OnClick):
                if (!Equals(OnClick, value))
                {
                    void NativeControlClick(object sender, global::Avalonia.Interactivity.RoutedEventArgs e) => InvokeEventCallback(OnClick, e);

                    OnClick = (EventCallback<global::Avalonia.Interactivity.RoutedEventArgs>)value;
                    NativeControl.Click -= NativeControlClick;
                    NativeControl.Click += NativeControlClick;
                }
                break;

            default:
                base.HandleParameter(name, value);
                break;
        }
    }

    protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
    {
        base.RenderAdditionalElementContent(builder, ref sequence);

        // If the control has a `RenderFragment`, here is the place to hook this up to the rendering tree - see `ContentControl.generated.cs` for how this can be done.
    }

    static partial void RegisterAdditionalHandlers()
    {
        // Used for registering attached properties - see `Grid.generated.attachments.cs` for how that can be done
    }
}
```

## ℹ️ About this repository

This repository is a fork of Deamescapers's [Experimental MobileBlazorBindings](https://github.com/DreamEscaper/MobileBlazorBindings), which I decided to fork and maintain separately. If at any point of time Avalonia developers decide to push that repository moving forward, I'll gladly contribute all of my changes to the original repository. 

# 🤝 Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant
to clarify expected behavior in our community.

For more information, see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

Thank you!

[^1]: You can also use a code-behind file, eg. for Blazor component `Foo.razor` you can add a `Foo.razor.cs` file. More details can be found in <a href="https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-8.0#partial-class-support">Blazor documentation</a>.