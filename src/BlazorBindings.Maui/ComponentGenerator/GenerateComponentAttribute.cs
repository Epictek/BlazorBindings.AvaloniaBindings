using System;

namespace BlazorBindings.Maui.ComponentGenerator
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class GenerateComponentAttribute : Attribute
    {
        public GenerateComponentAttribute(Type typeToGenerate)
        {
        }

        /// <summary>
        /// Exclude members from generation.
        /// </summary>
        public string[] Exclude { get; set; }

        /// <summary>
        /// Generate EventCallback for property based on PropertyChanged event.
        /// </summary>
        public string[] PropertyChangedEvents { get; set; }
    }
}