// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using AC = AlohaKit.Controls;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.AlohaKit
{
    public partial class CheckBox : BlazorBindings.Maui.Elements.GraphicsView
    {
        static CheckBox()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public AC.CheckBoxDrawable CheckBoxDrawable { get; set; }
        [Parameter] public bool IsChecked { get; set; }
        [Parameter] public double StrokeThickness { get; set; }
        [Parameter] public EventCallback<bool> IsCheckedChanged { get; set; }

        public new AC.CheckBox NativeControl => (AC.CheckBox)((Element)this).NativeControl;

        protected override MC.Element CreateNativeElement() => new AC.CheckBox();

        protected override void HandleParameter(string name, object value)
        {
            switch (name)
            {
                case nameof(CheckBoxDrawable):
                    if (!Equals(CheckBoxDrawable, value))
                    {
                        CheckBoxDrawable = (AC.CheckBoxDrawable)value;
                        NativeControl.CheckBoxDrawable = CheckBoxDrawable;
                    }
                    break;
                case nameof(IsChecked):
                    if (!Equals(IsChecked, value))
                    {
                        IsChecked = (bool)value;
                        NativeControl.IsChecked = IsChecked;
                    }
                    break;
                case nameof(StrokeThickness):
                    if (!Equals(StrokeThickness, value))
                    {
                        StrokeThickness = (double)value;
                        NativeControl.StrokeThickness = StrokeThickness;
                    }
                    break;
                case nameof(IsCheckedChanged):
                    if (!Equals(IsCheckedChanged, value))
                    {
                        void NativeControlCheckedChanged(object sender, MC.CheckedChangedEventArgs e) => IsCheckedChanged.InvokeAsync(NativeControl.IsChecked);

                        IsCheckedChanged = (EventCallback<bool>)value;
                        NativeControl.CheckedChanged -= NativeControlCheckedChanged;
                        NativeControl.CheckedChanged += NativeControlCheckedChanged;
                    }
                    break;

                default:
                    base.HandleParameter(name, value);
                    break;
            }
        }

        static partial void RegisterAdditionalHandlers();
    }
}
