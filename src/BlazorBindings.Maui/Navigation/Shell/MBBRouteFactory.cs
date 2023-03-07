// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.ShellNavigation;

//Based on the forms TypeRouteFactory https://github.com/xamarin/Xamarin.Forms/blob/9fd882e6c598a51bffbbb2f4de72c3bd9023ab41/Xamarin.Forms.Core/Routing.cs
internal class MBBRouteFactory : MC.RouteFactory
{
    private readonly Type _componentType;
    private readonly Func<Type, Task<Page>> _pageFactory;
    private MC.Page _element;

    public MBBRouteFactory(Type componentType, Func<Type, Task<MC.Page>> pageFactory)
    {
        _componentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
        _pageFactory = pageFactory ?? throw new ArgumentNullException(nameof(pageFactory));
    }

    public override MC.Element GetOrCreate()
    {
        return _element
            ?? throw new InvalidOperationException("The target element of the Shell navigation is supposed to be created at this point.");
    }

    public override MC.Element GetOrCreate(IServiceProvider services) => GetOrCreate();

    public async Task CreateAsync()
    {
        _element = await _pageFactory(_componentType);
    }

    public override bool Equals(object obj)
    {
        if ((obj is MBBRouteFactory otherRouteFactory))
        {
            return otherRouteFactory._componentType == _componentType;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return _componentType.GetHashCode();
    }
}
