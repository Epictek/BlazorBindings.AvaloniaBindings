using System.Globalization;

namespace BlazorBindings.Maui;

internal static class StringConverter
{
    /// <summary>
    /// Converts a string into the specified type. If conversion was successful, parsed property will be of the correct type and method will return true.
    /// If conversion fails it will return false and parsed property will be null.
    /// This method supports the 8 data types that are valid navigation parameters in Blazor. Passing a string is also safe but will be returned as is because no conversion is neccessary.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="s"></param>
    /// <param name="cultureInfo"></param>
    /// <param name="result">The parsed object of the type specified. This will be null if conversion failed.</param>
    /// <returns>True if s was converted successfully, otherwise false</returns>
    internal static bool TryParse(Type type, string s, CultureInfo cultureInfo, out object result)
    {
        if (type == typeof(string))
        {
            result = s;
            return true;
        }

        bool success;
        type = Nullable.GetUnderlyingType(type) ?? type;

        if (type == typeof(int))
        {
            success = int.TryParse(s, NumberStyles.Integer, cultureInfo, out var parsed);
            result = parsed;
        }
        else if (type == typeof(Guid))
        {
            success = Guid.TryParse(s, cultureInfo, out var parsed);
            result = parsed;
        }
        else if (type == typeof(bool))
        {
            success = bool.TryParse(s, out var parsed);
            result = parsed;
        }
        else if (type == typeof(DateTime))
        {
            success = DateTime.TryParse(s, cultureInfo, DateTimeStyles.None, out var parsed);
            result = parsed;
        }
        else if (type == typeof(DateOnly))
        {
            success = DateOnly.TryParse(s, cultureInfo, DateTimeStyles.None, out var parsed);
            result = parsed;
        }
        else if (type == typeof(TimeOnly))
        {
            success = TimeOnly.TryParse(s, cultureInfo, DateTimeStyles.None, out var parsed);
            result = parsed;
        }
        else if (type == typeof(decimal))
        {
            success = decimal.TryParse(s, NumberStyles.Number, cultureInfo, out var parsed);
            result = parsed;
        }
        else if (type == typeof(double))
        {
            success = double.TryParse(s, NumberStyles.Float, cultureInfo, out var parsed);
            result = parsed;
        }
        else if (type == typeof(float))
        {
            success = float.TryParse(s, NumberStyles.Float, cultureInfo, out var parsed);
            result = parsed;
        }
        else if (type == typeof(long))
        {
            success = long.TryParse(s, NumberStyles.Integer, cultureInfo, out var parsed);
            result = parsed;
        }
        else if (type.IsEnum)
        {
            success = Enum.TryParse(type, s, ignoreCase: true, out var parsed);
            result = parsed;

            if (success && !Enum.IsDefined(type, result))
            {
                success = false;
                result = null;
            }
        }
        else
        {
            result = null;
            success = false;
        }

        if (!success)
            result = null;

        return success;
    }
}
