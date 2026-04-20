using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Hashing;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace Ivy.Core.Sync
{
    [MessagePackFormatter(typeof(WidgetNodeMessagePackFormatter))]
    public class WidgetNode
    {
        internal WidgetMetadata Metadata { get; }

        public IWidget Widget { get; }

        public Type Type { get => Widget.GetType(); }

        public string Id { get; }

        public (string Name, IPropStructureNode Value)[] Props { get; }

        public IEnumerable<string> Events { get; }

        public WidgetNode[] Children { get; }

        public WidgetNode(IWidget widget)
        {
            Widget = widget;
            Metadata = WidgetMetadata.FromWidgetType(Type);
            Id = widget.Id!;
            Props = Metadata.PropMetadatas
                .Select(propMetadata => (propMetadata.CamelCaseName, propMetadata.GetValueAsStructure(widget)))
                .ToArray();
            Events = Metadata.GetEvents(widget);
            Children = widget.Children
                .Select(widget => new WidgetNode((IWidget)widget))
                .ToArray();
        }
    }

    public class WidgetNodeMessagePackFormatter : IMessagePackFormatter<WidgetNode>
    {
        private static StructureMessagePackFormatter propFormatter = new();

        public WidgetNode Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref MessagePackWriter writer, WidgetNode value, MessagePackSerializerOptions options)
        {
            writer.WriteArrayHeader(5);

            writer.Write(value.Metadata.TypeName);
            writer.Write(value.Id);

            var props = value.Props
                .Zip(value.Metadata.PropMetadatas)
                .Where(e =>
                {
                    var ((_, propValue), propMetadata) = e;
                    if (!propMetadata.AlwaysSerialize && propMetadata.DefaultValue != null)
                    {
                        if (propValue.DeepEquals(propMetadata.DefaultStructureValue))
                            return false;
                    }
                    if (propValue.DeepEquals(new PropStructureLeaf(null)))
                    {
                        return false;
                    }
                    return true;
                })
                .ToArray();

            writer.WriteMapHeader(props.Length);
            foreach(var ((propName, propValue), _) in props)
            {
                writer.Write(propName);
                propFormatter.Serialize(ref writer, propValue, options);
            }

            writer.WriteArrayHeader(value.Events.Count());
            foreach (var e in value.Events)
            {
                writer.Write(e);
            }

            writer.WriteArrayHeader(value.Children.Length);
            foreach (var child in value.Children)
            {
                Serialize(ref writer, child, options);
            }
        }
    }

    public static class WidgetExtensions
    {
        public static WidgetNode ToWidgetNode(this IWidget widget)
        {
            return new WidgetNode(widget);
        }
    }
}
