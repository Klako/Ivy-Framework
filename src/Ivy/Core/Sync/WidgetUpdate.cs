using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Core.Sync
{
    [MessagePackObject]
    internal record WidgetUpdate(
        Type? type = null,
        string? id = null,
        Dictionary<string, object>? props = null,
        string[]? events = null,
        IWidgetListDiff? children = null)
    {
        [Key(0)]
        [MessagePackFormatter(typeof(WidgetTypeFormatter))]
        public Type? Type { get; init; } = type;

        [Key(1)]
        public string? Id { get; init; } = id;

        [Key(2)]
        public Dictionary<string, object>? Props { get; init; } = props;

        [Key(3)]
        public string[]? Events { get; init; } = events;

        [Key(4)]
        public IWidgetListDiff? Children { get; init; } = children;

        private class WidgetTypeFormatter : IMessagePackFormatter<Type>
        {
            public Type Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public void Serialize(ref MessagePackWriter writer, Type value, MessagePackSerializerOptions options)
            {
                writer.Write(CleanTypeName(value));
            }

            public static string CleanTypeName(Type t)
            {
                return t.Namespace + "." + Utils.CleanGenericNotation(t.Name);
            }
        }
    }

    [MessagePackObject]
    internal record IWidgetListDiff(
        IWidgetListComplexOperation[]? complexOperations = null,
        IWidgetListOperation[]? operations = null)
    {
        [Key(0)]
        public IWidgetListComplexOperation[]? ComplexOperations { get; init; } = complexOperations;

        [Key(1)]
        public IWidgetListOperation[]? Operations { get; init; } = operations;
    }

    [Union(0, typeof(WidgetListUpdate))]
    [Union(1, typeof(WidgetListAdd))]
    [Union(2, typeof(WidgetListAddRange))]
    [Union(3, typeof(WidgetListReplace))]
    [Union(4, typeof(WidgetListReplaceRange))]
    internal interface IWidgetListOperation
    {
        [IgnoreMember()]
        public int SortIndex { get; }
    }

    [MessagePackObject]
    internal record WidgetListUpdate(int index, WidgetUpdate update) : IWidgetListOperation
    {
        [Key(0)]
        public int Index { get; init; } = index;

        [Key(1)]
        public WidgetUpdate Widget { get; init; } = update;

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListAdd(int index, IWidget widget) : IWidgetListOperation
    {
        [Key(0)]
        public int Index { get; init; } = index;
        
        [Key(1)]
        public IWidget Widget { get; init; } = widget;

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListAddRange(int index, IWidget[] widgets) : IWidgetListOperation
    {
        [Key(0)]
        public int Index { get; init; } = index;

        [Key(1)]
        public IWidget[] Widgets { get; init; } = widgets;

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListReplace(int index, IWidget? widget) : IWidgetListOperation
    {
        [Key(0)]
        public int Index { get; init; } = index;

        [Key(1)]
        public IWidget? Widget { get; init; } = widget;

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListReplaceRange(int startIndex, int endIndex, IWidget[]? widgets) : IWidgetListOperation
    {
        [Key(0)]
        public int StartIndex { get; init; } = startIndex;

        [Key(1)]
        public int EndIndex { get; init; } = endIndex;

        [Key(2)]
        public IWidget[]? widgets { get; init; } = widgets;

        public int SortIndex { get => StartIndex; }
    }

    [Union(0, typeof(WidgetListMove))]
    internal interface IWidgetListComplexOperation
    {

    }

    [MessagePackObject]
    internal record WidgetListMove(int fromIndex, int toIndex) : IWidgetListComplexOperation
    {
        [Key(0)]
        public int FromIndex { get; init; } = fromIndex;

        [Key(1)]
        public int ToIndex { get; init; } = toIndex;
    }
}
