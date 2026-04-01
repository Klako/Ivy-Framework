using Ivy.Core;
using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Ivy.Test.Sync
{
    [MessagePackObject]
    public record SerializedWidget
    {
        public SerializedWidget(
            string type,
            string id,
            IImmutableDictionary<string, object>? props = null,
            string[]? events = null,
            IImmutableList<SerializedWidget>? children = null)
        {
            Type = type;
            Id = id;
            Props = props ?? ImmutableDictionary<string, object>.Empty;
            Events = events ?? [];
            Children = children ?? ImmutableArray<SerializedWidget>.Empty;
        }

        [Key(0)]
        public string Type { get; init; }

        [Key(1)]
        public string Id { get; init; }

        [Key(2)]
        public IImmutableDictionary<string, object> Props { get; init; }

        [Key(3)]
        public string[] Events { get; init; }

        [Key(4)]
        public IImmutableList<SerializedWidget> Children { get; init; }

        private static MessagePackSerializerOptions _serializerOptions =
            new MessagePackSerializerOptions(CompositeResolver.Create([new Core.Sync.WidgetSerializer()], [StandardResolver.Instance]));

        public static SerializedWidget FromWidget(IWidget widget)
        {
            var data = MessagePackSerializer.Serialize<IWidget>(widget, _serializerOptions);
            return MessagePackSerializer.Deserialize<SerializedWidget>(data, _serializerOptions);
        }
    }
}
