using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Text.Json.Nodes;

namespace Ivy.Core.Sync
{
    public enum PropUpdateType
    {
        ObjectDiff = 0,
        ArrayDiff = 1,
        ValueDiff = 2
    }
    [MessagePackFormatter(typeof(PropUpdateMessagePackFormatter))]
    public interface IPropUpdate;
    public record PropObjectDiff(IDictionary<string, IPropObjectOperation> Changes) : IPropUpdate;
    public record PropArrayDiff(
        IEnumerable<(int, IPropUpdate)> Changes,
        IEnumerable<JsonNode?> Appends,
        int Removals) : IPropUpdate;
    public record PropValueDiff(JsonNode? NewValue) : IPropUpdate;
    public enum PropObjectOperationType
    {
        Update = 0,
        Set = 1,
        Remove = 2
    }
    public interface IPropObjectOperation;
    public record PropObjectUpdate(IPropUpdate Update) : IPropObjectOperation;
    public record PropObjectSet(JsonNode? NewValue) : IPropObjectOperation;
    public record PropObjectRemove() : IPropObjectOperation;

    public class PropUpdateMessagePackFormatter : IMessagePackFormatter<IPropUpdate>
    {
        public static PropUpdateMessagePackFormatter Instance { get; } = new();

        public IPropUpdate Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref MessagePackWriter writer, IPropUpdate value, MessagePackSerializerOptions options)
        {
            if (value is PropObjectDiff objectDiff)
            {
                writer.WriteArrayHeader(2);
                writer.Write((byte)PropUpdateType.ObjectDiff);
                writer.WriteArrayHeader(objectDiff.Changes.Count);
                foreach (var (key, change) in objectDiff.Changes)
                {
                    if (change is PropObjectUpdate update)
                    {
                        writer.WriteArrayHeader(3);
                        writer.Write(key);
                        writer.Write((byte)PropObjectOperationType.Update);
                        Serialize(ref writer, update.Update, options);
                    }
                    else if (change is PropObjectSet set)
                    {
                        writer.WriteArrayHeader(3);
                        writer.Write(key);
                        writer.Write((byte)PropObjectOperationType.Set);
                        MessagePackSerializer.Serialize(ref writer, set.NewValue, options);
                    }
                    else if (change is PropObjectRemove)
                    {
                        writer.WriteArrayHeader(2);
                        writer.Write(key);
                        writer.Write((byte)PropObjectOperationType.Remove);
                    }
                    else writer.WriteNil();
                }
            }
            else if (value is PropArrayDiff arrayDiff)
            {
                writer.WriteArrayHeader(4);
                writer.Write((byte)PropUpdateType.ArrayDiff);
                writer.WriteArrayHeader(arrayDiff.Changes.Count());
                foreach (var (index, change) in arrayDiff.Changes)
                {
                    writer.WriteArrayHeader(2);
                    writer.Write(index);
                    Serialize(ref writer, change, options);
                }
                writer.WriteArrayHeader(arrayDiff.Appends.Count());
                foreach (var jsonNode in arrayDiff.Appends)
                {
                    MessagePackSerializer.Serialize(ref writer, jsonNode, options);
                }
                writer.Write(arrayDiff.Removals);
            }
            else if (value is PropValueDiff valueDiff)
            {
                writer.WriteArrayHeader(2);
                writer.Write((byte)PropUpdateType.ValueDiff);
                MessagePackSerializer.Serialize(ref writer, valueDiff.NewValue, options);
            }
            else writer.WriteNil();
        }
    }
}
