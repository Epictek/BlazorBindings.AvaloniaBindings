// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates
{
    /// <summary>
    /// Unlike <see cref="DataTemplateItemsComponent{TControl, TItem}"/>, this DataTemplate component does not use a wrapping element. 
    /// This makes it possible to use when returning a View from template is not an option.
    /// However, it requires a DataTemplate to render synchronously, which does not always work with Blazor.
    /// </summary>
    internal class SyncDataTemplateItemsComponent<TControl, TItem> : NativeControlComponentBase, IMauiContainerElementHandler, INonChildContainerElement
    {
        protected override RenderFragment GetChildContent() => builder =>
        {
            foreach (var item in _initialItems)
            {
                builder.OpenComponent<SyncDataTemplateItemComponent<TItem>>(1);
                builder.AddAttribute(2, nameof(SyncDataTemplateItemComponent<TItem>.Template), Template);
                builder.AddAttribute(3, nameof(SyncDataTemplateItemComponent<TItem>.InitialItem), item);
                builder.AddComponentReferenceCapture(4, c => _lastAddedItem = (SyncDataTemplateItemComponent<TItem>)c);
                builder.CloseComponent();
            }
        };

        [Parameter] public Action<TControl, MC.DataTemplate> SetDataTemplateAction { get; set; }
        [Parameter] public RenderFragment<TItem> Template { get; set; }

        private readonly List<TItem> _initialItems = new();

        private SyncDataTemplateItemComponent<TItem> _lastAddedItem;

        public MC.BindableObject AddTemplateRoot(TItem initialItem)
        {
            _initialItems.Add(initialItem);
            StateHasChanged();

            var rootElement = _lastAddedItem?.RootControl
                ?? throw new InvalidOperationException("Template root control is supposed to be rendered at this point.");
            _lastAddedItem = null;

            return rootElement;
        }

        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parent = (TControl)parentElement;
            var dataTemplate = new DataTemplateSelector(AddTemplateRoot);
            SetDataTemplateAction(parent, dataTemplate);
        }

        void INonPhysicalChild.RemoveFromParent(object parentElement) { }

        MC.BindableObject IMauiElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;
        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex) { }
        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child) { }
        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child) => -1;
        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }


        class DataTemplateSelector : MC.DataTemplateSelector
        {
            private readonly MC.DataTemplate _dataTemplate;
            private readonly Func<TItem, MC.BindableObject> _loadTemplate;
            private TItem _initialItem;

            public DataTemplateSelector(Func<TItem, MC.BindableObject> loadTemplate)
            {
                _loadTemplate = loadTemplate;
                _dataTemplate = new MC.DataTemplate(() => _loadTemplate(_initialItem));
            }

            protected override MC.DataTemplate OnSelectTemplate(object item, MC.BindableObject container)
            {
                _initialItem = (TItem)item;
                return _dataTemplate;
            }
        }
    }
}
