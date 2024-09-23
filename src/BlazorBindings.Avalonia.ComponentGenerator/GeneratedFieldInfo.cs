using BlazorBindings.Avalonia.ComponentGenerator.Extensions;
using CommandLine;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BlazorBindings.Avalonia.ComponentGenerator;

public enum GeneratedFieldKind
{
    AttachedProperty,
    RenderFragment
}
public partial class GeneratedFieldInfo
{
    private readonly IFieldSymbol _fieldInfo;
    private readonly GeneratedTypeInfo _typeInfo;
    private Lazy<string> _componentFieldNameLazy;
    //private Lazy<string> _componentTypeLazy;

    public GeneratedTypeInfo ContainingType { get; set; }
    public GeneratedFieldKind Kind { get; }
    public string AvaloniaFieldName { get; set; }
    public string AvaloniaContainingTypeName { get; }
    public ITypeSymbol HostType { get; }
    public string ComponentName { get; }
    public Compilation Compilation { get; }
    public bool IsGeneric { get; }
    public INamedTypeSymbol GenericTypeArgument { get; }
    public string ComponentFieldName
    {
        get => _componentFieldNameLazy.Value;
        set => _componentFieldNameLazy = new Lazy<string>(value);
    }

    public bool IsRenderFragmentProperty => Kind == GeneratedFieldKind.RenderFragment;


    //public string ComponentType
    //{
    //    get => _componentTypeLazy.Value;
    //    set => _componentTypeLazy = new Lazy<string>(value);
    //}

    private GeneratedFieldInfo(GeneratedTypeInfo typeInfo,
                                  string avaloniaFieldName,
                                  string avaloniaContainingTypeName,
                                  string componentFieldName,
                                  string componentType,
                                  GeneratedFieldKind kind)
    {
        ContainingType = typeInfo;
        Compilation = typeInfo.Compilation;
        ComponentName = typeInfo.TypeName;
        Kind = kind;
        AvaloniaFieldName = avaloniaFieldName;
        AvaloniaContainingTypeName = avaloniaContainingTypeName;
        //_componentTypeLazy = new Lazy<string>(componentType);
        _componentFieldNameLazy = new Lazy<string>(componentFieldName);
    }

    private GeneratedFieldInfo(GeneratedTypeInfo typeInfo, IFieldSymbol fieldInfo, GeneratedFieldKind kind)
    {

        _fieldInfo = fieldInfo;
        _typeInfo = typeInfo;
        Kind = kind;
        if (typeInfo.TypeName.StartsWith("ToolTip"))
        {

        }


        ContainingType = typeInfo;
        Compilation = typeInfo.Compilation;
        ComponentName = typeInfo.TypeName;
        AvaloniaFieldName = ComponentWrapperGenerator.GetIdentifierName(fieldInfo.Name);
        IsGeneric = typeInfo.Settings.GenericProperties.TryGetValue(fieldInfo.Name, out var genericTypeArgument);
        GenericTypeArgument = genericTypeArgument;
        AvaloniaContainingTypeName = GetTypeNameAndAddNamespace(fieldInfo.ContainingType);

        HostType = fieldInfo.ContainingType.GetMethod("Get" + fieldInfo.Name[..^8])?
            .Parameters[0].Type;

        ComponentName = ComponentWrapperGenerator.GetIdentifierName(_fieldInfo.ContainingType.Name);
        _componentFieldNameLazy = new Lazy<string>(GetComponentFieldName);
        //_componentTypeLazy = new Lazy<string>(() => GetComponentFieldTypeName(_fieldInfo, typeInfo, makeNullable: true));

        string GetComponentFieldName()
        {
            if (ContainingType.Settings.Aliases.TryGetValue(AvaloniaFieldName, out var aliasName))
                return aliasName;

            if (IsRenderFragmentProperty &&
                ((INamedTypeSymbol)fieldInfo.Type).TypeArguments[0].Name == "Object")
            {
                return "ChildContent";
            }

            return ComponentWrapperGenerator.GetIdentifierName(_fieldInfo.Name[..^8]);
        }
    }

    public string GetAttachedPropertyType()
    {
        return GetAttachedPropertyTypeName(_fieldInfo, ContainingType, IsRenderFragmentProperty, makeNullable: false);
    }

