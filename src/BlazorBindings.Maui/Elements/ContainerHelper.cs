using Microsoft.Maui;
using System;
using System.Collections.Generic;

namespace BlazorBindings.Maui.Elements
{
    internal static class ContainerHelper
    {
        public static void SetChild<T>(IList<T> items, IElement previousItem, IElement newItem, int index)
        {
            if (newItem is not null)
            {
                // Set item
                if (newItem is not T typedItem)
                {
                    throw new ArgumentException($"{typeof(T).Name} item expected, but {newItem?.GetType()} found instead.", nameof(newItem));
                }

                if (index < items.Count)
                {
                    items[index] = typedItem;
                }
                else
                {
                    items.Add(typedItem);
                }
            }
            else if (previousItem is not null)
            {
                // Remove item
                if (previousItem is not T typedItem)
                {
                    throw new ArgumentException($"{typeof(T).Name} item expected, but {previousItem?.GetType()} found instead.", nameof(previousItem));
                }

                items.Remove(typedItem);
            }
        }
    }
}
