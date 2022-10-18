using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class RadioButtonGroup<T> : StackLayout
    {
        [Parameter] public T SelectedValue { get; set; }
        [Parameter] public EventCallback<T> SelectedValueChanged { get; set; }

        private readonly string _groupId = Guid.NewGuid().ToString();

        protected override MC.Element CreateNativeElement()
        {
            var stackLayout = new MC.StackLayout();
            MC.RadioButtonGroup.SetGroupName(stackLayout, _groupId);
            return stackLayout;
        }

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(SelectedValue):
                    if (!Equals(SelectedValue, value))
                    {
                        SelectedValue = (T)value;
                        MC.RadioButtonGroup.SetSelectedValue(NativeControl, SelectedValue);
                    }
                    break;

                case nameof(SelectedValueChanged):
                    if (!Equals(SelectedValueChanged, value))
                    {
                        void NativeControlPropertyChanged(object sender, PropertyChangedEventArgs e)
                        {
                            if (e.PropertyName == "SelectedValue")
                            {
                                SelectedValueChanged.InvokeAsync((T)MC.RadioButtonGroup.GetSelectedValue(NativeControl));
                            }
                        }

                        SelectedValueChanged = (EventCallback<T>)value;
                        NativeControl.PropertyChanged -= NativeControlPropertyChanged;
                        NativeControl.PropertyChanged += NativeControlPropertyChanged;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }
    }
}
