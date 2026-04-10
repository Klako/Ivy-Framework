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
using System.Text.Json.Nodes;

namespace Ivy.Test.Sync
{
    public class TreeDifferTests
    {
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
                foreach (var (name, propUpdate) in update.Props)
                {
                    widget.Props.TryGetValue(name, out var oldNode);
                    var newNode = ApplyPropDiff(oldNode, propUpdate);
                    if (newNode == null)
                    {
                        widget = widget with { Props = widget.Props.Remove(name) };
                    }
                    else
                    {
                        widget = widget with { Props = widget.Props.SetItem(name, newNode) };
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
                        else if (change is WidgetListSplice splice)
                        {
                            var convertedWidgets = splice.Widgets.Select(SerializedWidget.FromWidget);
                            children = children.RemoveRange(splice.Index, splice.Length);
                            children = children.InsertRange(splice.Index, convertedWidgets);
                        }
                    }
                    widget = widget with { Children = children };
                }
            }

            return widget;
        }

        private static JsonNode? ApplyPropDiff(JsonNode? source, IPropUpdate? update)
        {
            if (update is PropValueDiff valueDiff)
            {
                return valueDiff.NewValue?.DeepClone();
            }
            else if (update is PropObjectDiff objectDiff)
            {
                Assert.NotNull(source);
                Assert.True(source.GetValueKind() == System.Text.Json.JsonValueKind.Object);
                var sourceObject = source.AsObject();
                foreach (var (key, change) in objectDiff.Changes)
                {
                    if (change is PropObjectUpdate fieldUpdate)
                    {
                        sourceObject[key] = ApplyPropDiff(sourceObject[key], fieldUpdate.Update);
                    }
                    else if (change is PropObjectSet fieldSet)
                    {
                        sourceObject[key] = fieldSet.NewValue?.DeepClone();
                    }
                    else if (change is PropObjectRemove fieldRemove)
                    {
                        sourceObject.Remove(key);
                    }
                }
                return sourceObject.DeepClone();
            }
            else if (update is PropArrayDiff arrayDiff)
            {
                Assert.NotNull(source);
                Assert.True(source.GetValueKind() == System.Text.Json.JsonValueKind.Array);
                var sourceArray = source.AsArray();
                foreach (var (index, change) in arrayDiff.Changes)
                {
                    sourceArray[index] = ApplyPropDiff(sourceArray[index], change);
                }
                if (arrayDiff.Removals > 0)
                {
                    var fromIndex = sourceArray.Count - arrayDiff.Removals;
                    sourceArray.RemoveRange(fromIndex, arrayDiff.Removals);
                }
                foreach (var jsonNode in arrayDiff.Appends)
                {
                    sourceArray.Add(jsonNode?.DeepClone());
                }
                return sourceArray.DeepClone();
            }
            return source?.DeepClone();
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
            SerializedWidget.AssertEqual(expectedTarget, computedTarget);
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
            var expectedTarget = SerializedWidget.FromWidget(target);

            var update = TreeDiffer.ComputeDiff(source, target);

            Assert.IsType<WidgetUpdate>(update);

            var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

            SerializedWidget.AssertEqual(expectedTarget, updatedSource);

        }

        private static string[] texts = [
                "Five foxes four fairies",
                "Lorem ipsum dolor sit amet",
                "Aliquam augue massa",
                "Vivamus lobortis diam id nulla mattis"
            ];

        private IWidget GenerateBinaryTree(Random rand, int depth)
        {
            if (depth == 1)
            {
                return (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!; ;
            }
            var prefix = rand.Next().ToString();

            var nodeText = (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!;
            nodeText.Id = prefix + "1";
            var left = GenerateBinaryTree(rand, depth - 1);
            left.Id = prefix + "2";
            var right = GenerateBinaryTree(rand, depth - 1);
            right.Id = prefix + "3";
            return new List(nodeText, left, right);
        }

        [Fact]
        public void TreeDiffer_RandomBinaryTree()
        {
            var source = GenerateBinaryTree(new Random(), (int)Math.Log2(100));
            source.Id = "dwqpokqwd";
            var target = GenerateBinaryTree(new Random(), (int)Math.Log2(100));
            target.Id = "dwqpokqwd";

            var convertedSource = SerializedWidget.FromWidget(source);
            var convertedTarget = SerializedWidget.FromWidget(target);

            var update = TreeDiffer.ComputeDiff(source, target);

            Assert.IsType<WidgetUpdate>(update);

            var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

            SerializedWidget.AssertEqual(convertedTarget, updatedSource);
        }

        [Fact]
        public void TreeDiffer_ComplexPropChange()
        {
            var source = new TestWidget()
            {
                Id = "diqjwdqw",
                TestProp2 = new()
                {
                    {"Foo", ["Biz", "Baz", "Bar"]},
                    {"Zoo", ["Mis"]},
                }
            };
            var target = new TestWidget()
            {
                Id = "diqjwdqw",
                TestProp2 = new()
                {
                    {"Foo", ["Biz", "Raz"] },
                    {"Zoo", ["Mis", "Mas", "Mar"]},
                    {"Roo", [] }
                }
            };

            var convertedSource = SerializedWidget.FromWidget(source);
            var convertedTarget = SerializedWidget.FromWidget(target);

            var update = TreeDiffer.ComputeDiff(source, target);

            Assert.IsType<WidgetUpdate>(update);

            var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

            SerializedWidget.AssertEqual(convertedTarget, updatedSource);
        }
    }
}
