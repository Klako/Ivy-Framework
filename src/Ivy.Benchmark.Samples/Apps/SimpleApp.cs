using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Benchmark.Samples.Apps
{
    [App]
    internal class SimpleApp : ViewBase
    {
        enum Options
        {
            A,
            B,
            C
        }

        public override object? Build()
        {
            var interactCounter = UseState(0);

            var text = UseState("");
            var toggle = UseState(false);
            var option = UseState(Options.A);

            UseEffect(() =>
            {
                interactCounter.Set(interactCounter.Value + 1);
            }, text, toggle, option);

            return Layout.Vertical()
                | Layout.Horizontal("Interact counter", interactCounter.ToNumberInput().Disabled().TestId("interactCounter"))
                | Layout.Horizontal("Text input", text.ToTextInput().TestId("textInput"))
                | Layout.Horizontal("Bool input", toggle.ToBoolInput().TestId("boolInput"))
                | Layout.Horizontal("radio input", option.ToSelectInput(variant: SelectInputVariant.Radio).TestId("radioInput"));
        }
    }
}
