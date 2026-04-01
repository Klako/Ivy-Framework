using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Test.Sync
{
    public record TestWidget : WidgetBase<TestWidget>
    {
        public static string StringType = "Ivy.Test.Sync.TestWidget";

        [Prop]
        public string TestProp1 { get; set; } = "default";

        [Event]
        public EventHandler<Event<TestWidget>>? TestEvent { get; set; }
    }
}
