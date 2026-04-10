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
            IImmutableDictionary<string, JsonNode?>? props = null,
            string[]? events = null,
            IImmutableList<SerializedWidget>? children = null)
        {
            Type = type;
            Id = id;
            Props = props ?? ImmutableDictionary<string, JsonNode?>.Empty;
            Events = events ?? [];
            Children = children ?? ImmutableArray<SerializedWidget>.Empty;
        }

        [Key(0)]
        public string Type { get; init; }

        [Key(1)]
        public string Id { get; init; }

        [Key(2)]
        public IImmutableDictionary<string, JsonNode?> Props { get; init; }

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
                Assert.True(JsonNode.DeepEquals(entry.Value, actualValue), $"Expected {entry.Value}\nActual {actualValue}");
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

        internal static MessagePackSerializerOptions SerializeOptions { get; } =
            new MessagePackSerializerOptions(
               CompositeResolver.Create([
                        new JsonNodeMessagePackFormatter(),
                        new JsonObjectMessagePackFormatter(),
                        new JsonArrayMessagePackFormatter(),
                        new JsonValueMessagePackFormatter(),
                        new WidgetMessagePackFormatter()
                    ],
                    [
                        JsonNodeResolver.Instance,
                        WidgetMessagePackResolver.Instance,
                        StandardResolver.Instance
                    ]
                ));

        internal static MessagePackSerializerOptions DeserializeOptions { get; } =
            new MessagePackSerializerOptions(
                CompositeResolver.Create(
                    JsonNodeResolver.Instance,
                    StandardResolver.Instance));

        public static SerializedWidget FromWidget(IWidget widget)
        {
            var data = MessagePackSerializer.Serialize(widget, SerializeOptions);
            return MessagePackSerializer.Deserialize<SerializedWidget>(data, DeserializeOptions);
        }
    }
}
