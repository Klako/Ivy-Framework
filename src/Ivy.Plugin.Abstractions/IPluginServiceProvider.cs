namespace Ivy.Plugins;

public interface IPluginServiceProvider
{
    T? GetService<T>() where T : class;
    IEnumerable<T> GetServices<T>() where T : class;
}