    public string GetOriginalAttachedPropertyType()
    {
        return GetOriginalAttachedPropertyTypeName(_fieldInfo, ContainingType, IsRenderFragmentProperty, makeNullable: false);
    }

    public string GetAttachedPropertyNameWithoutSuffix()
    {
        return _fieldInfo.Name[..^8];
    }

    public string GetRegisterAttachedPropertyDeclaration()
    {
        // razor compiler doesn't allow 'new' properties, it considers them as duplicates.
        if (_fieldInfo is not null && _fieldInfo.IsHidingMember())
        {
            return "";
        }

        const string indent = "        ";
        var xmlDocContents = _fieldInfo is null ? "" : ComponentWrapperGenerator.GetXmlDocContents(_fieldInfo, indent);

        var effectiveBindingHostType = HostType.GetFullName();

        var effectiveAvaloniaFieldName = AvaloniaFieldName[..^8];

        return $$"""
            {{indent}}    AttachedPropertyRegistry.RegisterAttachedPropertyHandler("{{ComponentName}}.{{ComponentFieldName}}",
            {{indent}}        (element, value) => 
            {{indent}}        {
            {{indent}}            if (value?.Equals(AvaloniaProperty.UnsetValue) == true)
            {{indent}}            {
            {{indent}}                element.ClearValue({{AvaloniaContainingTypeName}}.{{AvaloniaFieldName}});
            {{indent}}            }
            {{indent}}            else
            {{indent}}            {
            {{indent}}                {{_typeInfo.AvaloniaType.GetFullName()}}.Set{{effectiveAvaloniaFieldName}}(({{effectiveBindingHostType}})element, ({{GetOriginalAttachedPropertyType()}})value);
            {{indent}}            }
            {{indent}}        });
            """;
    }

    public string GetExtensionMethodDeclaration()
    {
        // razor compiler doesn't allow 'new' properties, it considers them as duplicates.
        if (_fieldInfo is not null && _fieldInfo.IsHidingMember())
        {
            return "";
        }

        const string indent = "        ";
        var xmlDocContents = _fieldInfo is null ? "" : ComponentWrapperGenerator.GetXmlDocContents(_fieldInfo, indent);

        var effectiveBindingHostType = HostType.Name;

        return $$"""
            {{xmlDocContents}}{{indent}}public static {{effectiveBindingHostType}} {{ComponentName}}{{ComponentFieldName}}(this {{effectiveBindingHostType}} element, {{GetAttachedPropertyType()}} value)
            {{indent}}{
            {{indent}}    element.AttachedProperties["{{ComponentName}}.{{ComponentFieldName}}"] = value;
            {{indent}}
            {{indent}}    return element;
            {{indent}}}
            """;
    }

    public string GetFieldDeclaration()
    {
        // razor compiler doesn't allow 'new' properties, it considers them as duplicates.
        if (_fieldInfo is not null && _fieldInfo.IsHidingMember())
        {
            return "";
        }

        const string indent = "        ";

        var xmlDocContents = _fieldInfo is null ? "" : ComponentWrapperGenerator.GetXmlDocContents(_fieldInfo, indent);

        return $@"{xmlDocContents}{indent}[Parameter] public {GetAttachedPropertyType()} {ComponentFieldName} {{ get; set; }}
";
    }

