namespace Ivy.Plugins;

[AttributeUsage(AttributeTargets.Assembly)]
public class IvyPluginAttribute(Type pluginType) : Attribute
{
    public Type PluginType { get; } = pluginType;
}
