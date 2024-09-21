

namespace BlazorBindings.Avalonia.Elements
{
    
    internal static class ScrollViewerInitializer
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.AllowAutoHide",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.AllowAutoHideProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetAllowAutoHide((AvaloniaPage)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.BringIntoViewOnFocusChange",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.BringIntoViewOnFocusChangeProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetBringIntoViewOnFocusChange((AvaloniaPage)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.HorizontalScrollBarVisibility",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.HorizontalScrollBarVisibilityProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetHorizontalScrollBarVisibility((AvaloniaPage)element, (AC.Primitives.ScrollBarVisibility)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.HorizontalSnapPointsAlignment",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.HorizontalSnapPointsAlignmentProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetHorizontalSnapPointsAlignment((AvaloniaPage)element, (AC.Primitives.SnapPointsAlignment)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.HorizontalSnapPointsType",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.HorizontalSnapPointsTypeProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetHorizontalSnapPointsType((AvaloniaPage)element, (AC.Primitives.SnapPointsType)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.IsDeferredScrollingEnabled",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.IsDeferredScrollingEnabledProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetIsDeferredScrollingEnabled((AvaloniaPage)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.IsScrollChainingEnabled",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.IsScrollChainingEnabledProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetIsScrollChainingEnabled((AvaloniaPage)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.IsScrollInertiaEnabled",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.IsScrollInertiaEnabledProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetIsScrollInertiaEnabled((AvaloniaPage)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.VerticalScrollBarVisibility",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.VerticalScrollBarVisibilityProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetVerticalScrollBarVisibility((AvaloniaPage)element, (AC.Primitives.ScrollBarVisibility)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.VerticalSnapPointsAlignment",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.VerticalSnapPointsAlignmentProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetVerticalSnapPointsAlignment((AvaloniaPage)element, (AC.Primitives.SnapPointsAlignment)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("ScrollViewer.VerticalSnapPointsType",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.ScrollViewer.VerticalSnapPointsTypeProperty);
                    }
                    else
                    {
                        AC.ScrollViewer.SetVerticalSnapPointsType((AvaloniaPage)element, (AC.Primitives.SnapPointsType)value);
                    }
                });
        }
    }

