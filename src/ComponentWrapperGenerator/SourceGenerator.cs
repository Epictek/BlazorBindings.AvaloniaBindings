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
                FileHeader = "// Copyright (c) Microsoft Corporation.\r\n// Licensed under the MIT license.\r\n",
                RootNamespace = "BlazorBindings.Maui.Elements"
            }, new[] {
                "Microsoft.Maui.Controls" });

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
                "GenerateComponentAttribute.generated.cs",
                GenerateComponentAttribute));

            var enumDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // Select assembly targeted attributes.
                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // 
                .Where(static m => m != null);

            var compilationAndEnums = context.CompilationProvider.Combine(enumDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndEnums,
                static (spc, source) => Execute(source.Left, source.Right, spc));
        }

        static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        {
            return node is AttributeSyntax attribute
                    && attribute.Parent is AttributeListSyntax attributeList && attributeList.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword) == true;
        }

        static INamedTypeSymbol GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var attributeSyntax = (AttributeSyntax)context.Node;

            var typeName = context.SemanticModel.GetTypeInfo(attributeSyntax).Type?.ToDisplayString();

            if (typeName != "BlazorBindings.Maui.ComponentGenerator.GenerateComponentAttribute")
                return null;

            if (attributeSyntax.ArgumentList.Arguments.FirstOrDefault()?.Expression is not TypeOfExpressionSyntax typeOf)
                return null;

            var typeSymbol = context.SemanticModel.GetSymbolInfo(typeOf.Type).Symbol;

            return typeSymbol as INamedTypeSymbol;
        }

        static void Execute(Compilation compilation, ImmutableArray<INamedTypeSymbol> types, SourceProductionContext context)
        {
            foreach (var type in types)
            {
                var result = componentWrapperGenerator.GenerateComponentFile(compilation, type);
                context.AddSource($"{type.Name}.generated.cs", result);
            }
        }
    }
}
