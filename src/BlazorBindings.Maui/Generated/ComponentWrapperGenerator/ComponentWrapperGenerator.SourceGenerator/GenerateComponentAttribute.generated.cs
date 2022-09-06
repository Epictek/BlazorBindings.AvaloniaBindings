namespace BlazorBindings.Maui.ComponentGenerator
{
    [System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = true)]
    internal class GenerateComponentAttribute : System.Attribute
    { 
        public GenerateComponentAttribute(System.Type typeToGenerate)
        {
        }

        public string[] Aliases { get; set; }
    }
}