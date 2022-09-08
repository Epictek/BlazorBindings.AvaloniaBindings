// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Buildalyzer;
using Buildalyzer.Workspaces;
using CommandLine;
using Microsoft.CodeAnalysis;
using System;
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

        public static async Task Main(string[] args)
        {
            await Parser.Default
                .ParseArguments<Options>(args)
                .WithParsedAsync(async o =>
                {
                    if (o.ProjectPath is null)
                    {
                        o.ProjectPath = Directory.GetFiles(Directory.GetCurrentDirectory()).FirstOrDefault(f
                            => f.EndsWith(".csproj", StringComparison.InvariantCultureIgnoreCase))
                            ?? throw new Exception("Cannot find any csproj files.");
                    }
                    if (o.OutPath is null)
                    {
                        o.OutPath = Path.Combine(o.ProjectPath, "..", "Elements");
                    }

                    var compilation = await CreateComplitation(o);

                    var typesToGenerate = GetTypesToGenerate(compilation);

                    Console.WriteLine($"Generating {typesToGenerate.Length} files.");

                    foreach (var generatedType in typesToGenerate)
                    {
                        var (groupName, name, source) = componentWrapperGenerator.GenerateComponentFile(compilation, generatedType);

                        var fileName = $"{name}.generated.cs";
                        var path = string.IsNullOrEmpty(groupName)
                            ? Path.Combine(o.OutPath, fileName)
                            : Path.Combine(o.OutPath, groupName, fileName);

                        Directory.GetParent(path).Create();

                        File.WriteAllText(path, source);
                    }
                });
        }

        private static GeneratedComponentInfo[] GetTypesToGenerate(Compilation compilation)
        {
            Console.WriteLine("Finding types to generate.");

            var attributes = compilation.Assembly.GetAttributes();
            var typesToGenerate = attributes
                .Where(a => a.AttributeClass?.ToDisplayString() == "BlazorBindings.Maui.ComponentGenerator.GenerateComponentAttribute")
                .Select(a =>
                {
                    return new GeneratedComponentInfo
                    {
                        TypeSymbol = a.ConstructorArguments.FirstOrDefault().Value as INamedTypeSymbol,
                        Exclude = GetNamedArgumentValues<string>(a, "Exclude").ToHashSet()
                    };
                })
                .Where(type => type.TypeSymbol != null)
                .ToArray();
            return typesToGenerate;
        }

        private static T[] GetNamedArgumentValues<T>(AttributeData attribute, string name) where T : class
        {
            var argumentConstant = attribute.NamedArguments.FirstOrDefault(a => a.Key == name).Value;

            if (argumentConstant.Kind != TypedConstantKind.Array)
                return Array.Empty<T>();

            var values = argumentConstant.Values;

            if (values.IsDefaultOrEmpty)
                return Array.Empty<T>();

            return values.Select(a => a.Value as T).Where(v => v is not null).ToArray();
        }

        private static async Task<Compilation> CreateComplitation(Options o)
        {
            Console.WriteLine("Creating project compilation.");

            AnalyzerManager manager = new AnalyzerManager();
            IProjectAnalyzer analyzer = manager.GetProject(o.ProjectPath);
            AdhocWorkspace workspace = analyzer.GetWorkspace();
            var compilation = await workspace.CurrentSolution.Projects.First().GetCompilationAsync();
            return compilation;
        }

        private class Options
        {
            [Value(0, HelpText = "Project file path to run generator.")]
            public string ProjectPath { get; set; }

            [Option('o', "out-path", HelpText = "Out path for generated files.")]
            public string OutPath { get; set; }
        }
    }
}
