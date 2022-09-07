using System;

namespace BlazorBindings.Maui.ComponentGenerator
{
    [System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = true)]
    public class GenerateComponentAttribute : System.Attribute
    {
        private readonly Type typeToGenerate;

        public GenerateComponentAttribute(System.Type typeToGenerate)
        {
            this.typeToGenerate = typeToGenerate;
        }
    }
}