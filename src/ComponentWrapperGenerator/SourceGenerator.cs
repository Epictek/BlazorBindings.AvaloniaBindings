using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace ComponentWrapperGenerator
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        private const string GenerateComponentAttribute =
@"namespace BlazorBindings.Maui.ComponentGenerator
{
    [System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = true)]
    internal class GenerateComponentAttribute : System.Attribute
    { 
        public GenerateComponentAttribute(System.Type typeToGenerate)
        {
        }

        public string[] Aliases { get; set; }
    }
}";

        private static readonly ComponentWrapperGenerator componentWrapperGenerator = new(
            new GeneratorSettings
            {
                FileHeader = "// Copyright (c) Microsoft Corporation.\r\n// Licensed under the MIT license.\r\n"
            });

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
                "GenerateComponentAttribute.generated.cs",
                GenerateComponentAttribute));

            var enumDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // Select assembly targeted attributes.
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
                .Where(static m => m != null);

            var compilationAndEnums = context.CompilationProvider.Combine(enumDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndEnums,
                static (spc, source) => Execute(source.Left, source.Right, spc));
        }

        static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        {
            return node is AttributeSyntax attribute
                && attribute.Parent is AttributeListSyntax attributeList
                && attributeList.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword) == true;
        }

        static GeneratedComponentInfo GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var attributeSyntax = (AttributeSyntax)context.Node;

            if (attributeSyntax.ArgumentList.Arguments.FirstOrDefault()?.Expression is not TypeOfExpressionSyntax typeOf)
                return null;

            var attributeTypeSymbol = context.SemanticModel.GetTypeInfo(attributeSyntax).Type;

            if (attributeTypeSymbol?.ToDisplayString() != "BlazorBindings.Maui.ComponentGenerator.GenerateComponentAttribute")
                return null;

            var typeSymbol = context.SemanticModel.GetSymbolInfo(typeOf.Type).Symbol;

            if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
                return null;

            return new GeneratedComponentInfo
            {
                TypeSymbol = namedTypeSymbol
            };
        }

        static void Execute(Compilation compilation, ImmutableArray<GeneratedComponentInfo> types, SourceProductionContext context)
        {
            foreach (var generatedInfo in types)
            {
                var result = componentWrapperGenerator.GenerateComponentFile(compilation, generatedInfo);
                context.AddSource($"{generatedInfo.TypeSymbol.Name}.generated.cs", result);
            }
        }
    }
}
