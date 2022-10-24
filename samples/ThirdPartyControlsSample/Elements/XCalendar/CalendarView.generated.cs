// <auto-generated>
//     This code was generated by a BlazorBindings.Maui component generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.Handlers;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XCalendar.Core.Interfaces;
using XMV = XCalendar.Maui.Views;

namespace BlazorBindings.Maui.Elements.XCalendar
{
    public partial class CalendarView : BlazorBindings.Maui.Elements.ContentView
    {
        static CalendarView()
        {
            ElementHandlerRegistry.RegisterPropertyContentHandler<CalendarView>(nameof(DayNamesTemplate),
                (renderer, parent, component) => new ControlTemplatePropertyHandler<XMV.CalendarView>(component,
                    (x, controlTemplate) => x.DayNamesTemplate = controlTemplate));
            ElementHandlerRegistry.RegisterPropertyContentHandler<CalendarView>(nameof(DayNameTemplate),
                (renderer, parent, component) => new DataTemplatePropertyHandler<XMV.CalendarView, ICalendarDay>(component,
                    (x, dataTemplate) => x.DayNameTemplate = dataTemplate));
            ElementHandlerRegistry.RegisterPropertyContentHandler<CalendarView>(nameof(DaysViewTemplate),
                (renderer, parent, component) => new ControlTemplatePropertyHandler<XMV.CalendarView>(component,
                    (x, controlTemplate) => x.DaysViewTemplate = controlTemplate));
            ElementHandlerRegistry.RegisterPropertyContentHandler<CalendarView>(nameof(DayTemplate),
                (renderer, parent, component) => new DataTemplatePropertyHandler<XMV.CalendarView, ICalendarDay>(component,
                    (x, dataTemplate) => x.DayTemplate = dataTemplate));
            ElementHandlerRegistry.RegisterPropertyContentHandler<CalendarView>(nameof(NavigationViewTemplate),
                (renderer, parent, component) => new ControlTemplatePropertyHandler<XMV.CalendarView>(component,
                    (x, controlTemplate) => x.NavigationViewTemplate = controlTemplate));
            RegisterAdditionalHandlers();
        }

        [Parameter] public double? DayNameHorizontalSpacing { get; set; }
        [Parameter] public double? DayNamesHeightRequest { get; set; }
        [Parameter] public double? DayNameVerticalSpacing { get; set; }
        [Parameter] public IEnumerable<ICalendarDay> Days { get; set; }
        [Parameter] public IList<DayOfWeek> DaysOfWeek { get; set; }
        [Parameter] public double? DaysViewHeightRequest { get; set; }
        [Parameter] public DateTime? NavigatedDate { get; set; }
        [Parameter] public RenderFragment DayNamesTemplate { get; set; }
        [Parameter] public RenderFragment<ICalendarDay> DayNameTemplate { get; set; }
        [Parameter] public RenderFragment DaysViewTemplate { get; set; }
        [Parameter] public RenderFragment<ICalendarDay> DayTemplate { get; set; }
        [Parameter] public RenderFragment NavigationViewTemplate { get; set; }

        public new XMV.CalendarView NativeControl => (XMV.CalendarView)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new XMV.CalendarView();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(DayNameHorizontalSpacing):
                    if (!Equals(DayNameHorizontalSpacing, value))
                    {
                        DayNameHorizontalSpacing = (double?)value;
                        NativeControl.DayNameHorizontalSpacing = DayNameHorizontalSpacing ?? (double)XMV.CalendarView.DayNameHorizontalSpacingProperty.DefaultValue;
                    }
                    break;
                case nameof(DayNamesHeightRequest):
                    if (!Equals(DayNamesHeightRequest, value))
                    {
                        DayNamesHeightRequest = (double?)value;
                        NativeControl.DayNamesHeightRequest = DayNamesHeightRequest ?? (double)XMV.CalendarView.DayNamesHeightRequestProperty.DefaultValue;
                    }
                    break;
                case nameof(DayNameVerticalSpacing):
                    if (!Equals(DayNameVerticalSpacing, value))
                    {
                        DayNameVerticalSpacing = (double?)value;
                        NativeControl.DayNameVerticalSpacing = DayNameVerticalSpacing ?? (double)XMV.CalendarView.DayNameVerticalSpacingProperty.DefaultValue;
                    }
                    break;
                case nameof(Days):
                    if (!Equals(Days, value))
                    {
                        Days = (IEnumerable<ICalendarDay>)value;
                        NativeControl.Days = Days;
                    }
                    break;
                case nameof(DaysOfWeek):
                    if (!Equals(DaysOfWeek, value))
                    {
                        DaysOfWeek = (IList<DayOfWeek>)value;
                        NativeControl.DaysOfWeek = DaysOfWeek;
                    }
                    break;
                case nameof(DaysViewHeightRequest):
                    if (!Equals(DaysViewHeightRequest, value))
                    {
                        DaysViewHeightRequest = (double?)value;
                        NativeControl.DaysViewHeightRequest = DaysViewHeightRequest ?? (double)XMV.CalendarView.DaysViewHeightRequestProperty.DefaultValue;
                    }
                    break;
                case nameof(NavigatedDate):
                    if (!Equals(NavigatedDate, value))
                    {
                        NavigatedDate = (DateTime?)value;
                        NativeControl.NavigatedDate = NavigatedDate ?? (DateTime)XMV.CalendarView.NavigatedDateProperty.DefaultValue;
                    }
                    break;
                case nameof(DayNamesTemplate):
                    DayNamesTemplate = (RenderFragment)value;
                    break;
                case nameof(DayNameTemplate):
                    DayNameTemplate = (RenderFragment<ICalendarDay>)value;
                    break;
                case nameof(DaysViewTemplate):
                    DaysViewTemplate = (RenderFragment)value;
                    break;
                case nameof(DayTemplate):
                    DayTemplate = (RenderFragment<ICalendarDay>)value;
                    break;
                case nameof(NavigationViewTemplate):
                    NavigationViewTemplate = (RenderFragment)value;
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        protected override void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
            RenderTreeBuilderHelper.AddControlTemplateProperty(builder, sequence++, typeof(CalendarView), DayNamesTemplate);
            RenderTreeBuilderHelper.AddDataTemplateProperty(builder, sequence++, typeof(CalendarView), DayNameTemplate);
            RenderTreeBuilderHelper.AddControlTemplateProperty(builder, sequence++, typeof(CalendarView), DaysViewTemplate);
            RenderTreeBuilderHelper.AddDataTemplateProperty(builder, sequence++, typeof(CalendarView), DayTemplate);
            RenderTreeBuilderHelper.AddControlTemplateProperty(builder, sequence++, typeof(CalendarView), NavigationViewTemplate);
        }

        static partial void RegisterAdditionalHandlers();
    }
}
