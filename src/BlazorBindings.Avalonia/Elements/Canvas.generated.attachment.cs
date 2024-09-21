

namespace BlazorBindings.Avalonia.Elements
{
    
    internal static class CanvasInitializer
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Canvas.Bottom",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Canvas.BottomProperty);
                    }
                    else
                    {
                        AC.Canvas.SetBottom((AvaloniaBindableObject)element, (double)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Canvas.Left",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Canvas.LeftProperty);
                    }
                    else
                    {
                        AC.Canvas.SetLeft((AvaloniaBindableObject)element, (double)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Canvas.Right",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Canvas.RightProperty);
                    }
                    else
                    {
                        AC.Canvas.SetRight((AvaloniaBindableObject)element, (double)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Canvas.Top",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Canvas.TopProperty);
                    }
                    else
                    {
                        AC.Canvas.SetTop((AvaloniaBindableObject)element, (double)value);
                    }
                });
        }
    }

    public static class CanvasExtensions
    {
        /// <summary>
        /// Defines the Bottom attached property.
        /// </summary>
        public static AvaloniaObject CanvasBottom(this AvaloniaObject element, double value)
        {
            element.AttachedProperties["Canvas.Bottom"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the Left attached property.
        /// </summary>
        public static AvaloniaObject CanvasLeft(this AvaloniaObject element, double value)
        {
            element.AttachedProperties["Canvas.Left"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the Right attached property.
        /// </summary>
        public static AvaloniaObject CanvasRight(this AvaloniaObject element, double value)
        {
            element.AttachedProperties["Canvas.Right"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the Top attached property.
        /// </summary>
        public static AvaloniaObject CanvasTop(this AvaloniaObject element, double value)
        {
            element.AttachedProperties["Canvas.Top"] = value;
        
            return element;
        }
    }

    public class Canvas_Attachment : NativeControlComponentBase, INonPhysicalChild, IContainerElementHandler
    {
        /// <summary>
        /// Defines the Bottom attached property.
        /// </summary>
        [Parameter] public double Bottom { get; set; }

        /// <summary>
        /// Defines the Left attached property.
        /// </summary>
        [Parameter] public double Left { get; set; }

        /// <summary>
        /// Defines the Right attached property.
        /// </summary>
        [Parameter] public double Right { get; set; }

        /// <summary>
        /// Defines the Top attached property.
        /// </summary>
        [Parameter] public double Top { get; set; }

        private AvaloniaBindableObject _parent;

        public object TargetElement => _parent;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                var value = parameterValue.Value;
                switch (parameterValue.Name)
                {
                    case nameof(Bottom):
                    if (!Equals(Bottom, value))
                    {
                        Bottom = (double)value;
                        //NativeControl.BottomProperty = Bottom;
                    }
                    break;

                    case nameof(Left):
                    if (!Equals(Left, value))
                    {
                        Left = (double)value;
                        //NativeControl.LeftProperty = Left;
                    }
                    break;

                    case nameof(Right):
                    if (!Equals(Right, value))
                    {
                        Right = (double)value;
                        //NativeControl.RightProperty = Right;
                    }
                    break;

                    case nameof(Top):
                    if (!Equals(Top, value))
                    {
                        Top = (double)value;
                        //NativeControl.TopProperty = Top;
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
                if (Bottom == AC.Canvas.BottomProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.Canvas.BottomProperty);
                }
                else
                {
                    AC.Canvas.SetBottom((AvaloniaBindableObject)parentElement, Bottom);
                }
                
                if (Left == AC.Canvas.LeftProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.Canvas.LeftProperty);
                }
                else
                {
                    AC.Canvas.SetLeft((AvaloniaBindableObject)parentElement, Left);
                }
                
                if (Right == AC.Canvas.RightProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.Canvas.RightProperty);
                }
                else
                {
                    AC.Canvas.SetRight((AvaloniaBindableObject)parentElement, Right);
                }
                
                if (Top == AC.Canvas.TopProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaBindableObject)parentElement).ClearValue(AC.Canvas.TopProperty);
                }
                else
                {
                    AC.Canvas.SetTop((AvaloniaBindableObject)parentElement, Top);
                }
                
            }
        }
    
        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Bottom = Bottom != default ? Bottom : AC.Canvas.BottomProperty.GetDefaultValue(parentType);
                Left = Left != default ? Left : AC.Canvas.LeftProperty.GetDefaultValue(parentType);
                Right = Right != default ? Right : AC.Canvas.RightProperty.GetDefaultValue(parentType);
                Top = Top != default ? Top : AC.Canvas.TopProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = (AvaloniaBindableObject)parentElement;
        }
        
        
        public void RemoveFromParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Bottom = AC.Canvas.BottomProperty.GetDefaultValue(parentType);
                Left = AC.Canvas.LeftProperty.GetDefaultValue(parentType);
                Right = AC.Canvas.RightProperty.GetDefaultValue(parentType);
                Top = AC.Canvas.TopProperty.GetDefaultValue(parentType);

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
