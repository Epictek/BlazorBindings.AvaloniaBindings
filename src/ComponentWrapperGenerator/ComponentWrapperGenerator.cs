// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using ComponentWrapperGenerator.Extensions;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ComponentWrapperGenerator
{
    public partial class ComponentWrapperGenerator
    {
        const string MauiComponentsNamespace = "BlazorBindings.Maui.Elements";

        private GeneratorSettings Settings { get; }

        public ComponentWrapperGenerator(GeneratorSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public (string GroupName, string Name, string Source) GenerateComponentFile(Compilation compilation, GeneratedComponentInfo generatedInfo)
        {
            //if (!System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            var typeToGenerate = generatedInfo.TypeSymbol;
            var componentName = typeToGenerate.Name;
            var componentNamespace = GetComponentNamespace(typeToGenerate);

            var baseType = GetBaseTypeOfInterest(typeToGenerate);
            var componentBaseName = componentNamespace == GetComponentNamespace(baseType)
                ? baseType.Name
                : $"{GetComponentNamespace(baseType)}.{baseType.Name}";

            // header
            var headerText = Settings.FileHeader;

            // usings
            var usings = new List<UsingStatement>
            {
                new UsingStatement { Namespace = "Microsoft.AspNetCore.Components", IsUsed = true, },
                new UsingStatement { Namespace = "BlazorBindings.Core", IsUsed = true, },
                new UsingStatement { Namespace = "System.Threading.Tasks", IsUsed = true, },
                new UsingStatement { Namespace = "Microsoft.Maui.Controls", Alias = "MC", IsUsed = true }
            };

            var typeNamespace = typeToGenerate.ContainingNamespace.GetFullName();
            if (typeNamespace != "Microsoft.Maui.Controls")
            {
                var typeNamespaceAlias = GetNamespaceAlias(typeToGenerate.ContainingNamespace);
                usings.Add(new UsingStatement { Namespace = typeNamespace, Alias = typeNamespaceAlias, IsUsed = true });
            }

            if (componentNamespace != MauiComponentsNamespace)
            {
                usings.Add(new UsingStatement { Namespace = MauiComponentsNamespace, IsUsed = true });
            }

            var componentNamespacePrefix = GetNamespacePrefix(typeToGenerate, usings);

            // props
            var valueProperties = GeneratedPropertyInfo.GetValueProperties(compilation, typeToGenerate, usings);
            var contentProperties = GeneratedPropertyInfo.GetContentProperties(compilation, typeToGenerate, usings);
            var eventCallbackProperties = GeneratedPropertyInfo.GetEventCallbackProperties(compilation, typeToGenerate, usings);
            var allProperties = valueProperties.Concat(contentProperties).Concat(eventCallbackProperties);
            var propertyDeclarationBuilder = new StringBuilder();
            if (allProperties.Any())
            {
                propertyDeclarationBuilder.AppendLine();
            }
            foreach (var prop in allProperties)
            {
                propertyDeclarationBuilder.Append(prop.GetPropertyDeclaration());
            }
            var propertyDeclarations = propertyDeclarationBuilder.ToString();

            var handlePropertiesBuilder = new StringBuilder();
            foreach (var prop in valueProperties)
            {
                handlePropertiesBuilder.Append(prop.GetHandleValueProperty());
            }
            foreach (var prop in contentProperties)
            {
                handlePropertiesBuilder.Append(prop.GetHandleContentProperty());
            }
            foreach (var prop in eventCallbackProperties)
            {
                handlePropertiesBuilder.Append(prop.GetHandleEventCallbackProperty());
            }
            var handleProperties = handlePropertiesBuilder.ToString();

            var isComponentAbstract = typeToGenerate.IsAbstract || !typeToGenerate.Constructors.Any(c => c.DeclaredAccessibility == Accessibility.Public && c.Parameters.Length == 0);
            var classModifiers = string.Empty;
            if (isComponentAbstract)
            {
                classModifiers += "abstract ";
            }

            var staticConstructorBody = "";
            foreach (var prop in contentProperties)
            {
                staticConstructorBody += prop.GetContentHandlerRegistration();
            }
            staticConstructorBody += "\r\n            RegisterAdditionalHandlers();";

            var createNativeElement = isComponentAbstract ? "" : $@"
        protected override MC.Element CreateNativeElement() => new {GetTypeNameAndAddNamespace(typeToGenerate, usings)}();";

            var handleParameter = !allProperties.Any() ? "" : $@"
        protected override void HandleParameter(string name, object value)
        {{
            switch (name)
            {{
                {handleProperties.Trim()}

                default:
                    base.HandleParameter(name, value);
                    break;
            }}
        }}";

            var renderAdditionalElementContent = !contentProperties.Any() ? "" : $@"

        protected override void RenderAdditionalElementContent({GetTypeNameAndAddNamespace("Microsoft.AspNetCore.Components.Rendering", "RenderTreeBuilder", usings)} builder, ref int sequence)
        {{
            base.RenderAdditionalElementContent(builder, ref sequence);{string.Concat(contentProperties.Select(prop => prop.RenderContentProperty()))};
        }}";


            var usingsText = string.Join(
                Environment.NewLine,
                usings
                    .Distinct()
                    .Where(u => u.Namespace != componentNamespace)
                    .Where(u => u.IsUsed)
                    .OrderBy(u => u.ComparableString)
                    .Select(u => u.UsingText));

            var outputBuilder = new StringBuilder();
            outputBuilder.Append($@"{headerText}
{usingsText}

namespace {componentNamespace}
{{
    public {classModifiers}partial class {componentName} : {componentBaseName}
    {{
        static {componentName}()
        {{
            {staticConstructorBody.Trim()}
        }}
{propertyDeclarations}
        public new {componentNamespacePrefix}{componentName} NativeControl => ({componentNamespacePrefix}{componentName})((Element)this).NativeControl;
{createNativeElement}
{handleParameter}{renderAdditionalElementContent}

        static partial void RegisterAdditionalHandlers();
    }}
}}
");

            return (GetComponentGroup(typeToGenerate), componentName, outputBuilder.ToString());
        }

        private static string GetNamespacePrefix(INamedTypeSymbol type, List<UsingStatement> usings)
        {
            // Check if there's a 'using' already. If so, check if it has an alias. If not, add a new 'using'.
            var namespaceAlias = string.Empty;
            var namespaceName = type.ContainingNamespace.GetFullName();

            var existingUsing = usings.FirstOrDefault(u => u.Namespace == namespaceName);
            if (existingUsing == null)
            {
                usings.Add(new UsingStatement { Namespace = namespaceName, IsUsed = true, });
                return string.Empty;
            }
            else
            {
                existingUsing.IsUsed = true;
                if (existingUsing.Alias != null)
                {
                    return existingUsing.Alias + ".";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal static string GetXmlDocContents(IPropertySymbol prop, string indent)
        {
            var xmlDocString = prop.GetDocumentationCommentXml();

            if (string.IsNullOrEmpty(xmlDocString))
            {
                return null;
            }

            var xmlDoc = new XmlDocument();
            // Returned XML doc string has no root element, which does not allow to parse it.
            xmlDoc.LoadXml($"<member>{xmlDocString}</member>");
            var xmlDocNode = xmlDoc.FirstChild;

            var xmlDocContents = string.Empty;
            // Format of XML docs we're looking for in a given property:
            // <member name="P:Xamarin.Forms.ActivityIndicator.Color">
            //     <summary>Gets or sets the <see cref="T:Xamarin.Forms.Color" /> of the ActivityIndicator. This is a bindable property.</summary>
            //     <value>A <see cref="T:Xamarin.Forms.Color" /> used to display the ActivityIndicator. Default is <see cref="P:Xamarin.Forms.Color.Default" />.</value>
            //     <remarks />
            // </member>

            var summaryText = GetXmlDocText(xmlDocNode["summary"]);
            var valueText = GetXmlDocText(xmlDocNode["value"]);

            if (summaryText != null || valueText != null)
            {
                var xmlDocContentBuilder = new StringBuilder();
                if (summaryText != null)
                {
                    xmlDocContentBuilder.AppendLine($"{indent}/// <summary>");
                    xmlDocContentBuilder.AppendLine($"{indent}/// {summaryText}");
                    xmlDocContentBuilder.AppendLine($"{indent}/// </summary>");
                }
                if (valueText != null)
                {
                    xmlDocContentBuilder.AppendLine($"{indent}/// <value>");
                    xmlDocContentBuilder.AppendLine($"{indent}/// {valueText}");
                    xmlDocContentBuilder.AppendLine($"{indent}/// </value>");
                }
                xmlDocContents = xmlDocContentBuilder.ToString();
            }
            return xmlDocContents;

            static string GetXmlDocText(XmlElement xmlDocElement)
            {
                var allText = xmlDocElement?.InnerXml;
                allText = allText.Replace("To be added.", string.Empty);
                return string.IsNullOrWhiteSpace(allText) ? null : allText;
            }
        }

        internal static string GetTypeNameAndAddNamespace(string @namespace, string typeName, IList<UsingStatement> usings)
        {
            var @using = usings.FirstOrDefault(u => u.Namespace == @namespace);
            if (@using == null)
            {
                @using = new UsingStatement { Namespace = @namespace, IsUsed = true };
                usings.Add(@using);
            }
            else
            {
                @using.IsUsed = true;
            }

            return @using.Alias == null ? typeName : $"{@using.Alias}.{typeName}";
        }

        internal static string GetTypeNameAndAddNamespace(ITypeSymbol type, IList<UsingStatement> usings)
        {
            var typeName = GetCSharpType(type);
            if (typeName != null)
            {
                return typeName;
            }

            // Check if there's a 'using' already. If so, check if it has an alias. If not, add a new 'using'.
            var namespaceAlias = string.Empty;

            var nsName = type.ContainingNamespace.GetFullName();
            var existingUsing = usings.FirstOrDefault(u => u.Namespace == nsName);
            if (existingUsing == null)
            {
                usings.Add(new UsingStatement { Namespace = nsName, IsUsed = true, });
            }
            else
            {
                existingUsing.IsUsed = true;
                if (existingUsing.Alias != null)
                {
                    namespaceAlias = existingUsing.Alias + ".";
                }
            }
            typeName = namespaceAlias + FormatTypeName(type, usings);
            return typeName;
        }

        private static string FormatTypeName(ITypeSymbol type, IList<UsingStatement> usings)
        {
            if (type is not INamedTypeSymbol namedType || !namedType.IsGenericType)
            {
                return type.Name;
            }
            var typeNameBuilder = new StringBuilder();
            typeNameBuilder.Append(type.Name);
            typeNameBuilder.Append('<');
            var genericArgs = namedType.TypeArguments;
            for (var i = 0; i < genericArgs.Length; i++)
            {
                if (i > 0)
                {
                    typeNameBuilder.Append(", ");
                }
                typeNameBuilder.Append(GetTypeNameAndAddNamespace(genericArgs[i], usings));

            }
            typeNameBuilder.Append('>');
            return typeNameBuilder.ToString();
        }


        private static readonly Dictionary<SpecialType, string> TypeToCSharpName = new()
        {
            { SpecialType.System_Boolean, "bool" },
            { SpecialType.System_Byte, "byte" },
            { SpecialType.System_SByte, "sbyte" },
            { SpecialType.System_Char, "char" },
            { SpecialType.System_Decimal, "decimal" },
            { SpecialType.System_Double, "double" },
            { SpecialType.System_Single, "float" },
            { SpecialType.System_Int32, "int" },
            { SpecialType.System_UInt32, "uint" },
            { SpecialType.System_Int64, "long" },
            { SpecialType.System_UInt64, "ulong" },
            { SpecialType.System_Object, "object" },
            { SpecialType.System_Int16, "short" },
            { SpecialType.System_UInt16, "ushort" },
            { SpecialType.System_String, "string" },
        };

        private static string GetCSharpType(ITypeSymbol propertyType)
        {
            return TypeToCSharpName.TryGetValue(propertyType.SpecialType, out var typeName) ? typeName : null;
        }

        /// <summary>
        /// Finds the next non-generic base type of the specified type. This matches the Mobile Blazor Bindings
        /// model where there is no need to represent the intermediate generic base classes because they are
        /// generally only containers and have no API functionality that needs to be generated.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static INamedTypeSymbol GetBaseTypeOfInterest(INamedTypeSymbol type)
        {
            do
            {
                type = type.BaseType;
                if (!type.IsGenericType)
                {
                    return type;
                }
            }
            while (type != null);

            return null;
        }

        public static string GetIdentifierName(string possibleIdentifier)
        {
            return ReservedKeywords.Contains(possibleIdentifier, StringComparer.Ordinal)
                ? $"@{possibleIdentifier}"
                : possibleIdentifier;
        }

        private static readonly List<string> ReservedKeywords = new List<string>
            { "class", };

        private string GetComponentGroup(INamedTypeSymbol typeToGenerate)
        {
            var assemblyName = typeToGenerate.ContainingAssembly.Name;

            if (assemblyName == "Microsoft.Maui.Controls")
            {
                var nsName = typeToGenerate.ContainingNamespace.GetFullName();
                return nsName == "Microsoft.Maui.Controls" ? "" : nsName.Replace("Microsoft.Maui.Controls.", "");
            }
            else
            {
                return assemblyName.Replace(".Maui", "").Replace(".Views", "").Replace(".Controls", "");
            }
        }

        private string GetComponentNamespace(INamedTypeSymbol typeToGenerate)
        {
            var group = GetComponentGroup(typeToGenerate);
            return string.IsNullOrEmpty(group) ? "BlazorBindings.Maui.Elements" : $"BlazorBindings.Maui.Elements.{group}";
        }

        private static string GetNamespaceAlias(INamespaceSymbol namespaceSymbol)
        {
            var alias = "";
            while (!namespaceSymbol.IsGlobalNamespace)
            {
                if (namespaceSymbol.Name != "Microsoft")
                    alias = namespaceSymbol.Name[0] + alias;

                namespaceSymbol = namespaceSymbol.ContainingNamespace;
            }

            return alias;
        }
    }
}
