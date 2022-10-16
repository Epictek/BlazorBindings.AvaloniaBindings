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
    public partial class NavigationService
    {
        private readonly Dictionary<string, MBBRouteFactory> RouteFactories = new();
        private readonly Dictionary<Type, StructuredRouteResult> NavigationParameters = new();
        private List<StructuredRoute> Routes;

        public async Task NavigateToAsync(string uri, Dictionary<string, object> parameters = null)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            Routes ??= FindRoutes();

            var route = StructuredRoute.FindBestMatch(uri, Routes, parameters);

            if (route != null)
            {
                NavigationParameters[route.Route.Type] = route;
                if (!RouteFactories.TryGetValue(route.Route.BaseUri, out var routeFactory))
                {
                    throw new InvalidOperationException($"A route factory for URI '{uri}' could not be found.");
                }
                await routeFactory.CreateAsync().ConfigureAwait(true);
                await MC.Shell.Current.GoToAsync(route.Route.BaseUri);
            }
            else
            {
                throw new InvalidOperationException($"The route '{uri}' is not registered. Register page routes using the '@page' directive in the page.");
            }
        }

        //TODO This route matching could be better. Can we use the ASPNEt version?
        private List<StructuredRoute> FindRoutes()
        {
            var result = new List<StructuredRoute>();
            var assembly = MC.Application.Current.GetType().Assembly;
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
                if (!TryParse(propertyType, keyValue.Value, out var parsedValue))
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

        /// <summary>
        /// Converts a string into the specified type. If conversion was successful, parsed property will be of the correct type and method will return true.
        /// If conversion fails it will return false and parsed property will be null.
        /// This method supports the 8 data types that are valid navigation parameters in Blazor. Passing a string is also safe but will be returned as is because no conversion is neccessary.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="s"></param>
        /// <param name="result">The parsed object of the type specified. This will be null if conversion failed.</param>
        /// <returns>True if s was converted successfully, otherwise false</returns>
        internal static bool TryParse(Type type, string s, out object result)
        {
            bool success;

            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type == typeof(string))
            {
                result = s;
                success = true;
            }
            else if (type == typeof(int))
            {
                success = int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var parsed);
                result = parsed;
            }
            else if (type == typeof(Guid))
            {
                success = Guid.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(bool))
            {
                success = bool.TryParse(s, out var parsed);
                result = parsed;
            }
            else if (type == typeof(DateTime))
            {
                success = DateTime.TryParse(s, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out var parsed);
                result = parsed;
            }
            else if (type == typeof(decimal))
            {
                success = decimal.TryParse(s, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out var parsed);
                result = parsed;
            }
            else if (type == typeof(double))
            {
                success = double.TryParse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out var parsed);
                result = parsed;
            }
            else if (type == typeof(float))
            {
                success = float.TryParse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out var parsed);
                result = parsed;
            }
            else if (type == typeof(long))
            {
                success = long.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var parsed);
                result = parsed;
            }
            else
            {
                result = null;
                success = false;
            }
            return success;
        }
    }
}
