

namespace BlazorBindings.Avalonia.Elements
{
    
    internal static class GridInitializer
    {
        [System.Runtime.CompilerServices.ModuleInitializer]
        internal static void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.Column",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Grid.ColumnProperty);
                    }
                    else
                    {
                        if (value is string s)
                        {
                            AC.Grid.SetColumn((AvaloniaPage)element, int.Parse(s));

                        }
                        else
                        {
                            AC.Grid.SetColumn((AvaloniaPage)element, (int)value);
                        }
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.ColumnSpan",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Grid.ColumnSpanProperty);
                    }
                    else
                    {
                        AC.Grid.SetColumnSpan((AvaloniaPage)element, (int)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.IsSharedSizeScope",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Grid.IsSharedSizeScopeProperty);
                    }
                    else
                    {
                        AC.Grid.SetIsSharedSizeScope((AvaloniaPage)element, (bool)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.Row",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Grid.RowProperty);
                    }
                    else
                    {
                        AC.Grid.SetRow((AvaloniaPage)element, (int)value);
                    }
                });
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.RowSpan",
                (element, value) => 
                {
                    if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
                    {
                        element.ClearValue(AC.Grid.RowSpanProperty);
                    }
                    else
                    {
                        AC.Grid.SetRowSpan((AvaloniaPage)element, (int)value);
                    }
                });
        }
    }

    public static class GridExtensions
    {
        /// <summary>
        /// Column property. This is an attached property. Grid defines Column property, so that it can be set on any element treated as a cell. Column property specifies child's position with respect to columns.
        /// </summary>
        public static Control GridColumn(this Control element, int value)
        {
            element.AttachedProperties["Grid.Column"] = value;
        
            return element;
        }
        /// <summary>
        /// ColumnSpan property. This is an attached property. Grid defines ColumnSpan, so that it can be set on any element treated as a cell. ColumnSpan property specifies child's width with respect to columns. Example, ColumnSpan == 2 means that child will span across two columns.
        /// </summary>
        public static Control GridColumnSpan(this Control element, int value)
        {
            element.AttachedProperties["Grid.ColumnSpan"] = value;
        
            return element;
        }
        /// <summary>
        /// IsSharedSizeScope property marks scoping element for shared size.
        /// </summary>
        public static Control GridIsSharedSizeScope(this Control element, bool value)
        {
            element.AttachedProperties["Grid.IsSharedSizeScope"] = value;
        
            return element;
        }
        /// <summary>
        /// Row property. This is an attached property. Grid defines Row, so that it can be set on any element treated as a cell. Row property specifies child's position with respect to rows. <remarks><para> Rows are 0 - based. In order to appear in first row, element should have Row property set to <c>0</c>. </para><para> Default value for the property is <c>0</c>. </para></remarks>
        /// </summary>
        public static Control GridRow(this Control element, int value)
        {
            element.AttachedProperties["Grid.Row"] = value;
        
            return element;
        }
        /// <summary>
        /// RowSpan property. This is an attached property. Grid defines RowSpan, so that it can be set on any element treated as a cell. RowSpan property specifies child's height with respect to row grid lines. Example, RowSpan == 3 means that child will span across three rows.
        /// </summary>
        public static Control GridRowSpan(this Control element, int value)
        {
            element.AttachedProperties["Grid.RowSpan"] = value;
        
            return element;
        }
    }

    public class Grid_Attachment : NativeControlComponentBase, INonPhysicalChild, IContainerElementHandler
    {
        /// <summary>
        /// Column property. This is an attached property. Grid defines Column property, so that it can be set on any element treated as a cell. Column property specifies child's position with respect to columns.
        /// </summary>
        [Parameter] public int Column { get; set; }

        /// <summary>
        /// ColumnSpan property. This is an attached property. Grid defines ColumnSpan, so that it can be set on any element treated as a cell. ColumnSpan property specifies child's width with respect to columns. Example, ColumnSpan == 2 means that child will span across two columns.
        /// </summary>
        [Parameter] public int ColumnSpan { get; set; }

        /// <summary>
        /// IsSharedSizeScope property marks scoping element for shared size.
        /// </summary>
        [Parameter] public bool IsSharedSizeScope { get; set; }

        /// <summary>
        /// Row property. This is an attached property. Grid defines Row, so that it can be set on any element treated as a cell. Row property specifies child's position with respect to rows. <remarks><para> Rows are 0 - based. In order to appear in first row, element should have Row property set to <c>0</c>. </para><para> Default value for the property is <c>0</c>. </para></remarks>
        /// </summary>
        [Parameter] public int Row { get; set; }

        /// <summary>
        /// RowSpan property. This is an attached property. Grid defines RowSpan, so that it can be set on any element treated as a cell. RowSpan property specifies child's height with respect to row grid lines. Example, RowSpan == 3 means that child will span across three rows.
        /// </summary>
        [Parameter] public int RowSpan { get; set; }

        private AvaloniaPage _parent;

        public object TargetElement => _parent;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                var value = parameterValue.Value;
                switch (parameterValue.Name)
                {
                    case nameof(Column):
                    if (!Equals(Column, value))
                    {
                        Column = (int)value;
                        //NativeControl.ColumnProperty = Column;
                    }
                    break;

                    case nameof(ColumnSpan):
                    if (!Equals(ColumnSpan, value))
                    {
                        ColumnSpan = (int)value;
                        //NativeControl.ColumnSpanProperty = ColumnSpan;
                    }
                    break;

                    case nameof(IsSharedSizeScope):
                    if (!Equals(IsSharedSizeScope, value))
                    {
                        IsSharedSizeScope = (bool)value;
                        //NativeControl.IsSharedSizeScopeProperty = IsSharedSizeScope;
                    }
                    break;

                    case nameof(Row):
                    if (!Equals(Row, value))
                    {
                        Row = (int)value;
                        //NativeControl.RowProperty = Row;
                    }
                    break;

                    case nameof(RowSpan):
                    if (!Equals(RowSpan, value))
                    {
                        RowSpan = (int)value;
                        //NativeControl.RowSpanProperty = RowSpan;
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
                if (Column == AC.Grid.ColumnProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.Grid.ColumnProperty);
                }
                else
                {
                    AC.Grid.SetColumn((AvaloniaPage)parentElement, Column);
                }
                
                if (ColumnSpan == AC.Grid.ColumnSpanProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.Grid.ColumnSpanProperty);
                }
                else
                {
                    AC.Grid.SetColumnSpan((AvaloniaPage)parentElement, ColumnSpan);
                }
                
                if (IsSharedSizeScope == AC.Grid.IsSharedSizeScopeProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.Grid.IsSharedSizeScopeProperty);
                }
                else
                {
                    AC.Grid.SetIsSharedSizeScope((AvaloniaPage)parentElement, IsSharedSizeScope);
                }
                
                if (Row == AC.Grid.RowProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.Grid.RowProperty);
                }
                else
                {
                    AC.Grid.SetRow((AvaloniaPage)parentElement, Row);
                }
                
                if (RowSpan == AC.Grid.RowSpanProperty.GetDefaultValue(parentElement.GetType()))
                {
                    ((AvaloniaPage)parentElement).ClearValue(AC.Grid.RowSpanProperty);
                }
                else
                {
                    AC.Grid.SetRowSpan((AvaloniaPage)parentElement, RowSpan);
                }
                
            }
        }
    
        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Column = Column != default ? Column : AC.Grid.ColumnProperty.GetDefaultValue(parentType);
                ColumnSpan = ColumnSpan != default ? ColumnSpan : AC.Grid.ColumnSpanProperty.GetDefaultValue(parentType);
                IsSharedSizeScope = IsSharedSizeScope != default ? IsSharedSizeScope : AC.Grid.IsSharedSizeScopeProperty.GetDefaultValue(parentType);
                Row = Row != default ? Row : AC.Grid.RowProperty.GetDefaultValue(parentType);
                RowSpan = RowSpan != default ? RowSpan : AC.Grid.RowSpanProperty.GetDefaultValue(parentType);

                TryUpdateParent(parentElement);
            }

            _parent = (AvaloniaPage)parentElement;
        }
        
        
        public void RemoveFromParent(object parentElement)
        {
            var parentType = parentElement?.GetType();
            if (parentType is not null)
            {
                Column = AC.Grid.ColumnProperty.GetDefaultValue(parentType);
                ColumnSpan = AC.Grid.ColumnSpanProperty.GetDefaultValue(parentType);
                IsSharedSizeScope = AC.Grid.IsSharedSizeScopeProperty.GetDefaultValue(parentType);
                Row = AC.Grid.RowProperty.GetDefaultValue(parentType);
                RowSpan = AC.Grid.RowSpanProperty.GetDefaultValue(parentType);

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
