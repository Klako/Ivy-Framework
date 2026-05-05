using Ivy.Core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Ivy.Benchmark.Samples.Apps
{
    [App]
    internal class ListApp : ViewBase
    {
        private static Random _rand = new Random(123456789);
        private static ImmutableList<string> _initialList =
            Enumerable.Repeat(0, 1000)
                .Select(_ => _rand.Next().ToString())
                .ToImmutableList();

        enum SortOrder
        {
            Ascending,
            Descending
        };

        public override object? Build()
        {
            var interactCounter = UseState(0);

            var list = UseState(() => _initialList);

            var enableKeys = UseState(false);
            var mixTypes = UseState(false);
            var listSize = UseState(1000);
            var filter = UseState<string?>(() => null);
            var sort = UseState<SortOrder?>(() => null);

            UseEffect(() =>
            {
                var newList = _initialList.Take(listSize.Value).ToImmutableList();
                if (filter.Value is not null)
                {
                    newList = newList.RemoveAll((e) => !e.Contains(filter.Value));
                }
                if (sort.Value is not null)
                {
                    var compareSign = sort.Value switch
                    {
                        SortOrder.Ascending => 1,
                        SortOrder.Descending => -1,
                        _ => throw new Exception("Invalid SortOrder")
                    };
                    newList = newList.Sort((e1, e2) => e1.CompareTo(e2) * compareSign);
                }
                list.Set(newList);
                interactCounter.Set(interactCounter.Value + 1);
            }, filter, sort, listSize, enableKeys);

            return Layout.Vertical()
                | "Interact counter" | interactCounter.ToNumberInput().Disabled().TestId("interactCounter")
                | "Use keys" | enableKeys.ToBoolInput().TestId("useKeys")
                | "Mix types" | mixTypes.ToBoolInput().TestId("mixTypes")
                | "List size" | listSize.ToNumberInput().TestId("listSize")
                | "Filter" | filter.ToTextInput().TestId("filterText")
                | "Sort by" | sort.ToSelectInput(variant: SelectInputVariant.Radio).TestId("sortBy")
                | list.Value
                    .Take(listSize.Value)
                    .Select(e => new ListItem(e) with { Key = enableKeys.Value ? e : null });
        }
    }
}
