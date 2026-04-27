using Ivy.Plugins;

namespace Ivy.Plugin.HelloWorld;

public interface IHelloWorldPluginContext : IPluginContext
{
    void RegisterGreeter(IGreeter greeter);
}
