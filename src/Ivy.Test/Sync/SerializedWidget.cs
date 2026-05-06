using Ivy.Core;
using Ivy.Core.Server.Formatters;
using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;
using System.Collections.Immutable;
using System.Text.Json.JsonDiffPatch;
using System.Text.Json.Nodes;

namespace Ivy.Test.Sync
{
    [MessagePackObject]
    public record SerializedWidget
    {
        public SerializedWidget(
            string type,
            string id,
            IImmutableDictionary<string, IPropStructureNode>? props = null,
            string[]? events = null,
            IImmutableList<SerializedWidget>? children = null)
        {
            Type = type;
            Id = id;
            Props = props ?? ImmutableDictionary<string, IPropStructureNode>.Empty;
            Events = events ?? [];
            Children = children ?? ImmutableArray<SerializedWidget>.Empty;
        }

        [Key(0)]
        public string Type { get; init; }

        [Key(1)]
        public string Id { get; init; }

        [Key(2)]
        public IImmutableDictionary<string, IPropStructureNode> Props { get; init; }

        [Key(3)]
        public string[] Events { get; init; }

        [Key(4)]
        public IImmutableList<SerializedWidget> Children { get; init; }

        public static void AssertEqual(SerializedWidget expected, SerializedWidget actual)
        {
            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Id, actual.Id);
            foreach (var entry in expected.Props)
            {
                Assert.Contains(entry.Key, actual.Props);
                var actualValue = actual.Props[entry.Key];
                Assert.Equal(entry.Value, actualValue, StructureNodeEqualityComparer.Instance);
            }
            foreach (var entry in actual.Props)
            {
                Assert.Contains(entry.Key, expected.Props);
            }
            Assert.Equivalent(expected.Events, actual.Events);
            Assert.Equal(expected.Children.Count, actual.Children.Count);
            foreach (var (expectedChild, actualChild) in expected.Children.Zip(actual.Children))
            {
                AssertEqual(expectedChild, actualChild);
            }
        }

        public SerializedWidget ApplyDiff(WidgetUpdate update)
        {
            var widget = this;

            if (update.Type != null)
            {
                widget = widget with { Type = CleanTypeName(update.Type) };
            }

            if (update.Id != null)
            {
                widget = widget with { Id = update.Id };
            }

            if (update.Props != null)
            {
                foreach (var (name, propUpdate) in update.Props)
                {
                    if (propUpdate is null)
                    {
                        widget = widget with { Props = widget.Props.Remove(name) };
                    }
                    else
                    {
                        if (!widget.Props.TryGetValue(name, out var oldNode))
                        {
                            oldNode = new PropStructureLeaf(null);
                        }
                        var newNode = ApplyPropDiff(oldNode, propUpdate);
                        widget = widget with { Props = widget.Props.SetItem(name, newNode) };
                    }
                }
            }

            if (update.Events != null)
            {
                widget = widget with { Events = update.Events };
            }

            if (update.Children != null)
            {
                if (update.Children.Changes != null)
                {
                    var children = widget.Children;

                    // Assume changes are sorted by SortIndex.
                    // Reversing ensures that unaffected children
                    // keep the same index after each change.
                    foreach (var change in update.Children.Changes.Reverse())
                    {
                        if (change is WidgetListUpdate nestedUpdate)
                        {
                            var newWidget = children[nestedUpdate.Index].ApplyDiff(nestedUpdate.Update);
                            children = children.SetItem(nestedUpdate.Index, newWidget);
                        }
                        else if (change is WidgetListSplice splice)
                        {
                            var convertedWidgets = splice.Widgets.Select(SerializedWidget.FromWidgetNode);
                            children = children.RemoveRange(splice.Index, splice.Length);
                            children = children.InsertRange(splice.Index, convertedWidgets);
                        }
                    }
                    widget = widget with { Children = children };
                }
            }

            return widget;
        }

        private static IPropStructureNode ApplyPropDiff(IPropStructureNode source, IPropUpdate? update)
        {
            if (update is PropValueDiff valueDiff)
            {
                var serializedNewValue = MessagePackSerializer.Serialize(valueDiff.NewValue, SerializeOptions);
                var deserializedNewValue = MessagePackSerializer.Deserialize<IPropStructureNode>(serializedNewValue, SerializeOptions);
                return deserializedNewValue;
            }
            else if (update is PropObjectDiff objectDiff)
            {
                Assert.NotNull(source);
                Assert.IsType<PropStructureObject>(source);
                var members = ((PropStructureObject)source).Members;
                foreach (var (key, change) in objectDiff.Changes)
                {
                    if (change is PropObjectUpdate fieldUpdate)
                    {
                        members = members.SetItem(key, ApplyPropDiff(members[key], fieldUpdate.Update));
                    }
                    else if (change is PropObjectSet fieldSet)
                    {
                        members = members.SetItem(key, fieldSet.NewValue);
                    }
                    else if (change is PropObjectRemove fieldRemove)
                    {
                        members = members.Remove(key);
                    }
                }
                return new PropStructureObject(members);
            }
            else if (update is PropArrayDiff arrayDiff)
            {
                Assert.NotNull(source);
                Assert.IsType<PropStructureList>(source);
                var members = ((PropStructureList)source).Members;
                foreach (var (index, change) in arrayDiff.Changes)
                {
                    members = members.SetItem(index, ApplyPropDiff(members[index], change));
                }
                if (arrayDiff.Removals > 0)
                {
                    var fromIndex = members.Count - arrayDiff.Removals;
                    members = members.RemoveRange(fromIndex, arrayDiff.Removals);
                }
                foreach (var node in arrayDiff.Appends)
                {
                    members = members.Add(node);
                }
                return new PropStructureList(members);
            }
            return source;
        }

        private static string CleanTypeName(Type t)
        {
            return t.Namespace + "." + Utils.CleanGenericNotation(t.Name);
        }

        internal static MessagePackSerializerOptions SerializeOptions { get; } =
            new MessagePackSerializerOptions(
               CompositeResolver.Create([
                        new JsonNodeMessagePackFormatter(),
                        new JsonObjectMessagePackFormatter(),
                        new JsonArrayMessagePackFormatter(),
                        new JsonValueMessagePackFormatter(),
                        new StructureMessagePackFormatter(),
                        new WidgetNodeMessagePackFormatter()
                    ],
                    [
                        JsonNodeResolver.Instance,
                        DynamicEnumAsStringResolver.Instance,
                        ContractlessStandardResolver.Instance
                    ]
                ));

        internal static MessagePackSerializerOptions DeserializeOptions { get; } =
            new MessagePackSerializerOptions(StandardResolver.Instance);

        public static SerializedWidget FromWidgetNode(WidgetNode widget)
        {
            var data = MessagePackSerializer.Serialize(widget, SerializeOptions);
            return MessagePackSerializer.Deserialize<SerializedWidget>(data, DeserializeOptions);
        }
    }
}
