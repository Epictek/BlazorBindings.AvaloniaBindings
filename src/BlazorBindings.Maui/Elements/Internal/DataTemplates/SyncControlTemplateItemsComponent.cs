// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Internal;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.DataTemplates
{
    /// <summary>
    /// Unlike <see cref="ControlTemplateItemsComponent{T}"/>, this DataTemplate component does not use a wrapping element. 
    /// This makes it possible to use when returning a View from template is not an option.
    /// However, it requires a DataTemplate to render synchronously, which does not always work with Blazor.
    /// </summary>
    internal class SyncControlTemplateItemsComponent<T> : NativeControlComponentBase, IMauiContainerElementHandler, INonChildContainerElement
        where T : MC.BindableObject
    {
        protected override RenderFragment GetChildContent()
        {
            return builder =>
            {
                for (int i = 0; i < _count; i++)
                {
                    builder.OpenComponent<RootContainerComponent>(1);
                    builder.AddAttribute(2, nameof(RootContainerComponent.ChildContent), Template);
                    builder.AddComponentReferenceCapture(3, c => _lastRootContainer = (RootContainerComponent)c);

                    builder.CloseComponent();
                }
            };
        }

        [Parameter] public Action<T, MC.ControlTemplate> SetControlTemplateAction { get; set; }
        [Parameter] public Action<T, MC.DataTemplate> SetDataTemplateAction { get; set; }
        [Parameter] public RenderFragment Template { get; set; }

        private RootContainerComponent _lastRootContainer;
        private int _count;

        private Microsoft.Maui.IView AddTemplateRoot()
        {
            _count++;
            StateHasChanged();

            var rootElement = _lastRootContainer?.Elements?.FirstOrDefault()
                ?? throw new InvalidOperationException("Template root control is supposed to be rendered at this point.");
            _lastRootContainer = null;

            return (Microsoft.Maui.IView)rootElement;
        }

        void INonPhysicalChild.SetParent(object parentElement)
        {
            var parent = (T)parentElement;

            if (SetControlTemplateAction != null)
            {
                var controlTemplate = new MC.ControlTemplate(AddTemplateRoot);
                SetControlTemplateAction(parent, controlTemplate);
            }

            if (SetDataTemplateAction != null)
            {
                var dataTemplate = new MC.DataTemplate(AddTemplateRoot);
                SetDataTemplateAction(parent, dataTemplate);
            }
        }

        void INonPhysicalChild.RemoveFromParent(object parentElement) { }

        void IElementHandler.ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName) { }

        void IMauiContainerElementHandler.AddChild(MC.BindableObject child, int physicalSiblingIndex) { }
        void IMauiContainerElementHandler.RemoveChild(MC.BindableObject child) { }
        int IMauiContainerElementHandler.GetChildIndex(MC.BindableObject child) => -1;
        MC.BindableObject IMauiElementHandler.ElementControl => null;
        object IElementHandler.TargetElement => null;
    }
}
