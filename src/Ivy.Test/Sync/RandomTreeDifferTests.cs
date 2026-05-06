using Ivy.Core;
using Ivy.Core.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Test.Sync
{
    public class RandomTreeDifferTests
    {
        private TreeDiffer _linearNoPropDiffer = new(new(TreeChildrenDiffer.Linear, false));
        private TreeDiffer _linearWithPropDiffer = new(new(TreeChildrenDiffer.Linear, true));
        private TreeDiffer _lcsNoPropDiffer = new(new(TreeChildrenDiffer.LCS, false));
        private TreeDiffer _lcsWithPropDiffer = new(new(TreeChildrenDiffer.LCS, true));

        private static void AssertUpdateIsSorted(WidgetUpdate update)
        {
            var changes = update.Children?.Changes;
            if (changes is null)
            {
                return;
            }
            foreach (var (left, right) in changes.SkipLast(1).Zip(changes.Skip(1)))
            {
                Assert.True(left.SortIndex < right.SortIndex);
            }
            foreach (var change in changes)
            {
                if (change is WidgetListUpdate elementUpdate)
                {
                    AssertUpdateIsSorted(elementUpdate.Update);
                }
            }
        }

        private void TestAllDiffers(WidgetNode source, WidgetNode target)
        {
            var convertedSource = MockWidgetNode.FromWidgetNode(source);
            var convertedTarget = MockWidgetNode.FromWidgetNode(target);

            TreeDiffer[] differs = [
                _linearNoPropDiffer,
                _linearWithPropDiffer,
                _lcsNoPropDiffer,
                _lcsWithPropDiffer
            ];

            foreach (var differ in differs)
            {
                var result = differ.ComputeDiff(source, target);
                switch (result)
                {
                    case WidgetUpdate update:
                        AssertUpdateIsSorted(update);
                        var updatedSource = convertedSource.ApplyDiff(update);
                        MockWidgetNode.AssertEqual(convertedTarget, updatedSource);
                        break;
                    case WidgetNode newNode:
                        var convertedNewNode = MockWidgetNode.FromWidgetNode(newNode);
                        MockWidgetNode.AssertEqual(convertedTarget, convertedNewNode);
                        break;
                    case null:
                        MockWidgetNode.AssertEqual(convertedTarget, convertedSource);
                        break;
                    default:
                        throw new Exception("Invalid result from ComputeDiff");
                }
            }
        }

        private static string[] texts = [
                "Five foxes four fairies",
                "Lorem ipsum dolor sit amet",
                "Aliquam augue massa",
                "Vivamus lobortis diam id nulla mattis"
            ];

        private TestWidget GenerateRandomTestWidget(Random rand)
        {
            var widget = new TestWidget();
            if (rand.Next(2) == 0)
            {
                widget.TestEvent = new(_ => ValueTask.CompletedTask);
            }
            if (rand.Next(2) == 0)
            {
                widget.TestProp1 = texts[rand.Next(texts.Length)];
            }
            if (rand.Next(2) == 0)
            {
                widget.TestProp2 = Enumerable.Range(0, rand.Next(4))
                    .Select<int, KeyValuePair<string, string?[]>>(idx => new(
                            rand.Next(5).ToString(),
                            Enumerable.Range(0, rand.Next(5))
                                .Select(_ => rand.Next(10) == 0 ? null : texts[rand.Next(texts.Length)])
                                .ToArray()
                        ))
                    .DistinctBy(e => e.Key)
                    .ToDictionary();
            }
            if (rand.Next(2) == 0)
            {
                widget.TestProp3 = TestWidget.TestEnum.Second;
            }
            return widget;
        }

        private IWidget GenerateRandomTree(Random rand, int depth)
        {
            if (depth == 1)
            {
                return (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!;
            }
            var prefix = rand.Next().ToString();

            var textNode = (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!;
            var nodes = Enumerable.Range(0, rand.Next(2, 5))
                .Select(idx => rand.Next(2) switch
                {
                    0 => (WidgetBase)GenerateRandomTree(rand, depth - 1),
                    1 => GenerateRandomTestWidget(rand),
                    _ => throw new Exception("Test is faulty")
                } with
                { Id = prefix + idx.ToString() });

            return new List(nodes);
        }

        [Fact]
        public void TreeDiffer_Replace()
        {
            var sourceNode = new TestWidget("qwodijqwd").ToWidgetNode();
            var targetNode = ((WidgetBase)Text.H3("qwoijqd").Build()! with { Id = "qwdoijq" }).ToWidgetNode();

            TestAllDiffers(sourceNode, targetNode);
        }

        [Fact]
        public void TreeDiffer_RandomTree()
        {
            var source = GenerateRandomTree(new Random(), (int)Math.Log2(1000));
            source.Id = "dwqpokqwd";
            var target = GenerateRandomTree(new Random(), (int)Math.Log2(1000));
            target.Id = "dwqpokqwd";

            TestAllDiffers(source.ToWidgetNode(), target.ToWidgetNode());
        }
    }
}
