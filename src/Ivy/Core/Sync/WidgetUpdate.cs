using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Core.Sync
{
    [MessagePackObject]
    internal record WidgetUpdate
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
    internal record WidgetListDiff
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
    internal record WidgetListUpdate : IWidgetListOperation
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

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListAdd : IWidgetListOperation
    {
        public WidgetListAdd(int index, IWidget widget)
        {
            Index = index;
            Widget = widget;
        }

        [Key(0)]
        public int Index { get; init; }
        
        [Key(1)]
        public IWidget Widget { get; init; }

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListAddRange : IWidgetListOperation
    {
        public WidgetListAddRange(int index, IWidget[] widgets)
        {
            Index = index;
            Widgets = widgets;
        }

        [Key(0)]
        public int Index { get; init; }

        [Key(1)]
        public IWidget[] Widgets { get; init; }

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListReplace : IWidgetListOperation
    {
        public WidgetListReplace(int index, IWidget? widget)
        {
            Index = index;
            Widget = widget;
        }

        [Key(0)]
        public int Index { get; init; }

        [Key(1)]
        public IWidget? Widget { get; init; }

        public int SortIndex { get => Index; }
    }

    [MessagePackObject]
    internal record WidgetListReplaceRange : IWidgetListOperation
    {
        public WidgetListReplaceRange(int startIndex, int endIndex, IWidget[]? widgets)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            Widgets = widgets;
        }

        [Key(0)]
        public int StartIndex { get; init; }

        [Key(1)]
        public int EndIndex { get; init; }

        [Key(2)]
        public IWidget[]? Widgets { get; init; }

        public int SortIndex { get => StartIndex; }
    }

    [Union(0, typeof(WidgetListMove))]
    internal interface IWidgetListComplexOperation
    {

    }

    [MessagePackObject]
    internal record WidgetListMove : IWidgetListComplexOperation
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
