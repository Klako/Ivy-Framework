using Ivy.Core;
using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Ivy.Test.Sync
{
    public class WidgetUpdateTests
    {
        [Fact]
        public void WidgetUpdate_SerializeResult()
        {
            var update = new WidgetUpdate(
                typeof(TestWidget),
                "qwdppq",
                ImmutableDictionary<string, IPropUpdate>.Empty
                    .Add("testProp1", new PropValueDiff(new PropStructureLeaf("test"))),
                ["OnClick"],
                new WidgetListDiff(null, [
                    new WidgetListUpdate(0, new WidgetUpdate(null, "eqweee", null, null, null)),
                    WidgetListSplice.Insert(1, new TestWidget("dqwpokqw").ToWidgetNode()),
                    WidgetListSplice.RemoveRange(2, 1),
                    WidgetListSplice.Replace(3, new TestWidget("dqwdqwe").ToWidgetNode())
                    ]));
            
            var serialized = MessagePackSerializer.Serialize(update, SerializedWidget.SerializeOptions);
            var deserialized = MessagePackSerializer.Deserialize<object>(serialized, SerializedWidget.SerializeOptions);
            
        }
    }
}
