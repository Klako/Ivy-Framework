using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Ivy.Core.Sync
{
    [MessagePackFormatter(typeof(StructureMessagePackFormatter))]
    public interface IPropStructureNode
    {
        public bool DeepEquals(IPropStructureNode? otherNode);
    }

    public record PropStructureObject(IImmutableDictionary<string, IPropStructureNode> Members) : IPropStructureNode
    {
        public bool DeepEquals(IPropStructureNode? otherNode)
        {
            if (otherNode is PropStructureObject otherObj)
            {
                if (Members.Count != otherObj.Members.Count)
                {
                    return false;
                }
                foreach (var (key, value) in Members)
                {
                    if (!(otherObj.Members.TryGetValue(key, out var otherValue)
                          && value.DeepEquals(otherValue)))
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }
    }

    public record PropStructureList(ImmutableList<IPropStructureNode> Members) : IPropStructureNode
    {
        public bool DeepEquals(IPropStructureNode? otherNode)
        {
            if (otherNode is PropStructureList otherList)
            {
                return Members.SequenceEqual(otherList.Members, StructureNodeEqualityComparer.Instance);
            }
            else return false;
        }
    }

    public record PropStructureLeaf(object? Value) : IPropStructureNode
    {
        public bool DeepEquals(IPropStructureNode? otherNode)
        {
            if (otherNode is not PropStructureLeaf otherLeaf)
            {
                return false;
            }

            if (ReferenceEquals(Value, otherLeaf.Value))
            {
                return true;
            }

            if (Value is null || otherLeaf.Value is null)
            {
                return false;
            }

            if (Value.Equals(otherLeaf.Value))
            {
                return true;
            }

            if (Value.GetType().IsNumeric() && otherLeaf.Value.GetType().IsNumeric()
                && (dynamic)Value == (dynamic)otherLeaf.Value)
            {
                return true;
            }

            return false;
        }
    }

    public class StructureNodeEqualityComparer : IEqualityComparer<IPropStructureNode>
    {
        public static StructureNodeEqualityComparer Instance { get; } = new();

        private StructureNodeEqualityComparer() { }

        public bool Equals(IPropStructureNode? x, IPropStructureNode? y)
        {
            if (x == null)
            {
                return y == null;
            }

            return x.DeepEquals(y);
        }

        public int GetHashCode([DisallowNull] IPropStructureNode obj)
        {
            throw new NotImplementedException();
        }
    }

    public class StructureMessagePackFormatter : IMessagePackFormatter<IPropStructureNode>
    {
        public IPropStructureNode Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.NextMessagePackType == MessagePackType.Map)
            {
                var size = reader.ReadMapHeader();
                var builder = ImmutableDictionary.CreateBuilder<string, IPropStructureNode>();
                for (int i = 0; i < size; i++)
                {
                    var key = reader.ReadString();
                    if (key == null)
                    {
                        throw new MessagePackSerializationException("Expected string, got null");
                    }
                    var value = Deserialize(ref reader, options);
                    builder.Add(key, value);
                }
                return new PropStructureObject(builder.ToImmutable());
            }
            else if (reader.NextMessagePackType == MessagePackType.Array)
            {
                var size = reader.ReadArrayHeader();
                var builder = ImmutableList.CreateBuilder<IPropStructureNode>();
                for (int i = 0; i < size; i++)
                {
                    var element = Deserialize(ref reader, options);
                    builder.Add(element);
                }
                return new PropStructureList(builder.ToImmutable());
            }
            else
            {
                return new PropStructureLeaf(MessagePackSerializer.Deserialize<object?>(ref reader, options));
            }
        }

        public void Serialize(ref MessagePackWriter writer, IPropStructureNode value, MessagePackSerializerOptions options)
        {
            if (value is PropStructureLeaf leaf)
            {
                if (leaf.Value == null)
                {
                    writer.WriteNil();
                }
                else
                {
                    MessagePackSerializer.Serialize(leaf.Value.GetType(), ref writer, leaf.Value, options);
                }
            }
            else if (value is PropStructureObject obj)
            {
                writer.WriteMapHeader(obj.Members.Count);
                foreach (var (k, v) in obj.Members)
                {
                    writer.Write(k);
                    Serialize(ref writer, v, options);
                }
            }
            else if (value is PropStructureList list)
            {
                writer.WriteArrayHeader(list.Members.Count);
                foreach (var e in list.Members)
                {
                    Serialize(ref writer, e, options);
                }
            }
        }
    }
}
