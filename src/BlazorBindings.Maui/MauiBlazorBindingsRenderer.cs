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

        public async Task<TComponent> AddComponent<TComponent>(MC.Element parent, Dictionary<string, object> parameters = null) where TComponent : IComponent
        {
            var elementComponentTask = GetElementFromRenderedComponent(typeof(TComponent), parameters);

            if (!elementComponentTask.IsCompleted && parent is MC.Application app)
            {
                // MAUI requires the Application to have the MainPage. If rendering task is not completed synchroniously,
                // we need to set MainPage to something.
                app.MainPage ??= new MC.ContentPage();
            }

            var (element, componentTask) = await elementComponentTask;
            await SetChildContent(parent, element);
            return (TComponent)await componentTask;
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new MauiBlazorBindingsElementManager();
        }

        // It tries to return the Element as soon as it is available, therefore Component task might still be in progress.
        internal async Task<(MC.Element Element, Task<IComponent> Component)> GetElementFromRenderedComponent(Type componentType, Dictionary<string, object> parameters = null)
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

            if (container.Elements.Count != 1)
            {
                throw new InvalidOperationException("The target component must have exactly one root element.");
            }

            return (container.Elements[0], addComponentTask);
        }

        private static Task SetChildContent(MC.Element parent, MC.Element child)
        {
            switch (parent)
            {
                case MC.NavigationPage page:
                    return page.PushAsync(Cast<MC.Page>(child));

                case MC.Application application:
                    application.MainPage = Cast<MC.Page>(child);
                    break;
                case MC.ContentPage contentPage:
                    contentPage.Content = Cast<MC.View>(child);
                    break;
                case MC.ContentView contentView:
                    contentView.Content = Cast<MC.View>(child);
                    break;
                case MC.FlyoutPage flyoutPage:
                    flyoutPage.Detail = Cast<MC.Page>(child);
                    break;
                case MC.ScrollView scrollView:
                    scrollView.Content = Cast<MC.View>(child);
                    break;
                case MC.StackBase stackBase:
                    stackBase.Children.Add(Cast<MC.View>(child));
                    break;
                case MC.ShellContent shellContent:
                    shellContent.Content = Cast<MC.Page>(child);
                    break;
                case MC.Shell shell:
                    shell.Items.Add(Cast<MC.TemplatedPage>(child));
                    break;
                case MC.ShellItem shellItem:
                    shellItem.Items.Add(Cast<MC.TemplatedPage>(child));
                    break;
                case MC.ShellSection shellSection:
                    shellSection.Items.Add(Cast<MC.TemplatedPage>(child));
                    break;
                case MC.TabbedPage tabbedPage:
                    tabbedPage.Children.Add(Cast<MC.TemplatedPage>(child));
                    break;

                default:
                    throw new InvalidOperationException($"Renderer doesn't support {parent?.GetType()?.Name} as a parent element.");
            };

            return Task.CompletedTask;

            static T Cast<T>(MC.Element e) where T : MC.Element => e as T
                ?? throw new InvalidOperationException($"{typeof(T).Name} element expected, but {e?.GetType()?.Name} found.");
        }
    }
}
