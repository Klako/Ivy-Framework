using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Core.Sync
{
    [MessagePackObject]
    public record WidgetUpdate
    {
        public WidgetUpdate(
            Type? type = null,
            string? id = null,
            IDictionary<string, object?>? props = null,
            string[]? events = null,
            WidgetListDiff? children = null)
        {
            Type = type;
            Id = id;
            Props = props;
            Events = events;
            Children = children;
        }

        [Key(0)]
        [MessagePackFormatter(typeof(WidgetTypeFormatter))]
        public Type? Type { get; init; }

        [Key(1)]
        public string? Id { get; init; }

        [Key(2)]
        public IDictionary<string, object?>? Props { get; init; }

        [Key(3)]
        public string[]? Events { get; init; }

        [Key(4)]
        public WidgetListDiff? Children { get; init; }

        public class WidgetTypeFormatter : IMessagePackFormatter<Type?>
        {
            public Type Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public void Serialize(ref MessagePackWriter writer, Type? value, MessagePackSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNil();
                } else
                {
                    writer.Write(CleanTypeName(value));
                }
            }

            public static string CleanTypeName(Type t)
            {
                return t.Namespace + "." + Utils.CleanGenericNotation(t.Name);
            }
        }
    }

    [MessagePackObject]
    public record WidgetListDiff
    {
        public WidgetListDiff(
            IWidgetListComplexOperation[]? complexChanges = null,
            IWidgetListOperation[]? changes = null)
        {
            ComplexChanges = complexChanges;
            Changes = changes;
        }

        [Key(0)]
        public IWidgetListComplexOperation[]? ComplexChanges { get; init; }

        [Key(1)]
        public IWidgetListOperation[]? Changes { get; init; }
    }

    [Union(0, typeof(WidgetListUpdate))]
    [Union(1, typeof(WidgetListSplice))]
    public interface IWidgetListOperation
    {
        [IgnoreMember()]
        public int SortIndex { get; }
    }

    [MessagePackObject]
    public record WidgetListUpdate : IWidgetListOperation
    {
        public WidgetListUpdate(int index, WidgetUpdate update)
        {
            Index = index;
            Update = update;
        }

        [Key(0)]
        public int Index { get; init; }

        [Key(1)]
        public WidgetUpdate Update { get; init; }

        [IgnoreMember()]
        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    public record WidgetListSplice : IWidgetListOperation
    {
        public WidgetListSplice(int index, int length, IEnumerable<IWidget> widgets)
        {
            Index = index;
            Length = length;
            Widgets = widgets;
        }

        [Key(0)]
        public int Index { get; init; }

        [Key(1)]
        public int Length { get; init; }

        [Key(2)]
        public IEnumerable<IWidget> Widgets { get; init; }

        [IgnoreMember]
        public int SortIndex => Index;

        public static WidgetListSplice Add(int index, IWidget widget) => new(index, 0, [widget]);
        public static WidgetListSplice AddRange(int index, IEnumerable<IWidget> widgets) => new(index, 0, widgets);
        public static WidgetListSplice Remove(int index) => new(index, 1, []);
        public static WidgetListSplice RemoveRange(int index, int length) => new(index, length, []);
        public static WidgetListSplice Replace(int index, IWidget widget) => new(index, 1, [widget]);
        public static WidgetListSplice ReplaceRange(int index, int length, IEnumerable<IWidget> widgets) => new(index, length, widgets);
    }

    [Union(0, typeof(WidgetListMove))]
    public interface IWidgetListComplexOperation
    {

    }

    [MessagePackObject]
    public record WidgetListMove : IWidgetListComplexOperation
    {
        public WidgetListMove(int fromIndex, int toIndex)
        {
            FromIndex = fromIndex;
            ToIndex = toIndex;
        }

        [Key(0)]
        public int FromIndex { get; init; }

        [Key(1)]
        public int ToIndex { get; init; }
    }
}