    public string GetHandleValueField()
    {
        var propName = ComponentFieldName;

        return $@"case nameof({propName}):
                    if (!Equals({propName}, value))
                    {{
                        {propName} = ({GetAttachedPropertyType()})value;
                        //NativeControl.{AvaloniaFieldName} = {GetConvertedField(_fieldInfo.Type, propName)};
                    }}
                    break;
";

        string GetConvertedField(ITypeSymbol fieldType, string propName)
        {
            if (fieldType is INamedTypeSymbol namedType)
            {
                if (namedType.IsGenericType
                    && namedType.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IList_T
                    && namedType.TypeArguments[0].SpecialType == SpecialType.System_String)
                {
                    return $"AttributeHelper.GetStringList({propName})";
                }

                if (namedType.IsValueType && !namedType.IsNullableStruct())
                {
                    var hasBindingField = !_fieldInfo.ContainingType.GetMembers($"{propName}Field").IsEmpty;
                    var defaultValue = hasBindingField
                        ? $"({GetTypeNameAndAddNamespace(fieldType)}){AvaloniaContainingTypeName}.{propName}Field.GetDefaultValue({AvaloniaContainingTypeName}.{propName}Field.OwnerType)"
                        : "default";

                    return $"{propName} ?? {defaultValue}";
                }

                if (IsGeneric && namedType.GetFullName() == "Microsoft.Avalonia.Controls.BindingBase")
                {
                    return $"AttributeHelper.GetBinding({propName})";
                }

                if (IsGeneric && namedType.GetFullName() == "System.Collections.IList")
                {
                    return $"AttributeHelper.GetIList({propName})";
                }
            }

            return propName;
        }
    }

    internal static GeneratedFieldInfo[] GetValueProperties(GeneratedTypeInfo generatedType)
    {
        var componentInfo = generatedType.Settings;

        var props = GetMembers<IFieldSymbol>(componentInfo.TypeSymbol, generatedType.Settings.Include)
            .Where(p => !componentInfo.Exclude.Contains(p.Name))
            .Where(p => !componentInfo.ContentProperties.Contains(p.Name))
            .Where(IsPublicField)
            .Where(prop => IsExplicitlyAllowed(prop, generatedType) || !DisallowedComponentTypes.Contains(prop.Type.GetFullName()))
            .Where(prop => prop.Type.GetFullName() == "Avalonia.Media.Brush")
            .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase);

