

namespace BlazorBindings.Avalonia.Elements
{
    
    internal static class RelativePanelInitializer
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.Above",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AboveProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAbove((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignBottomWithPanel",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignBottomWithPanelProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignBottomWithPanel((AvaloniaBindableObject)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignBottomWith",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignBottomWithProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignBottomWith((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignHorizontalCenterWithPanel",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignHorizontalCenterWithPanelProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignHorizontalCenterWithPanel((AvaloniaBindableObject)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignHorizontalCenterWith",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignHorizontalCenterWithProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignHorizontalCenterWith((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignLeftWithPanel",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignLeftWithPanelProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignLeftWithPanel((AvaloniaBindableObject)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignLeftWith",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignLeftWithProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignLeftWith((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignRightWithPanel",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignRightWithPanelProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignRightWithPanel((AvaloniaBindableObject)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignRightWith",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignRightWithProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignRightWith((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignTopWithPanel",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignTopWithPanelProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignTopWithPanel((AvaloniaBindableObject)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignTopWith",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignTopWithProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignTopWith((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignVerticalCenterWithPanel",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignVerticalCenterWithPanelProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignVerticalCenterWithPanel((AvaloniaBindableObject)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.AlignVerticalCenterWith",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.AlignVerticalCenterWithProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetAlignVerticalCenterWith((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.Below",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.BelowProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetBelow((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.LeftOf",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.LeftOfProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetLeftOf((AvaloniaBindableObject)element, (object)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("RelativePanel.RightOf",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.RelativePanel.RightOfProperty);
                    }
                    else
                    {
                        AC.RelativePanel.SetRightOf((AvaloniaBindableObject)element, (object)value);
                    }
                });
        }
    }

    public static class RelativePanelExtensions
    {
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AboveProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAbove(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.Above"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignBottomWithPanelProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignBottomWithPanel(this AvaloniaObject element, bool value)
        {
            element.AttachedProperties["RelativePanel.AlignBottomWithPanel"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignBottomWithProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignBottomWith(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.AlignBottomWith"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignHorizontalCenterWithPanelProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignHorizontalCenterWithPanel(this AvaloniaObject element, bool value)
        {
            element.AttachedProperties["RelativePanel.AlignHorizontalCenterWithPanel"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignHorizontalCenterWithProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignHorizontalCenterWith(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.AlignHorizontalCenterWith"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignLeftWithPanelProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignLeftWithPanel(this AvaloniaObject element, bool value)
        {
            element.AttachedProperties["RelativePanel.AlignLeftWithPanel"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignLeftWithProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignLeftWith(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.AlignLeftWith"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignRightWithPanelProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignRightWithPanel(this AvaloniaObject element, bool value)
        {
            element.AttachedProperties["RelativePanel.AlignRightWithPanel"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignRightWithProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignRightWith(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.AlignRightWith"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignTopWithPanelProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignTopWithPanel(this AvaloniaObject element, bool value)
        {
            element.AttachedProperties["RelativePanel.AlignTopWithPanel"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignTopWithProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignTopWith(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.AlignTopWith"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignVerticalCenterWithPanelProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignVerticalCenterWithPanel(this AvaloniaObject element, bool value)
        {
            element.AttachedProperties["RelativePanel.AlignVerticalCenterWithPanel"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignVerticalCenterWithProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelAlignVerticalCenterWith(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.AlignVerticalCenterWith"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.BelowProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelBelow(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.Below"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.LeftOfProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelLeftOf(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.LeftOf"] = value;
        
            return element;
        }
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.RightOfProperty" /> XAML attached property.
        /// </summary>
        public static AvaloniaObject RelativePanelRightOf(this AvaloniaObject element, object value)
        {
            element.AttachedProperties["RelativePanel.RightOf"] = value;
        
            return element;
        }
    }

    public class RelativePanel_Attachment : NativeControlComponentBase, INonPhysicalChild, IContainerElementHandler
    {
        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AboveProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object Above { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignBottomWithPanelProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public bool AlignBottomWithPanel { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignBottomWithProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object AlignBottomWith { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignHorizontalCenterWithPanelProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public bool AlignHorizontalCenterWithPanel { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignHorizontalCenterWithProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object AlignHorizontalCenterWith { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignLeftWithPanelProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public bool AlignLeftWithPanel { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignLeftWithProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object AlignLeftWith { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignRightWithPanelProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public bool AlignRightWithPanel { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignRightWithProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object AlignRightWith { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignTopWithPanelProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public bool AlignTopWithPanel { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignTopWithProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object AlignTopWith { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignVerticalCenterWithPanelProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public bool AlignVerticalCenterWithPanel { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.AlignVerticalCenterWithProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object AlignVerticalCenterWith { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.BelowProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object Below { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.LeftOfProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object LeftOf { get; set; }

        /// <summary>
        /// Identifies the <see cref="F:Avalonia.Controls.RelativePanel.RightOfProperty" /> XAML attached property.
        /// </summary>
        [Parameter] public object RightOf { get; set; }

        private AvaloniaBindableObject _parent;

        public object TargetElement => _parent;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                var value = parameterValue.Value;
                switch (parameterValue.Name)
                {
                    case nameof(Above):
                    if (!Equals(Above, value))
                    {
                        Above = (object)value;
                        //NativeControl.AboveProperty = Above;
                    }
                    break;

                    case nameof(AlignBottomWithPanel):
                    if (!Equals(AlignBottomWithPanel, value))
                    {
                        AlignBottomWithPanel = (bool)value;
                        //NativeControl.AlignBottomWithPanelProperty = AlignBottomWithPanel;
                    }
                    break;

                    case nameof(AlignBottomWith):
                    if (!Equals(AlignBottomWith, value))
                    {
                        AlignBottomWith = (object)value;
                        //NativeControl.AlignBottomWithProperty = AlignBottomWith;
                    }
                    break;

                    case nameof(AlignHorizontalCenterWithPanel):
                    if (!Equals(AlignHorizontalCenterWithPanel, value))
                    {
                        AlignHorizontalCenterWithPanel = (bool)value;
                        //NativeControl.AlignHorizontalCenterWithPanelProperty = AlignHorizontalCenterWithPanel;
                    }
                    break;

                    case nameof(AlignHorizontalCenterWith):
                    if (!Equals(AlignHorizontalCenterWith, value))
                    {
                        AlignHorizontalCenterWith = (object)value;
                        //NativeControl.AlignHorizontalCenterWithProperty = AlignHorizontalCenterWith;
                    }
                    break;

                    case nameof(AlignLeftWithPanel):
                    if (!Equals(AlignLeftWithPanel, value))
                    {
                        AlignLeftWithPanel = (bool)value;
                        //NativeControl.AlignLeftWithPanelProperty = AlignLeftWithPanel;
                    }
                    break;

                    case nameof(AlignLeftWith):
                    if (!Equals(AlignLeftWith, value))
                    {
                        AlignLeftWith = (object)value;
                        //NativeControl.AlignLeftWithProperty = AlignLeftWith;
                    }
                    break;

                    case nameof(AlignRightWithPanel):
                    if (!Equals(AlignRightWithPanel, value))
                    {
                        AlignRightWithPanel = (bool)value;
                        //NativeControl.AlignRightWithPanelProperty = AlignRightWithPanel;
                    }
                    break;

                    case nameof(AlignRightWith):
                    if (!Equals(AlignRightWith, value))
                    {
                        AlignRightWith = (object)value;
                        //NativeControl.AlignRightWithProperty = AlignRightWith;
                    }
                    break;

                    case nameof(AlignTopWithPanel):
                    if (!Equals(AlignTopWithPanel, value))
                    {
                        AlignTopWithPanel = (bool)value;
                        //NativeControl.AlignTopWithPanelProperty = AlignTopWithPanel;
                    }
                    break;

                    case nameof(AlignTopWith):
                    if (!Equals(AlignTopWith, value))
                    {
                        AlignTopWith = (object)value;
                        //NativeControl.AlignTopWithProperty = AlignTopWith;
                    }
                    break;

                    case nameof(AlignVerticalCenterWithPanel):
                    if (!Equals(AlignVerticalCenterWithPanel, value))
                    {
                        AlignVerticalCenterWithPanel = (bool)value;
                        //NativeControl.AlignVerticalCenterWithPanelProperty = AlignVerticalCenterWithPanel;
                    }
                    break;

                    case nameof(AlignVerticalCenterWith):
                    if (!Equals(AlignVerticalCenterWith, value))
                    {
                        AlignVerticalCenterWith = (object)value;
                        //NativeControl.AlignVerticalCenterWithProperty = AlignVerticalCenterWith;
                    }
                    break;

                    case nameof(Below):
                    if (!Equals(Below, value))
                    {
                        Below = (object)value;
                        //NativeControl.BelowProperty = Below;
                    }
                    break;

                    case nameof(LeftOf):
                    if (!Equals(LeftOf, value))
                    {
                        LeftOf = (object)value;
                        //NativeControl.LeftOfProperty = LeftOf;
                    }
                    break;

                    case nameof(RightOf):
                    if (!Equals(RightOf, value))
                    {
                        RightOf = (object)value;
                        //NativeControl.RightOfProperty = RightOf;
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
                if (Above == AC.RelativePanel.AboveProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AboveProperty);
                }
                else
                {
                    AC.RelativePanel.SetAbove((AvaloniaBindableObject)parentElement, Above);
                }
                
                if (AlignBottomWithPanel == AC.RelativePanel.AlignBottomWithPanelProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignBottomWithPanelProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignBottomWithPanel((AvaloniaBindableObject)parentElement, AlignBottomWithPanel);
                }
                
                if (AlignBottomWith == AC.RelativePanel.AlignBottomWithProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignBottomWithProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignBottomWith((AvaloniaBindableObject)parentElement, AlignBottomWith);
                }
                
                if (AlignHorizontalCenterWithPanel == AC.RelativePanel.AlignHorizontalCenterWithPanelProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignHorizontalCenterWithPanelProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignHorizontalCenterWithPanel((AvaloniaBindableObject)parentElement, AlignHorizontalCenterWithPanel);
                }
                
                if (AlignHorizontalCenterWith == AC.RelativePanel.AlignHorizontalCenterWithProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignHorizontalCenterWithProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignHorizontalCenterWith((AvaloniaBindableObject)parentElement, AlignHorizontalCenterWith);
                }
                
                if (AlignLeftWithPanel == AC.RelativePanel.AlignLeftWithPanelProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignLeftWithPanelProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignLeftWithPanel((AvaloniaBindableObject)parentElement, AlignLeftWithPanel);
                }
                
                if (AlignLeftWith == AC.RelativePanel.AlignLeftWithProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignLeftWithProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignLeftWith((AvaloniaBindableObject)parentElement, AlignLeftWith);
                }
                
                if (AlignRightWithPanel == AC.RelativePanel.AlignRightWithPanelProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignRightWithPanelProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignRightWithPanel((AvaloniaBindableObject)parentElement, AlignRightWithPanel);
                }
                
                if (AlignRightWith == AC.RelativePanel.AlignRightWithProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignRightWithProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignRightWith((AvaloniaBindableObject)parentElement, AlignRightWith);
                }
                
                if (AlignTopWithPanel == AC.RelativePanel.AlignTopWithPanelProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignTopWithPanelProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignTopWithPanel((AvaloniaBindableObject)parentElement, AlignTopWithPanel);
                }
                
                if (AlignTopWith == AC.RelativePanel.AlignTopWithProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignTopWithProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignTopWith((AvaloniaBindableObject)parentElement, AlignTopWith);
                }
                
                if (AlignVerticalCenterWithPanel == AC.RelativePanel.AlignVerticalCenterWithPanelProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignVerticalCenterWithPanelProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignVerticalCenterWithPanel((AvaloniaBindableObject)parentElement, AlignVerticalCenterWithPanel);
                }
                
                if (AlignVerticalCenterWith == AC.RelativePanel.AlignVerticalCenterWithProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.AlignVerticalCenterWithProperty);
                }
                else
                {
                    AC.RelativePanel.SetAlignVerticalCenterWith((AvaloniaBindableObject)parentElement, AlignVerticalCenterWith);
                }
                
                if (Below == AC.RelativePanel.BelowProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.BelowProperty);
                }
                else
                {
                    AC.RelativePanel.SetBelow((AvaloniaBindableObject)parentElement, Below);
                }
                
                if (LeftOf == AC.RelativePanel.LeftOfProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.LeftOfProperty);
                }
                else
                {
                    AC.RelativePanel.SetLeftOf((AvaloniaBindableObject)parentElement, LeftOf);
                }
                
                if (RightOf == AC.RelativePanel.RightOfProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.RelativePanel.RightOfProperty);
                }
                else
                {
                    AC.RelativePanel.SetRightOf((AvaloniaBindableObject)parentElement, RightOf);
                }
                
            }
        }
    
        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Above = Above != default ? Above : AC.RelativePanel.AboveProperty.GetDefaultValue(parentType);
                AlignBottomWithPanel = AlignBottomWithPanel != default ? AlignBottomWithPanel : AC.RelativePanel.AlignBottomWithPanelProperty.GetDefaultValue(parentType);
                AlignBottomWith = AlignBottomWith != default ? AlignBottomWith : AC.RelativePanel.AlignBottomWithProperty.GetDefaultValue(parentType);
                AlignHorizontalCenterWithPanel = AlignHorizontalCenterWithPanel != default ? AlignHorizontalCenterWithPanel : AC.RelativePanel.AlignHorizontalCenterWithPanelProperty.GetDefaultValue(parentType);
                AlignHorizontalCenterWith = AlignHorizontalCenterWith != default ? AlignHorizontalCenterWith : AC.RelativePanel.AlignHorizontalCenterWithProperty.GetDefaultValue(parentType);
                AlignLeftWithPanel = AlignLeftWithPanel != default ? AlignLeftWithPanel : AC.RelativePanel.AlignLeftWithPanelProperty.GetDefaultValue(parentType);
                AlignLeftWith = AlignLeftWith != default ? AlignLeftWith : AC.RelativePanel.AlignLeftWithProperty.GetDefaultValue(parentType);
                AlignRightWithPanel = AlignRightWithPanel != default ? AlignRightWithPanel : AC.RelativePanel.AlignRightWithPanelProperty.GetDefaultValue(parentType);
                AlignRightWith = AlignRightWith != default ? AlignRightWith : AC.RelativePanel.AlignRightWithProperty.GetDefaultValue(parentType);
                AlignTopWithPanel = AlignTopWithPanel != default ? AlignTopWithPanel : AC.RelativePanel.AlignTopWithPanelProperty.GetDefaultValue(parentType);
                AlignTopWith = AlignTopWith != default ? AlignTopWith : AC.RelativePanel.AlignTopWithProperty.GetDefaultValue(parentType);
                AlignVerticalCenterWithPanel = AlignVerticalCenterWithPanel != default ? AlignVerticalCenterWithPanel : AC.RelativePanel.AlignVerticalCenterWithPanelProperty.GetDefaultValue(parentType);
                AlignVerticalCenterWith = AlignVerticalCenterWith != default ? AlignVerticalCenterWith : AC.RelativePanel.AlignVerticalCenterWithProperty.GetDefaultValue(parentType);
                Below = Below != default ? Below : AC.RelativePanel.BelowProperty.GetDefaultValue(parentType);
                LeftOf = LeftOf != default ? LeftOf : AC.RelativePanel.LeftOfProperty.GetDefaultValue(parentType);
                RightOf = RightOf != default ? RightOf : AC.RelativePanel.RightOfProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = (AvaloniaBindableObject)parentElement;
        }
        
        
        public void RemoveFromParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Above = AC.RelativePanel.AboveProperty.GetDefaultValue(parentType);
                AlignBottomWithPanel = AC.RelativePanel.AlignBottomWithPanelProperty.GetDefaultValue(parentType);
                AlignBottomWith = AC.RelativePanel.AlignBottomWithProperty.GetDefaultValue(parentType);
                AlignHorizontalCenterWithPanel = AC.RelativePanel.AlignHorizontalCenterWithPanelProperty.GetDefaultValue(parentType);
                AlignHorizontalCenterWith = AC.RelativePanel.AlignHorizontalCenterWithProperty.GetDefaultValue(parentType);
                AlignLeftWithPanel = AC.RelativePanel.AlignLeftWithPanelProperty.GetDefaultValue(parentType);
                AlignLeftWith = AC.RelativePanel.AlignLeftWithProperty.GetDefaultValue(parentType);
                AlignRightWithPanel = AC.RelativePanel.AlignRightWithPanelProperty.GetDefaultValue(parentType);
                AlignRightWith = AC.RelativePanel.AlignRightWithProperty.GetDefaultValue(parentType);
                AlignTopWithPanel = AC.RelativePanel.AlignTopWithPanelProperty.GetDefaultValue(parentType);
                AlignTopWith = AC.RelativePanel.AlignTopWithProperty.GetDefaultValue(parentType);
                AlignVerticalCenterWithPanel = AC.RelativePanel.AlignVerticalCenterWithPanelProperty.GetDefaultValue(parentType);
                AlignVerticalCenterWith = AC.RelativePanel.AlignVerticalCenterWithProperty.GetDefaultValue(parentType);
                Below = AC.RelativePanel.BelowProperty.GetDefaultValue(parentType);
                LeftOf = AC.RelativePanel.LeftOfProperty.GetDefaultValue(parentType);
                RightOf = AC.RelativePanel.RightOfProperty.GetDefaultValue(parentType);

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
