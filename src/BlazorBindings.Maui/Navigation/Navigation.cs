using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using IComponent = Microsoft.AspNetCore.Components.IComponent;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui
{
    public partial class Navigation : INavigation
    {
        protected readonly IServiceProvider _services;
        private Type _wrapperComponentType;

        public Navigation(IServiceProvider services)
        {
            _services = services;
        }

        internal void SetWrapperComponentType(Type wrapperComponentType)
        {
            _wrapperComponentType = wrapperComponentType;
        }

        protected MC.INavigation MauiNavigation => Application.Current.MainPage.Navigation;

        /// <summary>
        /// Push page component <typeparamref name="T"/> to the Navigation Stack.
        /// </summary>
        public async Task PushAsync<T>(Dictionary<string, object> arguments = null, bool animated = true) where T : IComponent
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(typeof(T), arguments);
                await MauiNavigation.PushAsync(page, animated);
            });
        }

        /// <summary>
        /// Push page component <typeparamref name="T"/> to the Modal Stack.
        /// </summary>
        public async Task PushModalAsync<T>(Dictionary<string, object> arguments = null, bool animated = true) where T : IComponent
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(typeof(T), arguments);
                await MauiNavigation.PushModalAsync(page, animated);
            });
        }

        /// <summary>
        /// Push page component from the <paramref name="renderFragment"/> to the Modal Stack.
        /// </summary>
        /// <remarks>Experimental API, subject to change.</remarks>
        public async Task PushModalAsync(RenderFragment renderFragment, bool animated = true)
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(renderFragment);
                await MauiNavigation.PushModalAsync(page, animated);
            });
        }

        /// <summary>
        /// Push page component from the <paramref name="renderFragment"/> to the Navigation Stack.
        /// </summary>
        /// <remarks>Experimental API, subject to change.</remarks>
        public async Task PushAsync(RenderFragment renderFragment, bool animated = true)
        {
            await NavigationAction(async () =>
            {
                var page = await BuildElement<Page>(renderFragment);
                await MauiNavigation.PushAsync(page, animated);
            });
        }

        public async Task PopModalAsync(bool animated = true)
        {
            await NavigationAction(() => MauiNavigation.PopModalAsync(animated));
        }

        public async Task PopAsync(bool animated = true)
        {
            await NavigationAction(() => MauiNavigation.PopAsync(animated));
        }

        public async Task PopToRootAsync(bool animated = true)
        {
            await NavigationAction(() => MauiNavigation.PopToRootAsync(animated));
        }

        /// <summary>
        /// Returns rendered MAUI element from <paramref name="renderFragment"/>.
        /// This method is exposed for extensibility purposes, and shouldn't be used directly.
        /// </summary>
        /// <remarks>Experimental API, subject to change.</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Task<T> BuildElement<T>(RenderFragment renderFragment) where T : Element
        {
            return BuildElement<T>(typeof(RenderFragmentComponent), new()
            {
                [nameof(RenderFragmentComponent.RenderFragment)] = renderFragment
            });
        }

        /// <summary>
        /// Returns rendered MAUI element from component <typeparamref name="T"/>.
        /// This method is exposed for extensibility purposes, and shouldn't be used directly.
        /// </summary>
        /// <remarks>Experimental API, subject to change.</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<T> BuildElement<T>(Type componentType, Dictionary<string, object> arguments) where T : Element
        {
            if (_wrapperComponentType != null)
            {
                arguments = new()
                {
                    ["ChildContent"] = RenderFragments.FromComponentType(componentType, arguments)
                };
                componentType = _wrapperComponentType;
            }

            var renderer = _services.GetRequiredService<MauiBlazorBindingsRenderer>();

            var (bindableObject, componentTask) = await renderer.GetElementFromRenderedComponent(componentType, arguments);
            var element = (Element)bindableObject;

            element.ParentChanged += DisposeScopeWhenParentRemoved;

            return element as T
                ?? throw new InvalidOperationException($"The target component of a navigation must derive from the {typeof(T).Name} component.");

            async void DisposeScopeWhenParentRemoved(object _, EventArgs __)
            {
                if (element.Parent is null)
                {
                    element.ParentChanged -= DisposeScopeWhenParentRemoved;

                    var component = await componentTask;
                    renderer.RemoveRootComponent(component);
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
