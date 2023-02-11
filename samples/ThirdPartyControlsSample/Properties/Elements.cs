using AlohaKit.Controls;
using BlazorBindings.Maui.ComponentGenerator;
using XCalendar.Maui.Views;

// AlohaKit
[assembly: GenerateComponent(typeof(Rating))]
[assembly: GenerateComponent(typeof(BusyIndicator))]
[assembly: GenerateComponent(typeof(Avatar))]
[assembly: GenerateComponent(typeof(AlohaKit.Controls.Button), Aliases = new[] { "Button:AlhButton" })]
[assembly: GenerateComponent(typeof(AlohaKit.Controls.CheckBox), Aliases = new[] { "CheckBox:AlhCheckBox" })]
[assembly: GenerateComponent(typeof(AlohaKit.Controls.ProgressBar), Aliases = new[] { "ProgressBar:AlhProgressBar" })]
[assembly: GenerateComponent(typeof(NumericUpDown))]
[assembly: GenerateComponent(typeof(ProgressRadial))]
[assembly: GenerateComponent(typeof(ToggleSwitch),
    Exclude = new[] { nameof(ToggleSwitch.Toggled) },
    PropertyChangedEvents = new[] { nameof(ToggleSwitch.IsOn) })]
[assembly: GenerateComponent(typeof(AlohaKit.Controls.Slider), Aliases = new[] { "Slider:AlhSlider" })]
[assembly: GenerateComponent(typeof(PulseIcon))]

// CommunityToolkit
[assembly: GenerateComponent(typeof(CommunityToolkit.Maui.Views.AvatarView), Exclude = new[] { nameof(CommunityToolkit.Maui.Views.AvatarView.CornerRadius) })]
[assembly: GenerateComponent(typeof(CommunityToolkit.Maui.Views.DrawingView))]
[assembly: GenerateComponent(typeof(CommunityToolkit.Maui.Views.Popup), Exclude = new[] { nameof(CommunityToolkit.Maui.Views.Popup.Anchor) })]

// XCalendar
[assembly: GenerateComponent(typeof(CalendarView),
    GenericProperties = new[] {
        $"{nameof(CalendarView.DayNameTemplate)}:XCalendar.Core.Interfaces.ICalendarDay",
        $"{nameof(CalendarView.DayTemplate)}:XCalendar.Core.Interfaces.ICalendarDay",
    })]