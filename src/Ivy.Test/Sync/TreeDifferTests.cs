using Force.DeepCloner;
using Ivy.Core;
using Ivy.Core.Sync;
using Ivy.Test.DataTables;
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

        [Fact]
        public void ComputeDiff_SimpleChanges()
        {
            var source = new WidgetNode(new TestWidget() { Id = "dwiojf" });
            var target = new WidgetNode(new TestWidget() { Id = source.Id, TestProp1 = "nondefault" });

            TestAllDiffers(source, target);
        }

        [Fact]
        public void ComputeDiff_AddChildren()
        {
            var source = new TestWidget()
            {
                Id = "pokwefp",
                Children = 
                [
                    new TestWidget()
                    {
                        Id = "12345",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "56789",
                        TestProp1 = "foo"
                    },
                ]
            };
            var target = new TestWidget()
            {
                Id = "pokwefp",
                Children =
                [
                    new TestWidget()
                    {
                        Id = "12345",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "23456",
                        TestProp1 = "foo"
                    },
                    new TestWidget()
                    {
                        Id = "34567",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "56789",
                        TestProp1 = "foo"
                    },
                    new TestWidget()
                    {
                        Id = "67890",
                        TestProp1 = "foo"
                    }
                ]
            };

            TestAllDiffers(source.ToWidgetNode(), target.ToWidgetNode());
        }

        [Fact]
        public void ComputeDiff_RemoveChildren()
        {
            var source = new TestWidget()
            {
                Id = "pokwefp",
                Children =
                [
                    new TestWidget()
                    {
                        Id = "12345",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "23456",
                        TestProp1 = "foo"
                    },
                    new TestWidget()
                    {
                        Id = "34567",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "56789",
                        TestProp1 = "foo"
                    },
                    new TestWidget()
                    {
                        Id = "67890",
                        TestProp1 = "foo"
                    }
                ]
            };
            var target = new TestWidget()
            {
                Id = "pokwefp",
                Children =
                [
                    new TestWidget()
                    {
                        Id = "12345",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "34567",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                ]
            };

            TestAllDiffers(source.ToWidgetNode(), target.ToWidgetNode());
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
            var nodes = Enumerable.Range(0, rand.Next(2, 5))
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
        public void TreeDiffer_NoChange()
        {
            var source = GenerateRandomTree(new Random(), (int)Math.Log2(100));
            source.Id = "dwqpokqwd";

            TestAllDiffers(source.ToWidgetNode(), source.ToWidgetNode());
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

            TestAllDiffers(sourceNode, targetNode);
        }

        [Fact]
        public void TreeDiffer_AttachedPropChange()
        {
            var source = (WidgetBase)Layout.Grid(
                new TestWidget("qwdoij"),
                new TestWidget("qwpdok"),
                new TestWidget("regerg"),
                new TestWidget("dqwioi"))
                .Columns(2).Build()! with
            { Id = "qwoijd" };
            var target = (WidgetBase)Layout.Grid(
                new TestWidget("qwdoij"),
                new TestWidget("qwpdok"),
                new TestWidget("regerg"),
                new TestWidget("dqwoij"),
                new TestWidget("odiqjw"))
                .Columns(5).Build()! with
            { Id = "qwoijd" };

            TestAllDiffers(source.ToWidgetNode(), target.ToWidgetNode());
        }
    }
}
