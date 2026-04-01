using Force.DeepCloner;
using Ivy.Core;
using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace Ivy.Test.Sync
{
    public class TreeDifferTests
    {
        private static MessagePackSerializerOptions _serializerOptions =
            new MessagePackSerializerOptions(CompositeResolver.Create([new Core.Sync.WidgetSerializer()], [StandardResolver.Instance]));


        private SerializedWidget ApplyDiff(SerializedWidget source, WidgetUpdate update)
        {
            var widget = source;

            if (update.Type != null)
            {
                widget = widget with { Type = CleanTypeName(update.Type) };
            }

            if (update.Id != null)
            {
                widget = widget with { Id = update.Id };
            }

            if (update.Props != null)
            {
                foreach (var (name, newValue) in update.Props)
                {
                    if (newValue == null)
                    {
                        widget = widget with { Props = widget.Props.Remove(name) };
                    }
                    else
                    {
                        widget = widget with { Props = widget.Props.SetItem(name, newValue) };
                    }
                }
            }

            if (update.Events != null)
            {
                widget = widget with { Events = update.Events };
            }

            if (update.Children != null)
            {
                if (update.Children.Changes != null)
                {
                    var children = widget.Children;

                    // Assume changes are sorted by SortIndex.
                    // Reversing ensures that unaffected children
                    // keep the same index after each change.
                    foreach (var change in update.Children.Changes.Reverse())
                    {
                        if (change is WidgetListUpdate nestedUpdate)
                        {
                            var newWidget = ApplyDiff(children[nestedUpdate.Index], nestedUpdate.Update);
                            children = children.SetItem(nestedUpdate.Index, newWidget);
                        }
                        else if (change is WidgetListAdd add)
                        {
                            var convertedWidget = SerializedWidget.FromWidget(add.Widget);
                            children = children.Insert(add.Index, convertedWidget);
                        }
                        else if (change is WidgetListAddRange addRange)
                        {
                            var convertedWidgets = addRange.Widgets
                                .Select(SerializedWidget.FromWidget);
                            children = children.InsertRange(addRange.Index, convertedWidgets);
                        }
                        else if (change is WidgetListReplace replace)
                        {
                            if (replace.Widget == null)
                            {
                                children = children.RemoveAt(replace.Index);
                            }
                            else
                            {
                                var convertedWidget = SerializedWidget.FromWidget(replace.Widget);
                                children = children.SetItem(replace.Index, convertedWidget);
                            }
                        }
                        else if (change is WidgetListReplaceRange replaceRange)
                        {
                            var length = replaceRange.EndIndex - replaceRange.StartIndex + 1;
                            if (replaceRange.Widgets == null)
                            {
                                children = children.RemoveRange(replaceRange.StartIndex, length);
                            }
                            else
                            {
                                var convertedWidgets = replaceRange.Widgets
                                    .Select(SerializedWidget.FromWidget);
                                children = children.RemoveRange(replaceRange.StartIndex, length);
                                children = children.InsertRange(replaceRange.StartIndex, convertedWidgets);
                            }
                        }
                    }
                    widget = widget with { Children = children };
                }
            }

            return widget;
        }

        private static string CleanTypeName(Type t)
        {
            return t.Namespace + "." + Utils.CleanGenericNotation(t.Name);
        }

        [Fact]
        public void ComputeDiff_SimpleChanges()
        {
            var source = new TestWidget() { Id = "dwiojf"};
            var convertedSource = SerializedWidget.FromWidget(source);
            var target = new TestWidget() { Id = source.Id, TestProp1 = "nondefault" };
            var expectedTarget = SerializedWidget.FromWidget(target);

            var update = TreeDiffer.ComputeDiff(source, target);

            Assert.IsType<WidgetUpdate>(update);

            var computedTarget = ApplyDiff(convertedSource, (WidgetUpdate)update);
            Assert.Equivalent(expectedTarget, computedTarget, true);
        }

        [Fact]
        public void ComputeDiff_AddChildren()
        {
            var source = new TestWidget()
            {
                Id = "pokwefp",
                Children = [ ]
            };
            var target = new TestWidget()
            {
                Id = "pokwefp",
                Children =
                [
                    new TestWidget()
                    {
                        Id = "dqwpok",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "dwqioj",
                        TestProp1 = "foo"
                    }
                ]
            };
            var convertedSource = SerializedWidget.FromWidget(source);
            var convertedTarget = SerializedWidget.FromWidget(target);

            var update = TreeDiffer.ComputeDiff(source, target);

            Assert.IsType<WidgetUpdate>(update);

            var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

            Assert.Equivalent(convertedTarget, updatedSource);

        }
    }
}
