using YamlDotNet.Serialization;

namespace Ivy.Filters;

/// <summary>
/// YAML static context for AOT-compatible serialization
/// </summary>
[YamlStaticContext]
[YamlSerializable(typeof(List<FieldMetaYaml>))]
[YamlSerializable(typeof(FieldMetaYaml))]
public partial class FilterYamlContext : StaticContext
{
}

/// <summary>
/// YAML-serializable representation of field metadata
/// </summary>
public class FieldMetaYaml
{
    public string DisplayName { get; set; } = string.Empty;
    public string ColId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
