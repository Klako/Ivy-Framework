using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ivy.Core.Sync
{
    internal record WidgetMetadata
    {

        private PropertyInfo[] eventProperties;
        
        public string TypeName { get; }       

        public IDictionary<string, PropMetadata> PropMetadatas { get; }

        private WidgetMetadata(Type widgetType)
        {
            TypeName = CleanTypeName(widgetType);

            var allProperties = widgetType.GetProperties();

            IWidget? defaultInstance = null;
            var defaultCtor = widgetType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                Type.EmptyTypes,
                null);

            if (defaultCtor != null)
            {
                try
                {
                    defaultInstance = defaultCtor.Invoke(null) as IWidget;
                } catch
                {
                    // Ignore construction failures - we'll just not have default values
                }
            }

            PropMetadatas = allProperties
                .Select(p => (Property: p, Attribute: p.GetCustomAttribute<PropAttribute>()))
                .Where(x => x.Attribute != null)
                .Select(x => {
                    var defaultValue = defaultInstance != null ? x.Property.GetValue(defaultInstance) : null;
                    return new PropMetadata(x.Property, x.Attribute!, defaultValue);
                    })
                .ToDictionary(x => x.CamelCaseName);

            eventProperties = allProperties
                .Where(p => p.GetCustomAttribute<EventAttribute>() != null)
                .ToArray();
        }

        public static string CleanTypeName(Type t)
        {
            return t.Namespace + "." + Utils.CleanGenericNotation(t.Name);
        }

        public IList<string> GetEvents(IWidget widget)
        {
            var events = new List<string>(eventProperties.Length);
            foreach (var info in eventProperties)
            {
                if (info.GetValue(widget) != null)
                {
                    events.Add(info.Name);
                }
            }
            return events;
        }

        private static ConcurrentDictionary<Type, WidgetMetadata> cache = new();

        public static WidgetMetadata FromWidgetType(Type type)
        {
            return cache.GetOrAdd(type, t => new WidgetMetadata(t));
        }
    }
}
