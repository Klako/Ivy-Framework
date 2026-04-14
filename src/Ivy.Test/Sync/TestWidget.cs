using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Test.Sync
{
    public record TestWidget : WidgetBase<TestWidget>
    {
        public static string StringType = "Ivy.Test.Sync.TestWidget";

        public enum TestEnum
        {
            First,
            Second
        }

        [Prop]
        public string TestProp1 { get; set; } = "default";

        [Prop]
        public Dictionary<string, string[]>? TestProp2 { get; set; } = null;

        [Prop]
        public TestEnum TestProp3 { get; set; } = TestEnum.First;

        [Event]
        public EventHandler<Event<TestWidget>>? TestEvent { get; set; }
    }
}
