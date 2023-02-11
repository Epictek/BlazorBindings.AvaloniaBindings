using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorBindings.Maui
{
    internal static class RenderFragments
    {
        public static RenderFragment FromComponentType(Type componentType, Dictionary<string, object> parameters = null)
        {
            return builder =>
            {
                builder.OpenComponent(1, componentType);

                if (parameters != null)
                {
                    builder.AddMultipleAttributes(2, parameters);
                }

                builder.CloseComponent();
            };
        }
    }
}
