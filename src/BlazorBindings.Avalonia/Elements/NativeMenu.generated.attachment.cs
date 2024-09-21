

namespace BlazorBindings.Avalonia.Elements
{
    
    internal static class NativeMenuInitializer
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("NativeMenu.Menu",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.NativeMenu.MenuProperty);
                    }
                    else
                    {
                        AC.NativeMenu.SetMenu((AvaloniaBindableObject)element, (AC.NativeMenu)value);
                    }
                });
        }
    }

    public static class NativeMenuExtensions
    {
        public static AvaloniaObject NativeMenuMenu(this AvaloniaObject element, AC.NativeMenu value)
        {
            element.AttachedProperties["NativeMenu.Menu"] = value;
        
            return element;
        }
    }

    public class NativeMenu_Attachment : NativeControlComponentBase, INonPhysicalChild, IContainerElementHandler
    {
        [Parameter] public AC.NativeMenu Menu { get; set; }

        private AvaloniaBindableObject _parent;

        public object TargetElement => _parent;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                var value = parameterValue.Value;
                switch (parameterValue.Name)
                {
                    case nameof(Menu):
                    if (!Equals(Menu, value))
                    {
                        Menu = (AC.NativeMenu)value;
                        //NativeControl.MenuProperty = Menu;
                    }
                    break;

                }
            }
        
            TryUpdateParent(_parent);
            return base.SetParametersAsync(ParameterView.Empty);
        }

        private void TryUpdateParent(object parentElement)
        {
            if (parentElement is not null)
            {
                if (Menu == AC.NativeMenu.MenuProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.NativeMenu.MenuProperty);
                }
                else
                {
                    AC.NativeMenu.SetMenu((AvaloniaBindableObject)parentElement, Menu);
                }
                
            }
        }
    
        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Menu = Menu != default ? Menu : AC.NativeMenu.MenuProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = (AvaloniaBindableObject)parentElement;
        }
        
        
        public void RemoveFromParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Menu = AC.NativeMenu.MenuProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = null;
        }

        public void AddChild(object child, int physicalSiblingIndex)
        {
        }

        public void RemoveChild(object child, int physicalSiblingIndex)
        {
        }

        protected override void RenderAdditionalElementContent(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder, ref int sequence)
        {
            base.RenderAdditionalElementContent(builder, ref sequence);
        }
    }
}
