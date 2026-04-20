using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;



namespace Ivy.Core.Sync
{

    internal class StructureFactory
    {


        private static ConcurrentDictionary<Type, Func<object, IPropStructureNode>> cache = new();

        public static IPropStructureNode Transform(object? value)
        {
            if (value == null)
            {
                return new PropStructureLeaf(null);
            }

            var transformer = cache.GetOrAdd(value.GetType(), static type =>
            {
                if (type.GetCustomAttribute<PropValueAsStringAttribute>() != null)
                {
                    return static value => new PropStructureLeaf(value.ToString());
                }

                if (type.IsPrimitive ||
                    type.IsEnum ||
                    type.IsValueType ||
                    type == typeof(string) ||
                    type == typeof(decimal) ||
                    type == typeof(Guid) ||
                    type == typeof(DateTime) ||
                    type == typeof(DateTimeOffset) ||
                    type == typeof(TimeSpan) ||
                    type == typeof(Uri))
                {
                    return static value => new PropStructureLeaf(value);
                }

                if (type.IsAssignableTo(typeof(IDictionary)))
                {
                    return static value =>
                    {
                        var obj = new PropStructureObject();
                        foreach (DictionaryEntry entry in (IDictionary)value)
                        {
                            obj.Add(entry.Key.ToString()!, Transform(entry.Value));
                        }
                        return obj;
                    };
                }

                if (type.IsAssignableTo(typeof(IEnumerable)))
                {
                    return static value =>
                    {
                        var list = new PropStructureList();
                        foreach (var element in (IEnumerable)value)
                        {
                            list.Add(Transform(element));
                        }
                        return list;
                    };
                }

                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (properties.Length > 0)
                {
                    return value =>
                    {
                        var obj = new PropStructureObject();
                        foreach (var property in properties)
                        {
                            if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                            {
                                continue;
                            }
                            obj.Add(Utils.PascalCaseToCamelCase(property.Name), Transform(property.GetValue(value)));
                        }
                        return obj;
                    };
                }

                throw new ArgumentException($"Value of type {type} does not match any type pattern");
            });

            return transformer(value);
        }
    }
}
