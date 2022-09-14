using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace ComponentWrapperGenerator.Extensions
{
    internal static class SymbolExtensions
    {
        public static IMethodSymbol GetMethod(this ITypeSymbol typeSymbol, string method)
        {
            return typeSymbol.GetMembers(method).FirstOrDefault() as IMethodSymbol;
        }

        public static IEventSymbol GetEvent(this ITypeSymbol typeSymbol, string eventName, bool includeBaseTypes)
        {
            var currentType = typeSymbol;

            while (currentType != null)
            {
                var eventSymbol = currentType.GetMembers(eventName).FirstOrDefault() as IEventSymbol;

                if (eventSymbol != null || !includeBaseTypes)
                    return eventSymbol;

                currentType = currentType.BaseType;
            }


            return null;
        }

        public static IPropertySymbol GetProperty(this ITypeSymbol typeSymbol, string propName)
        {
            return typeSymbol.GetMembers(propName).FirstOrDefault() as IPropertySymbol;
        }

        public static string GetFullName(this INamespaceOrTypeSymbol namespaceOrType)
        {
            var stack = new Stack<string>();

            stack.Push(GetName(namespaceOrType));

            if (namespaceOrType.ContainingType != null)
            {
                stack.Push(GetName(namespaceOrType.ContainingType));
            }

            var currentNamespace = namespaceOrType.ContainingNamespace;
            while (!currentNamespace.IsGlobalNamespace)
            {
                stack.Push(currentNamespace.Name);
                currentNamespace = currentNamespace.ContainingNamespace;
            }

            return string.Join(".", stack);
        }

        /// <summary>
        /// Returns name with generic type arguments (if any).
        /// </summary>
        private static string GetName(INamespaceOrTypeSymbol namespaceOrType)
        {
            if (namespaceOrType is INamedTypeSymbol namedType && namedType.IsGenericType)
            {
                var genericTypesNames = string.Join(", ", namedType.TypeArguments.Select(GetFullName));
                return $"{namedType.Name}<{genericTypesNames}>";
            }
            else
            {
                return namespaceOrType.Name;
            }
        }

        public static bool IsHidingMember(this ISymbol symbol)
        {
            var currentType = symbol.ContainingType?.BaseType;

            while (currentType != null)
            {
                var containsMember = currentType.GetMembers(symbol.Name).Any(s => s.Kind == symbol.Kind);
                if (containsMember)
                    return true;

                currentType = currentType.BaseType;
            }

            return false;
        }

        public static bool IsNullableStruct(this INamedTypeSymbol symbol)
        {
            return symbol.IsGenericType && symbol.ConstructedFrom.SpecialType == SpecialType.System_Nullable_T;
        }
    }
}