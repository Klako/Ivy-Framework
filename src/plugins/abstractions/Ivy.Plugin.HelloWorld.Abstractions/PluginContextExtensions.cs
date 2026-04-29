using Ivy.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Plugin.HelloWorld;

public static class PluginContextExtensions
{
    public static void RegisterGreeter(this IPluginContext context, IGreeter greeter)
    {
        context.Services.AddSingleton<IGreeter>(greeter);
    }
}
