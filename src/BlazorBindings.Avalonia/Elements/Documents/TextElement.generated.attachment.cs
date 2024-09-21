using ACD = Avalonia.Controls.Documents;
using BlazorBindings.Avalonia.Elements;

namespace BlazorBindings.Avalonia.Elements
{
    
    internal static class TextElementInitializer
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextElement.FontFamily",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(ACD.TextElement.FontFamilyProperty);
                    }
                    else
                    {
                        ACD.TextElement.SetFontFamily((AvaloniaPage)element, (global::Avalonia.Media.FontFamily)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextElement.FontSize",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(ACD.TextElement.FontSizeProperty);
                    }
                    else
                    {
                        ACD.TextElement.SetFontSize((AvaloniaPage)element, (double)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextElement.FontStretch",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(ACD.TextElement.FontStretchProperty);
                    }
                    else
                    {
                        ACD.TextElement.SetFontStretch((AvaloniaPage)element, (global::Avalonia.Media.FontStretch)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextElement.FontStyle",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(ACD.TextElement.FontStyleProperty);
                    }
                    else
                    {
                        ACD.TextElement.SetFontStyle((AvaloniaPage)element, (global::Avalonia.Media.FontStyle)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextElement.FontWeight",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(ACD.TextElement.FontWeightProperty);
                    }
                    else
                    {
                        ACD.TextElement.SetFontWeight((AvaloniaPage)element, (global::Avalonia.Media.FontWeight)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextElement.Foreground",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(ACD.TextElement.ForegroundProperty);
                    }
                    else
                    {
                        ACD.TextElement.SetForeground((AvaloniaPage)element, (global::Avalonia.Media.IBrush)value);
                    }
                });
        }
    }

    public static class TextElementExtensions
    {
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontFamily" /> property.
        /// </summary>
        public static Control TextElementFontFamily(this Control element, global::Avalonia.Media.FontFamily value)
        {
            element.AttachedProperties["TextElement.FontFamily"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontSize" /> property.
        /// </summary>
        public static Control TextElementFontSize(this Control element, double value)
        {
            element.AttachedProperties["TextElement.FontSize"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontStretch" /> property.
        /// </summary>
        public static Control TextElementFontStretch(this Control element, global::Avalonia.Media.FontStretch value)
        {
            element.AttachedProperties["TextElement.FontStretch"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontStyle" /> property.
        /// </summary>
        public static Control TextElementFontStyle(this Control element, global::Avalonia.Media.FontStyle value)
        {
            element.AttachedProperties["TextElement.FontStyle"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontWeight" /> property.
        /// </summary>
        public static Control TextElementFontWeight(this Control element, global::Avalonia.Media.FontWeight value)
        {
            element.AttachedProperties["TextElement.FontWeight"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.Foreground" /> property.
        /// </summary>
        public static Control TextElementForeground(this Control element, global::Avalonia.Media.IBrush value)
        {
            element.AttachedProperties["TextElement.Foreground"] = value;
        
            return element;
        }
    }

    public class TextElement_Attachment : NativeControlComponentBase, INonPhysicalChild, IContainerElementHandler
    {
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontFamily" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.FontFamily FontFamily { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontSize" /> property.
        /// </summary>
        [Parameter] public double FontSize { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontStretch" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.FontStretch FontStretch { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontStyle" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.FontStyle FontStyle { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.FontWeight" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.FontWeight FontWeight { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.Documents.TextElement.Foreground" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.IBrush Foreground { get; set; }

        private AvaloniaPage _parent;

        public object TargetElement => _parent;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                var value = parameterValue.Value;
                switch (parameterValue.Name)
                {
                    case nameof(FontFamily):
                    if (!Equals(FontFamily, value))
                    {
                        FontFamily = (global::Avalonia.Media.FontFamily)value;
                        //NativeControl.FontFamilyProperty = FontFamily;
                    }
                    break;

                    case nameof(FontSize):
                    if (!Equals(FontSize, value))
                    {
                        FontSize = (double)value;
                        //NativeControl.FontSizeProperty = FontSize;
                    }
                    break;

                    case nameof(FontStretch):
                    if (!Equals(FontStretch, value))
                    {
                        FontStretch = (global::Avalonia.Media.FontStretch)value;
                        //NativeControl.FontStretchProperty = FontStretch;
                    }
                    break;

                    case nameof(FontStyle):
                    if (!Equals(FontStyle, value))
                    {
                        FontStyle = (global::Avalonia.Media.FontStyle)value;
                        //NativeControl.FontStyleProperty = FontStyle;
                    }
                    break;

                    case nameof(FontWeight):
                    if (!Equals(FontWeight, value))
                    {
                        FontWeight = (global::Avalonia.Media.FontWeight)value;
                        //NativeControl.FontWeightProperty = FontWeight;
                    }
                    break;

                    case nameof(Foreground):
                    if (!Equals(Foreground, value))
                    {
                        Foreground = (global::Avalonia.Media.IBrush)value;
                        //NativeControl.ForegroundProperty = Foreground;
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
                if (FontFamily == ACD.TextElement.FontFamilyProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(ACD.TextElement.FontFamilyProperty);
                }
                else
                {
                    ACD.TextElement.SetFontFamily((AvaloniaPage)parentElement, FontFamily);
                }
                
                if (FontSize == ACD.TextElement.FontSizeProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(ACD.TextElement.FontSizeProperty);
                }
                else
                {
                    ACD.TextElement.SetFontSize((AvaloniaPage)parentElement, FontSize);
                }
                
                if (FontStretch == ACD.TextElement.FontStretchProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(ACD.TextElement.FontStretchProperty);
                }
                else
                {
                    ACD.TextElement.SetFontStretch((AvaloniaPage)parentElement, FontStretch);
                }
                
                if (FontStyle == ACD.TextElement.FontStyleProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(ACD.TextElement.FontStyleProperty);
                }
                else
                {
                    ACD.TextElement.SetFontStyle((AvaloniaPage)parentElement, FontStyle);
                }
                
                if (FontWeight == ACD.TextElement.FontWeightProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(ACD.TextElement.FontWeightProperty);
                }
                else
                {
                    ACD.TextElement.SetFontWeight((AvaloniaPage)parentElement, FontWeight);
                }
                
                if (Foreground == ACD.TextElement.ForegroundProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(ACD.TextElement.ForegroundProperty);
                }
                else
                {
                    ACD.TextElement.SetForeground((AvaloniaPage)parentElement, Foreground);
                }
                
            }
        }
    
        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                FontFamily = FontFamily != default ? FontFamily : ACD.TextElement.FontFamilyProperty.GetDefaultValue(parentType);
                FontSize = FontSize != default ? FontSize : ACD.TextElement.FontSizeProperty.GetDefaultValue(parentType);
                FontStretch = FontStretch != default ? FontStretch : ACD.TextElement.FontStretchProperty.GetDefaultValue(parentType);
                FontStyle = FontStyle != default ? FontStyle : ACD.TextElement.FontStyleProperty.GetDefaultValue(parentType);
                FontWeight = FontWeight != default ? FontWeight : ACD.TextElement.FontWeightProperty.GetDefaultValue(parentType);
                Foreground = Foreground != default ? Foreground : ACD.TextElement.ForegroundProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = (AvaloniaPage)parentElement;
        }
        
        
        public void RemoveFromParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                FontFamily = ACD.TextElement.FontFamilyProperty.GetDefaultValue(parentType);
                FontSize = ACD.TextElement.FontSizeProperty.GetDefaultValue(parentType);
                FontStretch = ACD.TextElement.FontStretchProperty.GetDefaultValue(parentType);
                FontStyle = ACD.TextElement.FontStyleProperty.GetDefaultValue(parentType);
                FontWeight = ACD.TextElement.FontWeightProperty.GetDefaultValue(parentType);
                Foreground = ACD.TextElement.ForegroundProperty.GetDefaultValue(parentType);

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
