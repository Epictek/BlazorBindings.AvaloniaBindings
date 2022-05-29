using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Linq;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class RadioButtonGroupHandler : StackLayoutHandler
    {
        private readonly string _groupId = Guid.NewGuid().ToString();
        private ulong _selectedValueChangedEventId;

        public RadioButtonGroupHandler(NativeComponentRenderer renderer, XF.StackLayout stackLayoutControl) : base(renderer, stackLayoutControl)
        {
            Initialize();
        }

        public void Initialize()
        {
            XF.RadioButtonGroup.SetGroupName(StackLayoutControl, _groupId);

            StackLayoutControl.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == "SelectedValue" && _selectedValueChangedEventId != default)
                {
                    Renderer.Dispatcher.InvokeAsync(() => Renderer.DispatchEventAsync(_selectedValueChangedEventId, null, new ChangeEventArgs
                    {
                        Value = XF.RadioButtonGroup.GetSelectedValue(StackLayoutControl)
                    }));
                }
            };

            ConfigureEvent("onselectedvaluechanged",
                setId: id => _selectedValueChangedEventId = id,
                clearId: id => { if (_selectedValueChangedEventId == id) { _selectedValueChangedEventId = 0; } });
        }

        public override void SetParent(XF.Element parent)
        {
            base.SetParent(parent);
        }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(RadioButtonGroup<object>.SelectedValue):
                    var selectedValue = AttributeHelper.DelegateToObject<object>(attributeValue);
                    XF.RadioButtonGroup.SetSelectedValue(StackLayoutControl, selectedValue);

                    var radio = StackLayoutControl.Children.OfType<XF.RadioButton>().FirstOrDefault(r => Equals(r.Value, selectedValue));
                    if (radio != null)
                        radio.IsChecked = true;

                    break;

                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
