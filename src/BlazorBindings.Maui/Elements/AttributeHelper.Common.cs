// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        public static bool GetBool(object value, bool defaultValueIfNull = default)
        {
            return value switch
            {
                null => defaultValueIfNull,
                bool b => b,
                string str when str == "1" => true,
                string str when str == "0" => false,
                string str when bool.TryParse(str, out var bln) => bln,
                _ => throw new NotSupportedException($"Cannot get bool value from {value.GetType().Name} attribute.")
            };
        }

        public static int GetInt(object value, int defaultValueIfNull = default)
        {
            return value switch
            {
                null => defaultValueIfNull,
                int i => i,
                string str => int.Parse(str, CultureInfo.InvariantCulture),
                _ => throw new NotSupportedException($"Cannot get int value from {value.GetType().Name} attribute.")
            };
        }

        public static T GetEnum<T>(object value, T defaultValueIfNull = default) where T : struct, Enum
        {
            return value switch
            {
                null => defaultValueIfNull,
                T t => t,
                string str when int.TryParse(str, out var i) => Unsafe.As<int, T>(ref i),
                string str when Enum.TryParse<T>(str, out var e) => e,
                _ => throw new NotSupportedException($"Cannot get {typeof(T).Name} value from {value.GetType().Name} attribute.")
            };
        }

        /// <summary>
        /// Helper method to deserialize <see cref="float" /> objects.
        /// </summary>
        public static float GetSingle(object value, float defaultValueIfNull = default)
        {
            return value switch
            {
                null => defaultValueIfNull,
                float f => f,
                int i => i,
                string str => float.Parse(str, CultureInfo.InvariantCulture),
                _ => throw new ArgumentException("Cannot convert value to Single.", nameof(value))
            };
        }

        /// <summary>
        /// Helper method to serialize <see cref="uint" /> objects.
        /// </summary>
        public static string UInt32ToString(uint value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="uint" /> objects.
        /// </summary>
        public static uint StringToUInt32(string uintString, uint defaultValueIfNull = default)
        {
            if (uintString is null)
            {
                return defaultValueIfNull;
            }
            return uint.Parse(uintString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parses the attribute value as a space-separated string. Entries are trimmed and empty entries are removed.
        /// </summary>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static IList<string> GetStringList(object attributeValue)
        {
            return ((string)attributeValue)?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
        }
    }
}
