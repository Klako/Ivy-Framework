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
            var metadata = WidgetMetadata.FromWidgetType(widget.GetType());

            writer.WriteArrayHeader(5);

            // Serialize basics

            writer.Write(metadata.TypeName);

            writer.Write(widget.Id);

            // Serialize props

            var props = new List<(string, object)>(metadata.PropMetadatas.Count);

            foreach (var (name, propMetadata) in metadata.PropMetadatas)
            {
                var value = propMetadata.GetValue(widget);

                // Skip properties that match their default values (unless AlwaysSerialize is set)
                if (!propMetadata.AlwaysSerialize && propMetadata.DefaultValue != null)
                {
                    if (ValuesAreEqual(value, propMetadata.DefaultValue))
                        continue;
                }
                if (value != null)
                {
                    props.Add((name, value));
                }
            }

            writer.WriteMapHeader(props.Count);
            foreach (var (propName, propValue) in props)
            {
                writer.Write(propName);
                MessagePackSerializer.Serialize(propValue.GetType(), ref writer, propValue, options);
            }

            // Serialize events

            var events = metadata.GetEvents(widget);

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
