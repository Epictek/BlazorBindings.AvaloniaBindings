using BlazorBindings.Maui.ShellNavigation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui
{
    public partial class Navigation
    {
        private readonly Dictionary<string, MBBRouteFactory> RouteFactories = new();
        private readonly Dictionary<Type, StructuredRouteResult> NavigationParameters = new();
        private List<StructuredRoute> Routes;

        /// <summary>
        /// Performs URI-based Shell navigation. This method is only available for Shell-based applications.
        /// </summary>
        /// <param name="uri">URI to navigate to.</param>
        /// <param name="parameters">Additional parameters to set for component.</param>
        public async Task NavigateToAsync(string uri, Dictionary<string, object> parameters = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var shell = MC.Shell.Current
                ?? throw new InvalidOperationException("URI-based navigation requires Shell-based application.");

            Routes ??= FindRoutes();

            var route = StructuredRoute.FindBestMatch(uri, Routes, parameters);

            if (route != null)
            {
                NavigationParameters[route.Route.Type] = route;
                if (!RouteFactories.TryGetValue(route.Route.BaseUri, out var routeFactory))
                {
                    throw new InvalidOperationException($"A route factory for URI '{uri}' could not be found.");
                }
                await routeFactory.CreateAsync();
                await shell.GoToAsync(route.Route.BaseUri);
            }
            else
            {
                throw new InvalidOperationException($"The route '{uri}' is not registered. Register page routes using the '@page' directive in the page.");
            }
        }

        //TODO This route matching could be better. Can we use the ASPNEt version?
        private List<StructuredRoute> FindRoutes()
        {
            var appType = MC.Application.Current.GetType();
            var assembly = appType.IsGenericType && appType.GetGenericTypeDefinition() == typeof(BlazorBindingsApplication<>)
                ? appType.GenericTypeArguments[0].Assembly
                : appType.Assembly;

            var result = new List<StructuredRoute>();
            var pages = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ComponentBase)));
            foreach (var page in pages)
            {
                //Find each @page on a page. There can be multiple.
                var routes = page.GetCustomAttributes<RouteAttribute>();
                foreach (var route in routes)
                {
                    if (route.Template == "/")
                    {
                        // This route can be used in Hybrid apps and should be ignored by Shell (because Shell doesn't support empty routes anyway)
                        continue;
                    }

                    var structuredRoute = new StructuredRoute(route.Template, page);

                    //Register with XamarinForms so it can handle Navigation.
                    var routeFactory = new MBBRouteFactory(page, BuildPage);
                    MC.Routing.RegisterRoute(structuredRoute.BaseUri, routeFactory);

                    //Also register route in our own list for setting parameters and tracking if it is registered;
                    result.Add(structuredRoute);
                    RouteFactories[structuredRoute.BaseUri] = routeFactory;
                }
            }

            return result;
        }

        private static Dictionary<string, object> ConvertParameters(Type componentType, Dictionary<string, string> parameters)
        {
            if (parameters is null)
            {
                return null;
            }

            var convertedParameters = new Dictionary<string, object>();

            foreach (var keyValue in parameters)
            {
                var propertyType = componentType.GetProperty(keyValue.Key)?.PropertyType ?? typeof(string);
                if (!StringConverter.TryParse(propertyType, keyValue.Value, CultureInfo.InvariantCulture, out var parsedValue))
                {
                    throw new InvalidOperationException($"The value {keyValue.Value} can not be converted to a {propertyType.Name}");
                }

                convertedParameters[keyValue.Key] = parsedValue;
            }

            return convertedParameters;
        }

        private Task<MC.Page> BuildPage(Type componentType)
        {
            var route = NavigationParameters[componentType];

            var parameters = ConvertParameters(componentType, route.PathParameters);

            if (route.AdditionalParameters is not null)
            {
                if (parameters is null)
                {
                    parameters = route.AdditionalParameters;
                }
                else
                {
                    foreach (var (key, value) in route.AdditionalParameters)
                    {
                        parameters.Add(key, value);
                    }
                }
            }

            return BuildElement<MC.Page>(componentType, parameters);
        }
    }
}
