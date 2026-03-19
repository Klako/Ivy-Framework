using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ivy.Core.Sync
{
    internal class WidgetSerializer : IMessagePackFormatter<IWidget>
    {
        private static readonly ConcurrentDictionary<Type, SerializationTypeMetadata> MetadataCache = new();

        private sealed record PropInfo(PropertyInfo Property, PropAttribute Attribute, string CamelCaseName);

        private sealed record EventInfo(PropertyInfo Property);

        private sealed record SerializationTypeMetadata(
            string TypeName,
            PropInfo[] PropProperties,
            EventInfo[] EventProperties,
            IWidget? DefaultInstance
        );

        public static string CleanTypeName(Type t)
        {
            return t.Namespace + "." + Utils.CleanGenericNotation(t.Name);
        }
        private static SerializationTypeMetadata GetMetadata(Type type)
        {
            return MetadataCache.GetOrAdd(type, static t =>
            {
                var allProperties = t.GetProperties();

                var propProperties = allProperties
                    .Select(p => (Property: p, Attribute: p.GetCustomAttribute<PropAttribute>()))
                    .Where(x => x.Attribute != null)
                    .Select(x => new PropInfo(x.Property, x.Attribute!, Utils.PascalCaseToCamelCase(x.Property.Name)))
                    .ToArray();

                var eventProperties = allProperties
                    .Where(p => p.GetCustomAttribute<EventAttribute>() != null)
                    .Select(p => new EventInfo(p))
                    .ToArray();

                IWidget? defaultInstance = null;
                var defaultCtor = t.GetConstructor(
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

                var typeName = CleanTypeName(t);

                return new SerializationTypeMetadata(typeName, propProperties, eventProperties, defaultInstance);
            });
        }

        private static object? GetPropertyValue(IWidget widget, PropertyInfo property, PropAttribute attribute)
        {
            if (attribute.IsAttached)
            {
                if (!property.PropertyType.IsArray || !property.PropertyType.GetElementType()!.IsGenericType)
                    throw new InvalidOperationException("Attached properties must be arrays of nullable types.");

                var children = widget.Children;
                var attachedValues = new object?[children.Length];
                var widgetType = widget.GetType();
                var attachedName = attribute.AttachedName!;

                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] is IWidget childWidget)
                    {
                        attachedValues[i] = childWidget.GetAttachedValue(widgetType, attachedName);
                    }
                }
                return attachedValues;
            }

            return property.GetValue(widget);
        }

        private static bool ValuesAreEqual(object? a, object? b)
        {
            if (Equals(a, b)) return true;

            if (a is Array arrA && b is Array arrB)
            {
                if (arrA.Length != arrB.Length) return false;
                for (int i = 0; i < arrA.Length; i++)
                {
                    if (!ValuesAreEqual(arrA.GetValue(i), arrB.GetValue(i)))
                        return false;
                }
                return true;
            }

            return false;
        }

        public void Serialize(ref MessagePackWriter writer, IWidget widget, MessagePackSerializerOptions options)
        {
            var metadata = GetMetadata(widget.GetType());

            writer.WriteArrayHeader(5);

            // Serialize basics

            writer.Write(metadata.TypeName);

            writer.Write(widget.Id);

            // Serialize props

            var props = new List<(PropInfo, object)>(metadata.PropProperties.Length);

            foreach (var propInfo in metadata.PropProperties)
            {
                var value = GetPropertyValue(widget, propInfo.Property, propInfo.Attribute);

                // Skip properties that match their default values (unless AlwaysSerialize is set)
                if (!propInfo.Attribute.AlwaysSerialize && metadata.DefaultInstance != null)
                {
                    var defaultValue = GetPropertyValue(metadata.DefaultInstance, propInfo.Property, propInfo.Attribute);
                    if (ValuesAreEqual(value, defaultValue))
                        continue;
                }
                if (value != null)
                {
                    props.Add((propInfo, value));
                }
            }

            writer.WriteMapHeader(props.Count);
            foreach (var (propInfo, propValue) in props)
            {
                writer.Write(propInfo.CamelCaseName);
                MessagePackSerializer.Serialize(propValue.GetType(), ref writer, propValue, options);
            }

            // Serialize events

            var events = new List<string>(metadata.EventProperties.Length);

            foreach (var eventInfo in metadata.EventProperties)
            {
                var value = eventInfo.Property.GetValue(widget);
                if (eventInfo.Property.GetValue(widget) != null)
                {
                    events.Add(eventInfo.Property.Name);
                }
            }

            writer.WriteArrayHeader(events.Count);
            foreach (var e in events)
            {
                writer.Write(e);
            }

            // Serialize children

            foreach (var child in widget.Children)
            {
                if (child is not IWidget)
                    throw new InvalidOperationException("Only widgets can be serialized.");
            }

            writer.WriteArrayHeader(widget.Children.Length);
            foreach (IWidget child in widget.Children)
            {
                Serialize(ref writer, child, options);
            }

        }

        public IWidget Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
