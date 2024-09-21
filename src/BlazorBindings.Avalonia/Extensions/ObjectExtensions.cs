namespace BlazorBindings.Avalonia.Extensions;

public static class ObjectExtensions
{
    public static T Cast<T>(this object obj)
    {
        if (obj is null && !typeof(T).IsValueType)
            return (T)(object)null;

        if (obj is T tObj)
            return tObj;

        throw new InvalidCastException($"{typeof(T).FullName} instance expected, but {obj.GetType().FullName} found.");
    }
}
