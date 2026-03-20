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
        public record TestWidget : WidgetBase<TestWidget>
        {
            [Prop]
            public string TestProp1 { get; set; } = "default";

            [Event]
            public EventHandler<Event<TestWidget>>? TestEvent { get; set; }
        }

        [MessagePackObject]
        public record ExpectedDeserializedStructure
        {
            [Key(0)]
            public required string Type { get; init; }

            [Key(1)]
            public required string Id { get; init; }

            [Key(2)]
            public required IDictionary<string, object> Props { get; init; }

            [Key(3)]
            public required string[] Events { get; init; }

            [Key(4)]
            public required ExpectedDeserializedStructure[] Children { get; init; }
        }

        private static MessagePackSerializerOptions _serializerOptions =
            new MessagePackSerializerOptions(CompositeResolver.Create([new WidgetSerializer()], [StandardResolver.Instance]));

        [Fact]
        public void TestWidget_BasicValues()
        {
            var expectedType = "Ivy.Test.Sync.TestWidget";
            var expectedId = "greoij";
            var expectedEvents = new string[0];

            var widget = new TestWidget()
            {
                Id = expectedId
            };

            var data = MessagePackSerializer.Serialize<Core.IWidget>(widget, _serializerOptions);
            var result = MessagePackSerializer.Deserialize<ExpectedDeserializedStructure>(data, _serializerOptions);

            Assert.Equal(expectedType, result.Type);
            Assert.Equal(expectedId, result.Id);
            Assert.Empty(result.Props);
            Assert.Empty(result.Events);
            Assert.Empty(result.Children);
        }

        [Fact]
        public void TestWidget_AddedValues()
        {
            var expectedType = "Ivy.Test.Sync.TestWidget";
            var expectedId = "doijqew";
            var expectedEvents = new string[] { "TestEvent" };
            var expectedProp1 = "nondefault";

            var widget = new TestWidget()
            {
                TestProp1 = expectedProp1,
                TestEvent = new(_ => ValueTask.CompletedTask)
            };
            widget.Id = expectedId;

            var data = MessagePackSerializer.Serialize<Core.IWidget>(widget, _serializerOptions);
            var obj = MessagePackSerializer.Deserialize<ExpectedDeserializedStructure>(data, _serializerOptions);

            Assert.Equal(expectedType, obj.Type);
            Assert.Equal(expectedId, obj.Id);
            Assert.Equal(expectedEvents, obj.Events);
            Assert.NotEmpty(obj.Props);
            var objProp1 = obj.Props["testProp1"];
            Assert.Equal(expectedProp1, objProp1);
            Assert.Empty(obj.Children);
        }

        [Fact]
        public void TestWidget_WithChildren()
        {
            var expectedId = "greoij";
            var expectedChild1Id = "diojwef";
            var expectedChild2Id = "fgewoijf";
            var expectedEvents = new string[0];

            var widget = new TestWidget()
            {
                Id = expectedId,
                Children = new object[]
                {
                    new TestWidget(){Id = expectedChild1Id, TestProp1 = "nondefault1"},
                    new TestWidget(){Id = expectedChild2Id}
                }
            };

            var data = MessagePackSerializer.Serialize<Core.IWidget>(widget, _serializerOptions);
            var result = MessagePackSerializer.Deserialize<ExpectedDeserializedStructure>(data, _serializerOptions);

            Assert.Equal(2, result.Children.Length);
            var child1 = result.Children[0];
            Assert.Equal(expectedChild1Id, child1.Id);
            Assert.Contains("testProp1", child1.Props);
            Assert.Equal("nondefault1", child1.Props["testProp1"]);
            var child2 = result.Children[1];
            Assert.Equal(expectedChild2Id, child2.Id);
            Assert.DoesNotContain("testProp1", child2.Props);
        }
    }
}
