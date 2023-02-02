// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui
{
    public class MauiBlazorBindingsRenderer : NativeComponentRenderer
    {
        public MauiBlazorBindingsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        public override Dispatcher Dispatcher { get; } = new MauiDeviceDispatcher();

        public Task AddComponent(Type componentType, MC.Application parent, Dictionary<string, object> parameters = null)
        {
            var handler = new ApplicationHandler(parent);
            var addComponentTask = AddComponent(componentType, handler, parameters);

            if (!addComponentTask.IsCompleted && parent is MC.Application app)
            {
                // MAUI requires the Application to have the MainPage. If rendering task is not completed synchronously,
                // we need to set MainPage to something.
                app.MainPage ??= new MC.ContentPage();
            }

            return addComponentTask;
        }

        public Task<TComponent> AddComponent<TComponent>(MC.Element parent, Dictionary<string, object> parameters = null) where TComponent : IComponent
        {
            var componentTask = AddComponentLocal();

            if (componentTask.Exception != null)
            {
                // If exception was thrown during the sync execution - throw it straight away.
                ExceptionDispatchInfo.Throw(componentTask.Exception.InnerException);
            }

            return componentTask;

            async Task<TComponent> AddComponentLocal()
            {
                var elementsComponentTask = GetElementsFromRenderedComponent(typeof(TComponent), parameters);

                if (!elementsComponentTask.IsCompleted && parent is MC.Application app)
                {
                    // MAUI requires the Application to have the MainPage. If rendering task is not completed synchronously,
                    // we need to set MainPage to something.
                    app.MainPage ??= new MC.ContentPage();
                }

                var (elements, componentTask) = await elementsComponentTask;
                await SetChildContent(parent, elements);
                return (TComponent)await componentTask;
            }
        }

        protected override void HandleException(Exception exception)
        {
            ExceptionDispatchInfo.Throw(exception);
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new MauiBlazorBindingsElementManager();
        }

        // It tries to return the Element as soon as it is available, therefore Component task might still be in progress.
        internal async Task<(MC.BindableObject Element, Task<IComponent> Component)> GetElementFromRenderedComponent(Type componentType, Dictionary<string, object> parameters = null)
        {
            var (elements, addComponentTask) = await GetElementsFromRenderedComponent(componentType, parameters);

            if (elements.Count != 1)
            {
                throw new InvalidOperationException("The target component must have exactly one root element.");
            }

            return (elements[0], addComponentTask);
        }

        private async Task<(List<MC.BindableObject> Elements, Task<IComponent> Component)> GetElementsFromRenderedComponent(Type componentType, Dictionary<string, object> parameters)
        {
            var container = new RootContainerHandler();

            var addComponentTask = AddComponent(componentType, container, parameters);
            var elementAddedTask = container.WaitForElementAsync();

            await Task.WhenAny(addComponentTask, elementAddedTask);

            if (addComponentTask.Exception != null)
            {
                var exception = addComponentTask.Exception.InnerException;
                ExceptionDispatchInfo.Throw(exception);
            }

            return (container.Elements, addComponentTask);
        }

        private static Task SetChildContent(MC.BindableObject parent, List<MC.BindableObject> children)
        {
            switch (parent)
            {
                case MC.NavigationPage page:
                    return page.PushAsync(CastSingle<MC.Page>(children));

                case MC.Application application:
                    application.MainPage = CastSingle<MC.Page>(children);
                    break;
                case MC.ContentPage contentPage:
                    contentPage.Content = CastSingle<MC.View>(children);
                    break;
                case MC.ContentView contentView:
                    contentView.Content = CastSingle<MC.View>(children);
                    break;
                case MC.FlyoutPage flyoutPage:
                    flyoutPage.Detail = CastSingle<MC.Page>(children);
                    break;
                case MC.ScrollView scrollView:
                    scrollView.Content = CastSingle<MC.View>(children);
                    break;
                case MC.ShellContent shellContent:
                    shellContent.Content = CastSingle<MC.Page>(children);
                    break;
                case MC.StackBase stackBase:
                    foreach (var child in children)
                        stackBase.Children.Add(Cast<MC.View>(child));
                    break;
                case MC.TabbedPage tabbedPage:
                    foreach (var child in children)
                        tabbedPage.Children.Add(Cast<MC.Page>(child));
                    break;
                case MC.Shell shell:
                    foreach (var child in children)
                        shell.Items.Add(Cast<MC.TemplatedPage>(child));
                    break;
                case MC.ShellItem shellItem:
                    foreach (var child in children)
                        shellItem.Items.Add(Cast<MC.TemplatedPage>(child));
                    break;
                case MC.ShellSection shellSection:
                    foreach (var child in children)
                        shellSection.Items.Add(Cast<MC.TemplatedPage>(child));
                    break;

                default:
                    throw new InvalidOperationException($"Renderer doesn't support {parent?.GetType()?.Name} as a parent element.");
            };

            return Task.CompletedTask;

            static T CastSingle<T>(List<MC.BindableObject> children) where T : MC.BindableObject
            {
                if (children.Count != 1)
                    throw new InvalidOperationException("The target component must have exactly one root element.");

                return Cast<T>(children[0]);
            }

            static T Cast<T>(MC.BindableObject e) where T : class
                => e as T ?? throw new InvalidOperationException($"{typeof(T).Name} element expected, but {e?.GetType()?.Name} found.");
        }
    }
}
