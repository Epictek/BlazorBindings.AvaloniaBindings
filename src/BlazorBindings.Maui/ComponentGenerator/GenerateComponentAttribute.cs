using System;

namespace BlazorBindings.Maui.ComponentGenerator
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class GenerateComponentAttribute : Attribute
    {
        public GenerateComponentAttribute(Type typeToGenerate)
        {
        }

        public string[] Exclude { get; set; }
        public string[] PropertyChangedEvents { get; set; }
    }
}