// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Buildalyzer;
using Buildalyzer.Workspaces;
using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentWrapperGenerator
{
    public class Program
    {
        private static readonly ComponentWrapperGenerator componentWrapperGenerator = new(
            new GeneratorSettings
            {
                FileHeader = "// Copyright (c) Microsoft Corporation.\r\n// Licensed under the MIT license.\r\n"
            });

        public static async Task Main()
        {
            var csprojPath = @"C:\Users\ollia\source\repos\MobileBlazorBindings\src\BlazorBindings.Maui\BlazorBindings.Maui.csproj";
            var outPath = Path.Combine(csprojPath, "..", "Elements");

            AnalyzerManager manager = new AnalyzerManager();
            IProjectAnalyzer analyzer = manager.GetProject(csprojPath);
            AdhocWorkspace workspace = analyzer.GetWorkspace();
            var compilation = await workspace.CurrentSolution.Projects.First().GetCompilationAsync();

            var attributes = compilation.Assembly.GetAttributes();
            var typesToGenerate = attributes
                .Where(a => a.AttributeClass?.ToDisplayString() == "BlazorBindings.Maui.ComponentGenerator.GenerateComponentAttribute")
                .Select(a => a.ConstructorArguments.FirstOrDefault().Value as INamedTypeSymbol)
                .Where(type => type != null)
                .Select(type => new GeneratedComponentInfo { TypeSymbol = type });

             foreach (var generatedType in typesToGenerate)
            {
                var (groupName, name, source) = componentWrapperGenerator.GenerateComponentFile(compilation, generatedType);

                var fileName = $"{name}.generated.cs";
                var path = string.IsNullOrEmpty(groupName)
                    ? Path.Combine(outPath, fileName)
                    : Path.Combine(outPath, groupName, fileName);

                Directory.GetParent(path).Create();

                File.WriteAllText(path, source);
            }
        }
    }
}
