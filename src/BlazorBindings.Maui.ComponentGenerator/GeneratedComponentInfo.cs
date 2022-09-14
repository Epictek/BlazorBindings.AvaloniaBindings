using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace ComponentWrapperGenerator
{
    public class GeneratedComponentInfo
    {
        public INamedTypeSymbol TypeSymbol { get; set; }
        public HashSet<string> Exclude { get; set; }
        public string[] PropertyChangedEvents { get; set; }
    }
}
