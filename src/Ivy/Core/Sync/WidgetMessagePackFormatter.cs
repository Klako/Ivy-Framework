using MessagePack;
using MessagePack.Formatters;
using System.Collections;
using System.Text.Json;
using System.Text.Json.JsonDiffPatch;
using System.Text.Json.Nodes;


namespace Ivy.Core.Sync
{
    public class WidgetMessagePackFormatter : IMessagePackFormatter<IWidget>
    {
        public void Serialize(ref MessagePackWriter writer, IWidget widget, MessagePackSerializerOptions options)
        {
            var metadata = WidgetMetadata.FromWidgetType(widget.GetType());

            writer.WriteArrayHeader(5);

            // Serialize basics

            writer.Write(metadata.TypeName);

            writer.Write(widget.Id);

            // Serialize props

            var props = new List<(string, JsonNode?)>(metadata.PropMetadatas.Count);

            foreach (var (name, propMetadata) in metadata.PropMetadatas)
            {
                var value = propMetadata.GetValueAsJson(widget);

                // Skip properties that match their default values (unless AlwaysSerialize is set)
                if (!propMetadata.AlwaysSerialize && propMetadata.DefaultJsonValue != null)
                {
                    if (value.DeepEquals(propMetadata.DefaultJsonValue))
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
                MessagePackSerializer.Serialize(ref writer, propValue, options);
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
