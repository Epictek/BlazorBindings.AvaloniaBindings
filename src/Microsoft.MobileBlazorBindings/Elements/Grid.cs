// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Grid : Layout
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.Column",
                (element, value) => XF.Grid.SetColumn(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.ColumnSpan",
                (element, value) => XF.Grid.SetColumnSpan(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.Row",
                (element, value) => XF.Grid.SetRow(element, AttributeHelper.GetInt(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Grid.RowSpan",
                (element, value) => XF.Grid.SetRowSpan(element, AttributeHelper.GetInt(value)));
        }

        /// <summary>
        /// A comma-separated list of column definitions. A column definition can be:
        /// Auto-sized with the <c>Auto</c> keyword; A numeric size, such as <c>80.5</c>; Or a relative size, such as <c>*</c>, <c>2*</c>, or <c>3.5*</c>.
        /// </summary>
        [Parameter] public string ColumnDefinitions { get; set; }
        /// <summary>
        /// A comma-separated list of row definitions. A row definition can be:
        /// Auto-sized with the <c>Auto</c> keyword; A numeric size, such as <c>80.5</c>; Or a relative size, such as <c>*</c>, <c>2*</c>, or <c>3.5*</c>.
        /// </summary>
        [Parameter] public string RowDefinitions { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (ColumnDefinitions != null)
            {
                builder.AddAttribute(nameof(ColumnDefinitions), ColumnDefinitions);
            }
            if (RowDefinitions != null)
            {
                builder.AddAttribute(nameof(RowDefinitions), RowDefinitions);
            }
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
