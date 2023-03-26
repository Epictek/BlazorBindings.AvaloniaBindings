using BlazorBindings.Maui.ShellNavigation;
using System.Globalization;
using System.Reflection;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui;

public partial class Navigation
{
    private List<StructuredRoute> Routes;

    /// <summary>
    /// Performs URI-based navigation.
    /// </summary>
    /// <param name="uri">URI to navigate to.</param>
    /// <param name="parameters">Additional parameters to set for component.</param>
    public async Task NavigateToAsync(string uri, Dictionary<string, object> parameters = null)
    {
        ArgumentNullException.ThrowIfNull(uri);

        Routes ??= FindRoutes();

        var route = StructuredRoute.FindBestMatch(uri, Routes, parameters);

        if (route != null)
        {
            var pars = GetParameters(route);
            await Navigate(route.Route.Type, pars, NavigationTarget.Navigation, true);
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

                //Also register route in our own list for setting parameters and tracking if it is registered;
                result.Add(structuredRoute);
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

    private Dictionary<string, object> GetParameters(StructuredRouteResult route)
    {
        var parameters = ConvertParameters(route.Route.Type, route.PathParameters);

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

        return parameters;
    }
}
