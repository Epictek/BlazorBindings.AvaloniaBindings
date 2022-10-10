using BlazorBindings.Maui.Elements.Handlers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace BlazorBindings.Maui
{
    public partial class NavigationService
    {
        protected readonly IServiceProvider _services;

        public NavigationService(IServiceProvider services)
        {
            _services = services;
        }

        protected INavigation Navigation => Application.Current.MainPage.Navigation;

        public async Task PushAsync<T>(Dictionary<string, object> arguments = null, bool animated = true) where T : IComponent
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(typeof(T), arguments);
                await Navigation.PushAsync(page, animated);
            });
        }

        public async Task PushModalAsync<T>(Dictionary<string, object> arguments = null, bool animated = true) where T : IComponent
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(typeof(T), arguments);
                await Navigation.PushModalAsync(page, animated);
            });
        }

        public async Task PushModalAsync(RenderFragment renderFragment, bool animated = true)
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(renderFragment);
                await Navigation.PushModalAsync(page, animated);
            });
        }

        public async Task PushAsync(RenderFragment renderFragment, bool animated = true)
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(renderFragment);
                await Navigation.PushAsync(page, animated);
            });
        }

        public async Task PopModalAsync(bool animated = true)
        {
            await NavigationAction(() => Navigation.PopModalAsync(animated));
        }

        public async Task PopAsync(bool animated = true)
        {
            await NavigationAction(() => Navigation.PopAsync(animated));
        }

        static bool _navigationInProgress;
        static async Task NavigationAction(Func<Task> action)
        {
            // Do not allow multiple navigations at the same time.
            if (_navigationInProgress)
                return;

            try
            {
                _navigationInProgress = true;
                await action();
            }
            finally
            {
                // Small delay for animation.
                await Task.Delay(200);
                _navigationInProgress = false;
            }
        }

        protected Task<T> BuildElement<T>(RenderFragment renderFragment) where T : Element
        {
            return BuildElement<T>(typeof(RenderFragmentComponent), new()
            {
                [nameof(RenderFragmentComponent.RenderFragment)] = renderFragment
            });
        }

        protected async Task<T> BuildElement<T>(Type componentType, Dictionary<string, object> arguments) where T : Element
        {
            var scope = _services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var renderer = serviceProvider.GetRequiredService<MauiBlazorBindingsRenderer>();
            var container = new RootContainerHandler();

            var addComponentTask = renderer.AddComponent(componentType, container, arguments);
            var elementAddedTask = container.WaitForElementAsync();

            await Task.WhenAny(addComponentTask, elementAddedTask).ConfigureAwait(false);

            if (addComponentTask.Exception != null)
            {
                var exception = addComponentTask.Exception.InnerException;
                ExceptionDispatchInfo.Throw(exception);
            }

            if (container.Elements.Count != 1)
            {
                throw new InvalidOperationException("The target component of a navigation must have exactly one root element.");
            }

            var element = container.Elements[0] as T
                ?? throw new InvalidOperationException($"The target component of a navigation must derive from the {typeof(T).Name} component.");

            element.ParentChanged += DisposeScopeWhenParentRemoved;

            return element;

            void DisposeScopeWhenParentRemoved(object _, EventArgs __)
            {
                if (element.Parent is null)
                {
                    scope.Dispose();
                    element.ParentChanged -= DisposeScopeWhenParentRemoved;
                }
            }
        }

        private class RenderFragmentComponent : ComponentBase
        {
            [Parameter] public RenderFragment RenderFragment { get; set; }

            protected override void BuildRenderTree(RenderTreeBuilder builder)
            {
                RenderFragment(builder);
            }
        }
    }
}
