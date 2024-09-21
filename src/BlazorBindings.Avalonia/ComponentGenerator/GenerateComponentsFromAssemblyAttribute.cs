namespace BlazorBindings.Avalonia.ComponentGenerator;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenerateComponentsFromAssemblyAttribute : Attribute
{
    public GenerateComponentsFromAssemblyAttribute(Type containingType) { }

    public string TypeNamePrefix { get; set; }
}