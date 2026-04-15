using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Ivy.Core.Sync
{
    internal class StructureFactory
    {
        public IPropStructureNode Transform(object? value)
        {
            if (value == null)
            {
                return new PropStructureLeaf(null);
            }

            var type = value.GetType();

            if (type.GetCustomAttribute<PropValueAsStringAttribute>() != null)
            {
                return new PropStructureLeaf(value.ToString());
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
                return new PropStructureLeaf(value);
            }

            if (value is IDictionary dict)
            {
                var map = new PropStructureObject();
                foreach (DictionaryEntry entry in dict)
                {
                    map.Add(entry.Key.ToString()!, Transform(entry.Value));
                }
                return map;
            }

            if (value is IEnumerable enumerable)
            {
                var list = new PropStructureList();
                foreach (var element in enumerable)
                {
                    list.Add(Transform(element));
                }
                return list;
            }

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (properties.Length > 0)
            {
                var map = new PropStructureObject();
                foreach (var property in properties)
                {
                    if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                    {
                        continue;
                    }
                    map.Add(Utils.PascalCaseToCamelCase(property.Name), Transform(property.GetValue(value)));
                }
                return map;
            }

            throw new ArgumentException($"Value of type {type} does not match any type pattern");
        }
    }
}
