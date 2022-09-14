// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public class GridCell : NativeControlComponentBase, IMauiContainerElementHandler, INonChildContainerElement
    {
        [Parameter] public int? Column { get; set; }
        [Parameter] public int? ColumnSpan { get; set; }
        [Parameter] public int? Row { get; set; }
        [Parameter] public int? RowSpan { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }


        private readonly List<MC.View> _children = new();
        private MC.Grid _parentGrid;

        MC.Element IMauiElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameterValue in parameters)
            {
                switch (parameterValue.Name)
                {
                    case nameof(Column):
                        var columnValue = (int)parameterValue.Value;
                        if (columnValue != Column)
                        {
                            Column = columnValue;
                            _children.ForEach(c => MC.Grid.SetColumn(c, Column ?? 0));
                        }
                        break;
                    case nameof(Row):
                        var rowValue = (int)parameterValue.Value;
                        if (rowValue != Row)
                        {
                            Row = rowValue;
                            _children.ForEach(c => MC.Grid.SetRow(c, Row ?? 0));
                        }
                        break;
                    case nameof(ColumnSpan):
                        var colSpanValue = (int)parameterValue.Value;
                        if (colSpanValue != ColumnSpan)
                        {
                            ColumnSpan = colSpanValue;
                            _children.ForEach(c => MC.Grid.SetColumnSpan(c, ColumnSpan ?? 1));
                        }
                        break;
                    case nameof(RowSpan):
                        var rowSpanValue = (int)parameterValue.Value;
                        if (rowSpanValue != RowSpan)
                        {
                            RowSpan = rowSpanValue;
                            _children.ForEach(c => MC.Grid.SetRowSpan(c, RowSpan ?? 1));
                        }
                        break;
                    case nameof(ChildContent):
                        {
                            ChildContent = (RenderFragment)parameterValue.Value;
                            break;
                        }
                }
            }

            return base.SetParametersAsync(ParameterView.Empty);
        }

        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        public void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            if (child is not MC.View childView)
            {
                throw new ArgumentException($"Expected parent to be of type {typeof(MC.View).FullName} but it is of type {child?.GetType().FullName}.", nameof(child));
            }

            MC.Grid.SetColumn(childView, Column ?? 0);
            MC.Grid.SetColumnSpan(childView, ColumnSpan ?? 1);
            MC.Grid.SetRow(childView, Row ?? 0);
            MC.Grid.SetRowSpan(childView, RowSpan ?? 1);

            _children.Add(childView);
            _parentGrid.Children.Add(childView);
        }

        public void RemoveChild(MC.Element child)
        {
            if (child is not MC.View childView)
            {
                throw new ArgumentException($"Expected parent to be of type {typeof(MC.View).FullName} but it is of type {child?.GetType().FullName}.", nameof(child));
            }

            _children.Remove(childView);
            _parentGrid.Children.Remove(childView);
        }

        public int GetChildIndex(MC.Element child)
        {
            return child is MC.View childView
                ? _children.IndexOf(childView)
                : -1;
        }

        void INonPhysicalChild.SetParent(object parentElement)
        {
            if (parentElement is not MC.Grid parentGrid)
            {
                throw new ArgumentException($"Expected parent to be of type {typeof(MC.Grid).FullName} but it is of type {parentElement?.GetType().FullName}.", nameof(parentElement));
            }

            _parentGrid = parentGrid;
        }

        void INonPhysicalChild.Remove()
        {
            if (_parentGrid != null)
            {
                foreach (var child in _children)
                {
                    _parentGrid.Children.Remove(child);
                }

                _children.Clear();
                _parentGrid = null;
            }
        }

        bool IMauiElementHandler.IsParented()
        {
            // Because this is a 'fake' element, all matters related to physical trees
            // should be no-ops.
            return false;
        }

        void IMauiElementHandler.SetParent(MC.Element parent)
        {
            // This should never get called. Instead, INonChildContainerElement.SetParent() implemented
            // in this class should get called.
            throw new NotSupportedException();
        }
    }
}
