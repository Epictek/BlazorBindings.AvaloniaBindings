using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using IComponent = Microsoft.AspNetCore.Components.IComponent;

namespace BlazorBindings.Maui
{
    public partial class NavigationService : INavigationService
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

        public async Task PopToRootAsync(bool animated = true)
        {
            await NavigationAction(() => Navigation.PopToRootAsync(animated));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Task<T> BuildElement<T>(RenderFragment renderFragment) where T : Element
        {
            return BuildElement<T>(typeof(RenderFragmentComponent), new()
            {
                [nameof(RenderFragmentComponent.RenderFragment)] = renderFragment
            });
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<T> BuildElement<T>(Type componentType, Dictionary<string, object> arguments) where T : Element
        {
            var scope = _services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var renderer = serviceProvider.GetRequiredService<MauiBlazorBindingsRenderer>();

            var (element, _) = await renderer.GetElementFromRenderedComponent(componentType, arguments);

            element.ParentChanged += DisposeScopeWhenParentRemoved;

            return element as T
                ?? throw new InvalidOperationException($"The target component of a navigation must derive from the {typeof(T).Name} component.");

            void DisposeScopeWhenParentRemoved(object _, EventArgs __)
            {
                if (element.Parent is null)
                {
                    //scope.Dispose();
                    element.ParentChanged -= DisposeScopeWhenParentRemoved;
                }
            }
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
                await Task.Yield();
                _navigationInProgress = false;
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