        return props.Select(prop =>
            {
                if (prop.Type.GetFullName() == "Avalonia.Media.Brush")
                {
                    var propName = prop.Name.Replace("Brush", "") + "Color";

                    if (prop.ContainingType.GetProperty(propName, includeBaseTypes: true) != null)
                    {
                        return null;
                    }

                    return new GeneratedFieldInfo(generatedType, prop, GeneratedFieldKind.AttachedProperty)
                    {
                        ComponentFieldName = propName,
                        //ComponentType = generatedType.GetTypeNameAndAddNamespace("Avalonia.Media", "Color")
                    };
                }
                else
                {
                    return new GeneratedFieldInfo(generatedType, prop, GeneratedFieldKind.AttachedProperty);
                }
            })
            .Where(p => p != null)
            .ToArray();
    }

    internal static GeneratedFieldInfo[] GetAttachedProperties(GeneratedTypeInfo generatedType)
    {
        if (generatedType.TypeName == "ToolTip")
        {

        }
        var componentInfo = generatedType.Settings;

        var props = GetMembers<IFieldSymbol>(componentInfo.TypeSymbol, generatedType.Settings.Include)
            .Where(p => !componentInfo.Exclude.Contains(p.Name))
            //.Where(p => !componentInfo.ContentProperties.Contains(p.Name))
            .Where(p => p.DeclaredAccessibility == Accessibility.Public)
            .Where(p => componentInfo.TypeSymbol.GetMethod("Set" + p.Name[..^8])?.DeclaredAccessibility == Accessibility.Public)
            .Where(prop => IsExplicitlyAllowed(prop, generatedType) || !DisallowedComponentTypes.Contains(prop.Type.GetFullName()))
            //.Where(prop => prop.Type.GetFullName() == "Avalonia.Media.Brush")
            .Where(prop => prop.Type.GetFullName().StartsWith("Avalonia.AttachedProperty"))
            .OrderBy(prop => prop.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        return props.Select(prop =>
        {

            if (prop.Type.GetFullName() == "Avalonia.Media.Brush")
            {
                var propName = prop.Name.Replace("Brush", "") + "Color";

                if (prop.ContainingType.GetField(propName, includeBaseTypes: true) != null)
                {
                    return null;
                }

                return new GeneratedFieldInfo(generatedType, prop, GeneratedFieldKind.AttachedProperty)
                {
                    ComponentFieldName = propName,
                    //ComponentType = generatedType.GetTypeNameAndAddNamespace("Avalonia.Media", "Color")
                };
            }
            else
            {
                var isContent = componentInfo.ContentProperties.Any(x => x == prop.Name);
                return new GeneratedFieldInfo(generatedType, prop, isContent ? GeneratedFieldKind.RenderFragment : GeneratedFieldKind.AttachedProperty);
            }
        })
            .Where(p => p != null)
            .ToArray();
    }


    private static string GetAttachedPropertyTypeName(IFieldSymbol fieldSymbol, GeneratedTypeInfo containingType, bool isRenderFragmentProperty = false, bool makeNullable = false)
    {
        var typeSymbol = ((INamedTypeSymbol)fieldSymbol.Type).TypeArguments[0];
        var isGeneric = containingType.Settings.GenericProperties.TryGetValue(fieldSymbol.Name, out var typeArgument);
        var typeArgumentName = typeArgument is null ? "T" : containingType.GetTypeNameAndAddNamespace(typeArgument);

        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return containingType.GetTypeNameAndAddNamespace(typeSymbol);
        }
        else if (namedTypeSymbol.IsGenericType
            && namedTypeSymbol.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IList_T
            && namedTypeSymbol.TypeArguments[0].SpecialType == SpecialType.System_String)
        {
            // Lists of strings are special-cased because they are handled specially by the handlers as a comma-separated list
            return "string";
        }
        else if (isRenderFragmentProperty)
        {
            return isGeneric ? $"RenderFragment<{typeArgumentName}>" : "RenderFragment";
        }
        else if (makeNullable && namedTypeSymbol.IsValueType && !namedTypeSymbol.IsNullableStruct())
        {
            return containingType.GetTypeNameAndAddNamespace(typeSymbol) + "?";
        }
        else if (namedTypeSymbol.SpecialType == SpecialType.System_Collections_IEnumerable && isGeneric)
        {
            return containingType.GetTypeNameAndAddNamespace("System.Collections.Generic", $"IEnumerable<{typeArgumentName}>");
        }
        else if (namedTypeSymbol.GetFullName() == "System.Collections.IList" && isGeneric)
        {
            return containingType.GetTypeNameAndAddNamespace("System.Collections.Generic", $"IList<{typeArgumentName}>");
        }
        else if (namedTypeSymbol.GetFullName() == "Avalonia.Data.BindingBase" && isGeneric)
        {
            return "Func<T, string>";
        }
        else if (namedTypeSymbol.SpecialType == SpecialType.System_Object && isGeneric)
        {
            return typeArgumentName;
        }
        else
        {
            return containingType.GetTypeNameAndAddNamespace(namedTypeSymbol);
        }
    }

    private static string GetOriginalAttachedPropertyTypeName(IFieldSymbol fieldSymbol, GeneratedTypeInfo containingType, bool isRenderFragmentProperty = false, bool makeNullable = false)
    {
        var typeSymbol = ((INamedTypeSymbol)fieldSymbol.Type).TypeArguments[0];
        var isGeneric = containingType.Settings.GenericProperties.TryGetValue(fieldSymbol.Name, out var typeArgument);
        var typeArgumentName = typeArgument is null ? "T" : containingType.GetTypeNameAndAddNamespace(typeArgument);

        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return containingType.GetTypeNameAndAddNamespace(typeSymbol);
        }
        else if (namedTypeSymbol.IsGenericType
            && namedTypeSymbol.ConstructedFrom.SpecialType == SpecialType.System_Collections_Generic_IList_T
            && namedTypeSymbol.TypeArguments[0].SpecialType == SpecialType.System_String)
        {
            // Lists of strings are special-cased because they are handled specially by the handlers as a comma-separated list
            return "string";
        }
        else if (makeNullable && namedTypeSymbol.IsValueType && !namedTypeSymbol.IsNullableStruct())
        {
            return containingType.GetTypeNameAndAddNamespace(typeSymbol) + "?";
        }
        else if (namedTypeSymbol.SpecialType == SpecialType.System_Collections_IEnumerable && isGeneric)
        {
            return containingType.GetTypeNameAndAddNamespace("System.Collections.Generic", $"IEnumerable<{typeArgumentName}>");
        }
        else if (namedTypeSymbol.GetFullName() == "System.Collections.IList" && isGeneric)
        {
            return containingType.GetTypeNameAndAddNamespace("System.Collections.Generic", $"IList<{typeArgumentName}>");
        }
        else if (namedTypeSymbol.SpecialType == SpecialType.System_Object && isGeneric)
        {
            return typeArgumentName;
        }
        else
        {
            return containingType.GetTypeNameAndAddNamespace(namedTypeSymbol);
        }
    }

    private static bool IsExplicitlyAllowed(IFieldSymbol fieldInfo, GeneratedTypeInfo generatedType)
    {
        return generatedType.Settings.Include.Contains(fieldInfo.Name)
            || fieldInfo.Type.SpecialType == SpecialType.System_Object && generatedType.Settings.GenericProperties.ContainsKey(fieldInfo.Name);
    }

    private static bool IsPublicField(IFieldSymbol fieldInfo)
    {
        return fieldInfo.DeclaredAccessibility == Accessibility.Public
            && IsBrowsable(fieldInfo)
            && !IsObsolete(fieldInfo);
    }

    private static bool IsBrowsable(ISymbol propInfo)
    {
        // [EditorBrowsable(EditorBrowsableState.Never)]
        return !propInfo.GetAttributes().Any(a => a.AttributeClass.Name == nameof(EditorBrowsableAttribute)
            && a.ConstructorArguments.FirstOrDefault().Value?.Equals((int)EditorBrowsableState.Never) == true);
    }

    private static bool IsObsolete(ISymbol symbol)
    {
        return symbol.GetAttributes().Any(a => a.AttributeClass.Name == nameof(ObsoleteAttribute));
    }

    private static readonly List<string> DisallowedComponentTypes = new()
    {
        //"Microsoft.Avalonia.Controls.Button.ButtonContentLayout", // TODO: This is temporary; should be possible to add support later
        //"Microsoft.Avalonia.Controls.ColumnDefinitionCollection",
        //"Microsoft.Avalonia.Controls.PointCollection",
        //"Microsoft.Avalonia.Controls.DoubleCollection",
        //"Microsoft.Avalonia.Controls.ControlTemplate",
        //"Microsoft.Avalonia.Controls.DataTemplate",
        //"Microsoft.Avalonia.Controls.Element",
        //"Microsoft.Avalonia.Font", // TODO: This is temporary; should be possible to add support later
        //"Microsoft.Avalonia.Graphics.Font", // TODO: This is temporary; should be possible to add support later
        //"Microsoft.Avalonia.Controls.FormattedString",
        //"Microsoft.Avalonia.Controls.Shapes.Geometry",
        //"Microsoft.Avalonia.Controls.GradientStopCollection",
        //"System.Windows.Input.ICommand",
        //"System.Object",
        //"Microsoft.Avalonia.Controls.Page",
        //"Microsoft.Avalonia.Controls.RowDefinitionCollection",
        //"Microsoft.Avalonia.Controls.Shadow",
        //"Microsoft.Avalonia.Controls.ShellContent",
        //"Microsoft.Avalonia.Controls.ShellItem",
        //"Microsoft.Avalonia.Controls.ShellSection",
        //"Microsoft.Avalonia.Controls.IVisual",
        //"Microsoft.Avalonia.Controls.View",
        //"Microsoft.Avalonia.Graphics.IShape",
        //"Microsoft.Avalonia.IView",
        //"Microsoft.Avalonia.IViewHandler"
    };

    private string GetTypeNameAndAddNamespace(ITypeSymbol type)
    {
        return ContainingType.GetTypeNameAndAddNamespace(type);
    }

    private string GetTypeNameAndAddNamespace(string @namespace, string typeName)
    {
        return ContainingType.GetTypeNameAndAddNamespace(@namespace, typeName);
    }

    private static IEnumerable<T> GetMembers<T>(ITypeSymbol typeSymbol, IEnumerable<string> includeBaseMemberNames) where T : ISymbol
    {
        var baseMembers = includeBaseMemberNames
            .Select(member => typeSymbol.GetMember(member, true))
            .Where(member => member != null);

        return typeSymbol.GetMembers().Union(baseMembers, SymbolEqualityComparer.Default).OfType<T>();
    }
}