    public static class ScrollViewerExtensions
    {
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.AllowAutoHide" /> property.
        /// </summary>
        public static Control ScrollViewerAllowAutoHide(this Control element, bool value)
        {
            element.AttachedProperties["ScrollViewer.AllowAutoHide"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.BringIntoViewOnFocusChange" /> property.
        /// </summary>
        public static Control ScrollViewerBringIntoViewOnFocusChange(this Control element, bool value)
        {
            element.AttachedProperties["ScrollViewer.BringIntoViewOnFocusChange"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.HorizontalScrollBarVisibility" /> property.
        /// </summary>
        public static Control ScrollViewerHorizontalScrollBarVisibility(this Control element, AC.Primitives.ScrollBarVisibility value)
        {
            element.AttachedProperties["ScrollViewer.HorizontalScrollBarVisibility"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.HorizontalSnapPointsAlignment" /> property.
        /// </summary>
        public static Control ScrollViewerHorizontalSnapPointsAlignment(this Control element, AC.Primitives.SnapPointsAlignment value)
        {
            element.AttachedProperties["ScrollViewer.HorizontalSnapPointsAlignment"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.HorizontalSnapPointsType" /> property.
        /// </summary>
        public static Control ScrollViewerHorizontalSnapPointsType(this Control element, AC.Primitives.SnapPointsType value)
        {
            element.AttachedProperties["ScrollViewer.HorizontalSnapPointsType"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.IsDeferredScrollingEnabled" /> property.
        /// </summary>
        public static Control ScrollViewerIsDeferredScrollingEnabled(this Control element, bool value)
        {
            element.AttachedProperties["ScrollViewer.IsDeferredScrollingEnabled"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.IsScrollChainingEnabled" /> property.
        /// </summary>
        public static Control ScrollViewerIsScrollChainingEnabled(this Control element, bool value)
        {
            element.AttachedProperties["ScrollViewer.IsScrollChainingEnabled"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.IsScrollInertiaEnabled" /> property.
        /// </summary>
        public static Control ScrollViewerIsScrollInertiaEnabled(this Control element, bool value)
        {
            element.AttachedProperties["ScrollViewer.IsScrollInertiaEnabled"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.VerticalScrollBarVisibility" /> property.
        /// </summary>
        public static Control ScrollViewerVerticalScrollBarVisibility(this Control element, AC.Primitives.ScrollBarVisibility value)
        {
            element.AttachedProperties["ScrollViewer.VerticalScrollBarVisibility"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.VerticalSnapPointsAlignment" /> property.
        /// </summary>
        public static Control ScrollViewerVerticalSnapPointsAlignment(this Control element, AC.Primitives.SnapPointsAlignment value)
        {
            element.AttachedProperties["ScrollViewer.VerticalSnapPointsAlignment"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.VerticalSnapPointsType" /> property.
        /// </summary>
        public static Control ScrollViewerVerticalSnapPointsType(this Control element, AC.Primitives.SnapPointsType value)
        {
            element.AttachedProperties["ScrollViewer.VerticalSnapPointsType"] = value;
        
            return element;
        }
    }

    public class ScrollViewer_Attachment : NativeControlComponentBase, INonPhysicalChild, IContainerElementHandler
    {
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.AllowAutoHide" /> property.
        /// </summary>
        [Parameter] public bool AllowAutoHide { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.BringIntoViewOnFocusChange" /> property.
        /// </summary>
        [Parameter] public bool BringIntoViewOnFocusChange { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.HorizontalScrollBarVisibility" /> property.
        /// </summary>
        [Parameter] public AC.Primitives.ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.HorizontalSnapPointsAlignment" /> property.
        /// </summary>
        [Parameter] public AC.Primitives.SnapPointsAlignment HorizontalSnapPointsAlignment { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.HorizontalSnapPointsType" /> property.
        /// </summary>
        [Parameter] public AC.Primitives.SnapPointsType HorizontalSnapPointsType { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.IsDeferredScrollingEnabled" /> property.
        /// </summary>
        [Parameter] public bool IsDeferredScrollingEnabled { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.IsScrollChainingEnabled" /> property.
        /// </summary>
        [Parameter] public bool IsScrollChainingEnabled { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.IsScrollInertiaEnabled" /> property.
        /// </summary>
        [Parameter] public bool IsScrollInertiaEnabled { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.VerticalScrollBarVisibility" /> property.
        /// </summary>
        [Parameter] public AC.Primitives.ScrollBarVisibility VerticalScrollBarVisibility { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.VerticalSnapPointsAlignment" /> property.
        /// </summary>
        [Parameter] public AC.Primitives.SnapPointsAlignment VerticalSnapPointsAlignment { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.ScrollViewer.VerticalSnapPointsType" /> property.
        /// </summary>
        [Parameter] public AC.Primitives.SnapPointsType VerticalSnapPointsType { get; set; }

        private AvaloniaPage _parent;

        public object TargetElement => _parent;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                var value = parameterValue.Value;
                switch (parameterValue.Name)
                {
                    case nameof(AllowAutoHide):
                    if (!Equals(AllowAutoHide, value))
                    {
                        AllowAutoHide = (bool)value;
                        //NativeControl.AllowAutoHideProperty = AllowAutoHide;
                    }
                    break;

                    case nameof(BringIntoViewOnFocusChange):
                    if (!Equals(BringIntoViewOnFocusChange, value))
                    {
                        BringIntoViewOnFocusChange = (bool)value;
                        //NativeControl.BringIntoViewOnFocusChangeProperty = BringIntoViewOnFocusChange;
                    }
                    break;

                    case nameof(HorizontalScrollBarVisibility):
                    if (!Equals(HorizontalScrollBarVisibility, value))
                    {
                        HorizontalScrollBarVisibility = (AC.Primitives.ScrollBarVisibility)value;
                        //NativeControl.HorizontalScrollBarVisibilityProperty = HorizontalScrollBarVisibility;
                    }
                    break;

                    case nameof(HorizontalSnapPointsAlignment):
                    if (!Equals(HorizontalSnapPointsAlignment, value))
                    {
                        HorizontalSnapPointsAlignment = (AC.Primitives.SnapPointsAlignment)value;
                        //NativeControl.HorizontalSnapPointsAlignmentProperty = HorizontalSnapPointsAlignment;
                    }
                    break;

                    case nameof(HorizontalSnapPointsType):
                    if (!Equals(HorizontalSnapPointsType, value))
                    {
                        HorizontalSnapPointsType = (AC.Primitives.SnapPointsType)value;
                        //NativeControl.HorizontalSnapPointsTypeProperty = HorizontalSnapPointsType;
                    }
                    break;

                    case nameof(IsDeferredScrollingEnabled):
                    if (!Equals(IsDeferredScrollingEnabled, value))
                    {
                        IsDeferredScrollingEnabled = (bool)value;
                        //NativeControl.IsDeferredScrollingEnabledProperty = IsDeferredScrollingEnabled;
                    }
                    break;

                    case nameof(IsScrollChainingEnabled):
                    if (!Equals(IsScrollChainingEnabled, value))
                    {
                        IsScrollChainingEnabled = (bool)value;
                        //NativeControl.IsScrollChainingEnabledProperty = IsScrollChainingEnabled;
                    }
                    break;

                    case nameof(IsScrollInertiaEnabled):
                    if (!Equals(IsScrollInertiaEnabled, value))
                    {
                        IsScrollInertiaEnabled = (bool)value;
                        //NativeControl.IsScrollInertiaEnabledProperty = IsScrollInertiaEnabled;
                    }
                    break;

                    case nameof(VerticalScrollBarVisibility):
                    if (!Equals(VerticalScrollBarVisibility, value))
                    {
                        VerticalScrollBarVisibility = (AC.Primitives.ScrollBarVisibility)value;
                        //NativeControl.VerticalScrollBarVisibilityProperty = VerticalScrollBarVisibility;
                    }
                    break;

                    case nameof(VerticalSnapPointsAlignment):
                    if (!Equals(VerticalSnapPointsAlignment, value))
                    {
                        VerticalSnapPointsAlignment = (AC.Primitives.SnapPointsAlignment)value;
                        //NativeControl.VerticalSnapPointsAlignmentProperty = VerticalSnapPointsAlignment;
                    }
                    break;

                    case nameof(VerticalSnapPointsType):
                    if (!Equals(VerticalSnapPointsType, value))
                    {
                        VerticalSnapPointsType = (AC.Primitives.SnapPointsType)value;
                        //NativeControl.VerticalSnapPointsTypeProperty = VerticalSnapPointsType;
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
                if (AllowAutoHide == AC.ScrollViewer.AllowAutoHideProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.AllowAutoHideProperty);
                }
                else
                {
                    AC.ScrollViewer.SetAllowAutoHide((AvaloniaPage)parentElement, AllowAutoHide);
                }
                
                if (BringIntoViewOnFocusChange == AC.ScrollViewer.BringIntoViewOnFocusChangeProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.BringIntoViewOnFocusChangeProperty);
                }
                else
                {
                    AC.ScrollViewer.SetBringIntoViewOnFocusChange((AvaloniaPage)parentElement, BringIntoViewOnFocusChange);
                }
                
                if (HorizontalScrollBarVisibility == AC.ScrollViewer.HorizontalScrollBarVisibilityProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.HorizontalScrollBarVisibilityProperty);
                }
                else
                {
                    AC.ScrollViewer.SetHorizontalScrollBarVisibility((AvaloniaPage)parentElement, HorizontalScrollBarVisibility);
                }
                
                if (HorizontalSnapPointsAlignment == AC.ScrollViewer.HorizontalSnapPointsAlignmentProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.HorizontalSnapPointsAlignmentProperty);
                }
                else
                {
                    AC.ScrollViewer.SetHorizontalSnapPointsAlignment((AvaloniaPage)parentElement, HorizontalSnapPointsAlignment);
                }
                
                if (HorizontalSnapPointsType == AC.ScrollViewer.HorizontalSnapPointsTypeProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.HorizontalSnapPointsTypeProperty);
                }
                else
                {
                    AC.ScrollViewer.SetHorizontalSnapPointsType((AvaloniaPage)parentElement, HorizontalSnapPointsType);
                }
                
                if (IsDeferredScrollingEnabled == AC.ScrollViewer.IsDeferredScrollingEnabledProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.IsDeferredScrollingEnabledProperty);
                }
                else
                {
                    AC.ScrollViewer.SetIsDeferredScrollingEnabled((AvaloniaPage)parentElement, IsDeferredScrollingEnabled);
                }
                
                if (IsScrollChainingEnabled == AC.ScrollViewer.IsScrollChainingEnabledProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.IsScrollChainingEnabledProperty);
                }
                else
                {
                    AC.ScrollViewer.SetIsScrollChainingEnabled((AvaloniaPage)parentElement, IsScrollChainingEnabled);
                }
                
                if (IsScrollInertiaEnabled == AC.ScrollViewer.IsScrollInertiaEnabledProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.IsScrollInertiaEnabledProperty);
                }
                else
                {
                    AC.ScrollViewer.SetIsScrollInertiaEnabled((AvaloniaPage)parentElement, IsScrollInertiaEnabled);
                }
                
                if (VerticalScrollBarVisibility == AC.ScrollViewer.VerticalScrollBarVisibilityProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.VerticalScrollBarVisibilityProperty);
                }
                else
                {
                    AC.ScrollViewer.SetVerticalScrollBarVisibility((AvaloniaPage)parentElement, VerticalScrollBarVisibility);
                }
                
                if (VerticalSnapPointsAlignment == AC.ScrollViewer.VerticalSnapPointsAlignmentProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.VerticalSnapPointsAlignmentProperty);
                }
                else
                {
                    AC.ScrollViewer.SetVerticalSnapPointsAlignment((AvaloniaPage)parentElement, VerticalSnapPointsAlignment);
                }
                
                if (VerticalSnapPointsType == AC.ScrollViewer.VerticalSnapPointsTypeProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.ScrollViewer.VerticalSnapPointsTypeProperty);
                }
                else
                {
                    AC.ScrollViewer.SetVerticalSnapPointsType((AvaloniaPage)parentElement, VerticalSnapPointsType);
                }
                
            }
        }
    
        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                AllowAutoHide = AllowAutoHide != default ? AllowAutoHide : AC.ScrollViewer.AllowAutoHideProperty.GetDefaultValue(parentType);
                BringIntoViewOnFocusChange = BringIntoViewOnFocusChange != default ? BringIntoViewOnFocusChange : AC.ScrollViewer.BringIntoViewOnFocusChangeProperty.GetDefaultValue(parentType);
                HorizontalScrollBarVisibility = HorizontalScrollBarVisibility != default ? HorizontalScrollBarVisibility : AC.ScrollViewer.HorizontalScrollBarVisibilityProperty.GetDefaultValue(parentType);
                HorizontalSnapPointsAlignment = HorizontalSnapPointsAlignment != default ? HorizontalSnapPointsAlignment : AC.ScrollViewer.HorizontalSnapPointsAlignmentProperty.GetDefaultValue(parentType);
                HorizontalSnapPointsType = HorizontalSnapPointsType != default ? HorizontalSnapPointsType : AC.ScrollViewer.HorizontalSnapPointsTypeProperty.GetDefaultValue(parentType);
                IsDeferredScrollingEnabled = IsDeferredScrollingEnabled != default ? IsDeferredScrollingEnabled : AC.ScrollViewer.IsDeferredScrollingEnabledProperty.GetDefaultValue(parentType);
                IsScrollChainingEnabled = IsScrollChainingEnabled != default ? IsScrollChainingEnabled : AC.ScrollViewer.IsScrollChainingEnabledProperty.GetDefaultValue(parentType);
                IsScrollInertiaEnabled = IsScrollInertiaEnabled != default ? IsScrollInertiaEnabled : AC.ScrollViewer.IsScrollInertiaEnabledProperty.GetDefaultValue(parentType);
                VerticalScrollBarVisibility = VerticalScrollBarVisibility != default ? VerticalScrollBarVisibility : AC.ScrollViewer.VerticalScrollBarVisibilityProperty.GetDefaultValue(parentType);
                VerticalSnapPointsAlignment = VerticalSnapPointsAlignment != default ? VerticalSnapPointsAlignment : AC.ScrollViewer.VerticalSnapPointsAlignmentProperty.GetDefaultValue(parentType);
                VerticalSnapPointsType = VerticalSnapPointsType != default ? VerticalSnapPointsType : AC.ScrollViewer.VerticalSnapPointsTypeProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = (AvaloniaPage)parentElement;
        }
        
        
        public void RemoveFromParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                AllowAutoHide = AC.ScrollViewer.AllowAutoHideProperty.GetDefaultValue(parentType);
                BringIntoViewOnFocusChange = AC.ScrollViewer.BringIntoViewOnFocusChangeProperty.GetDefaultValue(parentType);
                HorizontalScrollBarVisibility = AC.ScrollViewer.HorizontalScrollBarVisibilityProperty.GetDefaultValue(parentType);
                HorizontalSnapPointsAlignment = AC.ScrollViewer.HorizontalSnapPointsAlignmentProperty.GetDefaultValue(parentType);
                HorizontalSnapPointsType = AC.ScrollViewer.HorizontalSnapPointsTypeProperty.GetDefaultValue(parentType);
                IsDeferredScrollingEnabled = AC.ScrollViewer.IsDeferredScrollingEnabledProperty.GetDefaultValue(parentType);
                IsScrollChainingEnabled = AC.ScrollViewer.IsScrollChainingEnabledProperty.GetDefaultValue(parentType);
                IsScrollInertiaEnabled = AC.ScrollViewer.IsScrollInertiaEnabledProperty.GetDefaultValue(parentType);
                VerticalScrollBarVisibility = AC.ScrollViewer.VerticalScrollBarVisibilityProperty.GetDefaultValue(parentType);
                VerticalSnapPointsAlignment = AC.ScrollViewer.VerticalSnapPointsAlignmentProperty.GetDefaultValue(parentType);
                VerticalSnapPointsType = AC.ScrollViewer.VerticalSnapPointsTypeProperty.GetDefaultValue(parentType);

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
