using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;

namespace Ivy.Test.Sync
{
    public class WidgetSerializerTests
    {
        private record TestWidget : WidgetBase<TestWidget>
        {
            [Prop]
            public string TestProp1 { get; set; } = "default";

            [Event]
            public EventHandler<Event<TestWidget>>? TestEvent { get; set; }
        }

        [Fact]
        public void TestWidget_Serializes_Values()
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

            var resolver = CompositeResolver.Create([new WidgetSerializer()], [StandardResolver.Instance]);

            var serializeOptions = new MessagePackSerializerOptions(resolver);

            var data = MessagePackSerializer.Serialize<Ivy.Core.IWidget>(widget, serializeOptions);

            var deserializeOptions = new MessagePackSerializerOptions(StandardResolver.Instance);

            var obj = MessagePackSerializer.Deserialize<(string type,
                string id,
                IDictionary<string, object> props,
                string[] events,
                object[] children)>(data, deserializeOptions);

            Assert.Equal(obj.type, expectedType);
            Assert.Equal(obj.id, expectedId);
            Assert.Equal(obj.events, expectedEvents);
            Assert.NotEmpty(obj.props);
            var objProp1 = obj.props["testProp1"];
            Assert.Equal(objProp1, expectedProp1);
            Assert.Empty(obj.children);
        }
    }
}
