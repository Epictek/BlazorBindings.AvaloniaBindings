using System;

namespace BlazorBindings.Avalonia.ComponentGenerator;

[Flags]
public enum AttachedPropertyGeneration
{
    ExtensionMethods = 1,
    Elements = 2
}
