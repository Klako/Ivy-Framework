using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Ivy.Test.Sync
{
    public class WidgetSerializerTests
    {
        private static MessagePackSerializerOptions _serializerOptions =
            new MessagePackSerializerOptions(CompositeResolver.Create([new WidgetSerializer()], [StandardResolver.Instance]));

        [Fact]
        public void TestWidget_BasicValues()
        {
            var expected = new SerializedWidget(
                "Ivy.Test.Sync.TestWidget",
                "greoij");

            var widget = new TestWidget()
            {
                Id = expected.Id
            };

            var data = MessagePackSerializer.Serialize<Core.IWidget>(widget, _serializerOptions);
            var result = MessagePackSerializer.Deserialize<SerializedWidget>(data, _serializerOptions);

            Assert.Equivalent(expected, result, true);
        }

        [Fact]
        public void TestWidget_AddedValues()
        {
            var expected = new SerializedWidget(
                "Ivy.Test.Sync.TestWidget",
                "greoij")
            {
                Props = ImmutableDictionary<string, object>.Empty
                    .Add("testProp1", "nondefault"),
                Events = ["TestEvent"],
                Children = []
            };

            var widget = new TestWidget()
            {
                TestProp1 = (string)expected.Props["testProp1"],
                TestEvent = new(_ => ValueTask.CompletedTask)
            };
            widget.Id = expected.Id;

            var data = MessagePackSerializer.Serialize<Core.IWidget>(widget, _serializerOptions);
            var obj = MessagePackSerializer.Deserialize<SerializedWidget>(data, _serializerOptions);

            Assert.Equivalent(expected, obj, true);
        }

        [Fact]
        public void TestWidget_WithChildren()
        {
            var expected = new SerializedWidget("Ivy.Test.Sync.TestWidget", "greoij")
            {
                Events = [],
                Props = ImmutableDictionary<string, object>.Empty,
                Children =
                [
                    new SerializedWidget("Ivy.Test.Sync.TestWidget", "diojwef"){
                        Events = [],
                        Props = ImmutableDictionary<string, object>.Empty
                            .Add("testProp1", "nondefault"),
                        Children = []
                    },
                    new SerializedWidget("Ivy.Test.Sync.TestWidget", "diojwef"){
                        Events = [],
                        Props = ImmutableDictionary<string, object>.Empty,
                        Children = []
                    }
                ]
            };

            var widget = new TestWidget()
            {
                Id = expected.Id,
                Children = new object[]
                {
                    new TestWidget()
                    {
                        Id = expected.Children[0].Id,
                        TestProp1 = (string)expected.Children[0].Props["testProp1"]
                    },
                    new TestWidget(){Id = expected.Children[1].Id}
                }
            };

            var data = MessagePackSerializer.Serialize<Core.IWidget>(widget, _serializerOptions);
            var result = MessagePackSerializer.Deserialize<SerializedWidget>(data, _serializerOptions);

            Assert.Equivalent(expected, result, true);
        }
    }
}
