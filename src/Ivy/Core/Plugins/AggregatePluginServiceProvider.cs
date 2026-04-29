using Ivy.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Core.Plugins;

internal class AggregatePluginServiceProvider : IPluginServiceProvider
{
    private readonly Dictionary<string, IServiceProvider> _providers = new();
    private readonly ReaderWriterLockSlim _lock = new();

    public void AddProvider(string pluginId, IServiceProvider provider)
    {
        _lock.EnterWriteLock();
        try
        {
            _providers[pluginId] = provider;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void RemoveProvider(string pluginId)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_providers.TryGetValue(pluginId, out var provider))
            {
                _providers.Remove(pluginId);
                (provider as IDisposable)?.Dispose();
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public T? GetService<T>() where T : class
    {
        _lock.EnterReadLock();
        try
        {
            foreach (var provider in _providers.Values)
            {
                var service = provider.GetService<T>();
                if (service is not null) return service;
            }
            return null;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public IEnumerable<T> GetServices<T>() where T : class
    {
        _lock.EnterReadLock();
        try
        {
            return _providers.Values
                .SelectMany(p => p.GetServices<T>())
                .ToList();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public IReadOnlyCollection<string> LoadedPluginIds
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _providers.Keys.ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}
