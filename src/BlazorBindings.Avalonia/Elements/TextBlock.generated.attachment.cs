

namespace BlazorBindings.Avalonia.Elements
{
    
    internal static class TextBlockInitializer
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextBlock.BaselineOffset",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.TextBlock.BaselineOffsetProperty);
                    }
                    else
                    {
                        AC.TextBlock.SetBaselineOffset((AvaloniaPage)element, (double)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextBlock.LetterSpacing",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.TextBlock.LetterSpacingProperty);
                    }
                    else
                    {
                        AC.TextBlock.SetLetterSpacing((AvaloniaPage)element, (double)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextBlock.LineHeight",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.TextBlock.LineHeightProperty);
                    }
                    else
                    {
                        AC.TextBlock.SetLineHeight((AvaloniaPage)element, (double)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextBlock.MaxLines",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.TextBlock.MaxLinesProperty);
                    }
                    else
                    {
                        AC.TextBlock.SetMaxLines((AvaloniaPage)element, (int)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextBlock.TextAlignment",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.TextBlock.TextAlignmentProperty);
                    }
                    else
                    {
                        AC.TextBlock.SetTextAlignment((AvaloniaPage)element, (global::Avalonia.Media.TextAlignment)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextBlock.TextTrimming",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.TextBlock.TextTrimmingProperty);
                    }
                    else
                    {
                        AC.TextBlock.SetTextTrimming((AvaloniaPage)element, (global::Avalonia.Media.TextTrimming)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("TextBlock.TextWrapping",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.TextBlock.TextWrappingProperty);
                    }
                    else
                    {
                        AC.TextBlock.SetTextWrapping((AvaloniaPage)element, (global::Avalonia.Media.TextWrapping)value);
                    }
                });
        }
    }

    public static class TextBlockExtensions
    {
        /// <summary>
        /// DependencyProperty for <see cref="P:Avalonia.Controls.TextBlock.BaselineOffset" /> property.
        /// </summary>
        public static Control TextBlockBaselineOffset(this Control element, double value)
        {
            element.AttachedProperties["TextBlock.BaselineOffset"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.LetterSpacing" /> property.
        /// </summary>
        public static Control TextBlockLetterSpacing(this Control element, double value)
        {
            element.AttachedProperties["TextBlock.LetterSpacing"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.LineHeight" /> property.
        /// </summary>
        public static Control TextBlockLineHeight(this Control element, double value)
        {
            element.AttachedProperties["TextBlock.LineHeight"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.MaxLines" /> property.
        /// </summary>
        public static Control TextBlockMaxLines(this Control element, int value)
        {
            element.AttachedProperties["TextBlock.MaxLines"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.TextAlignment" /> property.
        /// </summary>
        public static Control TextBlockTextAlignment(this Control element, global::Avalonia.Media.TextAlignment value)
        {
            element.AttachedProperties["TextBlock.TextAlignment"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.TextTrimming" /> property.
        /// </summary>
        public static Control TextBlockTextTrimming(this Control element, global::Avalonia.Media.TextTrimming value)
        {
            element.AttachedProperties["TextBlock.TextTrimming"] = value;
        
            return element;
        }
        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.TextWrapping" /> property.
        /// </summary>
        public static Control TextBlockTextWrapping(this Control element, global::Avalonia.Media.TextWrapping value)
        {
            element.AttachedProperties["TextBlock.TextWrapping"] = value;
        
            return element;
        }
    }

    public class TextBlock_Attachment : NativeControlComponentBase, INonPhysicalChild, IContainerElementHandler
    {
        /// <summary>
        /// DependencyProperty for <see cref="P:Avalonia.Controls.TextBlock.BaselineOffset" /> property.
        /// </summary>
        [Parameter] public double BaselineOffset { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.LetterSpacing" /> property.
        /// </summary>
        [Parameter] public double LetterSpacing { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.LineHeight" /> property.
        /// </summary>
        [Parameter] public double LineHeight { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.MaxLines" /> property.
        /// </summary>
        [Parameter] public int MaxLines { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.TextAlignment" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.TextAlignment TextAlignment { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.TextTrimming" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.TextTrimming TextTrimming { get; set; }

        /// <summary>
        /// Defines the <see cref="P:Avalonia.Controls.TextBlock.TextWrapping" /> property.
        /// </summary>
        [Parameter] public global::Avalonia.Media.TextWrapping TextWrapping { get; set; }

        private AvaloniaPage _parent;

        public object TargetElement => _parent;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                var value = parameterValue.Value;
                switch (parameterValue.Name)
                {
                    case nameof(BaselineOffset):
                    if (!Equals(BaselineOffset, value))
                    {
                        BaselineOffset = (double)value;
                        //NativeControl.BaselineOffsetProperty = BaselineOffset;
                    }
                    break;

                    case nameof(LetterSpacing):
                    if (!Equals(LetterSpacing, value))
                    {
                        LetterSpacing = (double)value;
                        //NativeControl.LetterSpacingProperty = LetterSpacing;
                    }
                    break;

                    case nameof(LineHeight):
                    if (!Equals(LineHeight, value))
                    {
                        LineHeight = (double)value;
                        //NativeControl.LineHeightProperty = LineHeight;
                    }
                    break;

                    case nameof(MaxLines):
                    if (!Equals(MaxLines, value))
                    {
                        MaxLines = (int)value;
                        //NativeControl.MaxLinesProperty = MaxLines;
                    }
                    break;

                    case nameof(TextAlignment):
                    if (!Equals(TextAlignment, value))
                    {
                        TextAlignment = (global::Avalonia.Media.TextAlignment)value;
                        //NativeControl.TextAlignmentProperty = TextAlignment;
                    }
                    break;

                    case nameof(TextTrimming):
                    if (!Equals(TextTrimming, value))
                    {
                        TextTrimming = (global::Avalonia.Media.TextTrimming)value;
                        //NativeControl.TextTrimmingProperty = TextTrimming;
                    }
                    break;

                    case nameof(TextWrapping):
                    if (!Equals(TextWrapping, value))
                    {
                        TextWrapping = (global::Avalonia.Media.TextWrapping)value;
                        //NativeControl.TextWrappingProperty = TextWrapping;
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
                if (BaselineOffset == AC.TextBlock.BaselineOffsetProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.TextBlock.BaselineOffsetProperty);
                }
                else
                {
                    AC.TextBlock.SetBaselineOffset((AvaloniaPage)parentElement, BaselineOffset);
                }
                
                if (LetterSpacing == AC.TextBlock.LetterSpacingProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.TextBlock.LetterSpacingProperty);
                }
                else
                {
                    AC.TextBlock.SetLetterSpacing((AvaloniaPage)parentElement, LetterSpacing);
                }
                
                if (LineHeight == AC.TextBlock.LineHeightProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.TextBlock.LineHeightProperty);
                }
                else
                {
                    AC.TextBlock.SetLineHeight((AvaloniaPage)parentElement, LineHeight);
                }
                
                if (MaxLines == AC.TextBlock.MaxLinesProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.TextBlock.MaxLinesProperty);
                }
                else
                {
                    AC.TextBlock.SetMaxLines((AvaloniaPage)parentElement, MaxLines);
                }
                
                if (TextAlignment == AC.TextBlock.TextAlignmentProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.TextBlock.TextAlignmentProperty);
                }
                else
                {
                    AC.TextBlock.SetTextAlignment((AvaloniaPage)parentElement, TextAlignment);
                }
                
                if (TextTrimming == AC.TextBlock.TextTrimmingProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.TextBlock.TextTrimmingProperty);
                }
                else
                {
                    AC.TextBlock.SetTextTrimming((AvaloniaPage)parentElement, TextTrimming);
                }
                
                if (TextWrapping == AC.TextBlock.TextWrappingProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.TextBlock.TextWrappingProperty);
                }
                else
                {
                    AC.TextBlock.SetTextWrapping((AvaloniaPage)parentElement, TextWrapping);
                }
                
            }
        }
    
        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                BaselineOffset = BaselineOffset != default ? BaselineOffset : AC.TextBlock.BaselineOffsetProperty.GetDefaultValue(parentType);
                LetterSpacing = LetterSpacing != default ? LetterSpacing : AC.TextBlock.LetterSpacingProperty.GetDefaultValue(parentType);
                LineHeight = LineHeight != default ? LineHeight : AC.TextBlock.LineHeightProperty.GetDefaultValue(parentType);
                MaxLines = MaxLines != default ? MaxLines : AC.TextBlock.MaxLinesProperty.GetDefaultValue(parentType);
                TextAlignment = TextAlignment != default ? TextAlignment : AC.TextBlock.TextAlignmentProperty.GetDefaultValue(parentType);
                TextTrimming = TextTrimming != default ? TextTrimming : AC.TextBlock.TextTrimmingProperty.GetDefaultValue(parentType);
                TextWrapping = TextWrapping != default ? TextWrapping : AC.TextBlock.TextWrappingProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = (AvaloniaPage)parentElement;
        }
        
        
        public void RemoveFromParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                BaselineOffset = AC.TextBlock.BaselineOffsetProperty.GetDefaultValue(parentType);
                LetterSpacing = AC.TextBlock.LetterSpacingProperty.GetDefaultValue(parentType);
                LineHeight = AC.TextBlock.LineHeightProperty.GetDefaultValue(parentType);
                MaxLines = AC.TextBlock.MaxLinesProperty.GetDefaultValue(parentType);
                TextAlignment = AC.TextBlock.TextAlignmentProperty.GetDefaultValue(parentType);
                TextTrimming = AC.TextBlock.TextTrimmingProperty.GetDefaultValue(parentType);
                TextWrapping = AC.TextBlock.TextWrappingProperty.GetDefaultValue(parentType);

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
