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
        private TreeDiffer _linearNoPropDiffer = new(new(TreeChildrenDiffer.Linear, false));
        private TreeDiffer _linearWithPropDiffer = new(new(TreeChildrenDiffer.Linear, true));
        private TreeDiffer _lcsNoPropDiffer = new(new(TreeChildrenDiffer.LCS, false));
        private TreeDiffer _lcsWithPropDiffer = new(new(TreeChildrenDiffer.LCS, true));

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
                    if (!widget.Props.TryGetValue(name, out var oldNode))
                    {
                        oldNode = new PropStructureLeaf(null);
                    }
                    var newNode = ApplyPropDiff(oldNode, propUpdate);
                    if (newNode is PropStructureLeaf leaf && leaf.Value == null)
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

        private static IPropStructureNode ApplyPropDiff(IPropStructureNode source, IPropUpdate? update)
        {
            if (update is PropValueDiff valueDiff)
            {
                return valueDiff.NewValue;
            }
            else if (update is PropObjectDiff objectDiff)
            {
                Assert.NotNull(source);
                Assert.IsType<PropStructureObject>(source);
                var members = ((PropStructureObject)source).Members;
                foreach (var (key, change) in objectDiff.Changes)
                {
                    if (change is PropObjectUpdate fieldUpdate)
                    {
                        members = members.SetItem(key, ApplyPropDiff(members[key], fieldUpdate.Update));
                    }
                    else if (change is PropObjectSet fieldSet)
                    {
                        members = members.SetItem(key, fieldSet.NewValue);
                    }
                    else if (change is PropObjectRemove fieldRemove)
                    {
                        members = members.Remove(key);
                    }
                }
                return new PropStructureObject(members);
            }
            else if (update is PropArrayDiff arrayDiff)
            {
                Assert.NotNull(source);
                Assert.IsType<PropStructureList>(source);
                var members = ((PropStructureList)source).Members;
                foreach (var (index, change) in arrayDiff.Changes)
                {
                    members = members.SetItem(index, ApplyPropDiff(members[index], change));
                }
                if (arrayDiff.Removals > 0)
                {
                    var fromIndex = members.Count - arrayDiff.Removals;
                    members = members.RemoveRange(fromIndex, arrayDiff.Removals);
                }
                foreach (var node in arrayDiff.Appends)
                {
                    members = members.Add(node);
                }
                return new PropStructureList(members);
            }
            return source;
        }

        private static string CleanTypeName(Type t)
        {
            return t.Namespace + "." + Utils.CleanGenericNotation(t.Name);
        }

        [Fact]
        public void ComputeDiff_SimpleChanges()
        {
            var source = new WidgetNode(new TestWidget() { Id = "dwiojf" });
            var convertedSource = SerializedWidget.FromWidget(source);
            var target = new WidgetNode(new TestWidget() { Id = source.Id, TestProp1 = "nondefault" });
            var expectedTarget = SerializedWidget.FromWidget(target);

            var treeDifferOptions = new TreeDifferOptions(TreeChildrenDiffer.Linear, false);
            var treeDiffer = new TreeDiffer(treeDifferOptions);

            var update = treeDiffer.ComputeDiff(source, target);

            Assert.IsType<WidgetUpdate>(update);

            var computedTarget = ApplyDiff(convertedSource, (WidgetUpdate)update);
            SerializedWidget.AssertEqual(expectedTarget, computedTarget);
        }

        [Fact]
        public void ComputeDiff_AddChildren_Linear()
        {
            var source = new TestWidget()
            {
                Id = "pokwefp",
                Children = []
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
            var convertedSource = SerializedWidget.FromWidget(source.ToWidgetNode());
            var expectedTarget = SerializedWidget.FromWidget(target.ToWidgetNode());

            var update = _linearNoPropDiffer.ComputeDiff(source.ToWidgetNode(), target.ToWidgetNode());

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

        private IWidget GenerateRandomTree(Random rand, int depth)
        {
            if (depth == 1)
            {
                return (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!;
            }
            var prefix = rand.Next().ToString();

            var textNode = (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!;
            var nodes = Enumerable.Range(0, rand.Next(5))
                .Select(idx => rand.Next(2) switch
                {
                    0 => (WidgetBase)GenerateRandomTree(rand, depth - 1),
                    1 => (WidgetBase)Text.H3(texts[rand.Next(texts.Length)]).Build()!,
                    _ => throw new Exception("Test is faulty")
                } with
                { Id = prefix + idx.ToString() });

            return new List(nodes);
        }

        [Fact]
        public void TreeDiffer_RandomBinaryTree()
        {
            var source = GenerateRandomTree(new Random(), (int)Math.Log2(100));
            source.Id = "dwqpokqwd";
            var target = GenerateRandomTree(new Random(), (int)Math.Log2(100));
            target.Id = "dwqpokqwd";

            var sourceNode = source.ToWidgetNode();
            var targetNode = target.ToWidgetNode();

            var convertedSource = SerializedWidget.FromWidget(sourceNode);
            var convertedTarget = SerializedWidget.FromWidget(targetNode);

            var update = _linearNoPropDiffer.ComputeDiff(sourceNode, targetNode);

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

            var sourceNode = source.ToWidgetNode();
            var targetNode = target.ToWidgetNode();

            var convertedSource = SerializedWidget.FromWidget(sourceNode);
            var expectedTarget = SerializedWidget.FromWidget(targetNode);

            {
                var update = _linearNoPropDiffer.ComputeDiff(sourceNode, targetNode);

                Assert.IsType<WidgetUpdate>(update);

                var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

                SerializedWidget.AssertEqual(expectedTarget, updatedSource);
            }

            {
                var update = _linearWithPropDiffer.ComputeDiff(sourceNode, targetNode);

                Assert.IsType<WidgetUpdate>(update);

                var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

                SerializedWidget.AssertEqual(expectedTarget, updatedSource);
            }

            {
                var update = _lcsNoPropDiffer.ComputeDiff(sourceNode, targetNode);

                Assert.IsType<WidgetUpdate>(update);

                var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

                SerializedWidget.AssertEqual(expectedTarget, updatedSource);
            }

            {
                var update = _lcsWithPropDiffer.ComputeDiff(sourceNode, targetNode);

                Assert.IsType<WidgetUpdate>(update);

                var updatedSource = ApplyDiff(convertedSource, (WidgetUpdate)update);

                SerializedWidget.AssertEqual(expectedTarget, updatedSource);
            }
        }
    }
}
