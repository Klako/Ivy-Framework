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

            var testCase = new
            {
                source = source,
                target = target,
                updates = new WidgetUpdate?[4]
            };

            foreach (var (index, differ) in differs.Index())
            {
                var result = differ.ComputeDiff(source, target);
                switch (result)
                {
                    case WidgetUpdate update:
                        AssertUpdateIsSorted(update);
                        testCase.updates[index] = update;
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
#pragma warning disable 0162
            if (false)
            {

                if (!File.Exists("tests.msgpack"))
                {
                    File.Create("tests.msgpack").Close();
                }

                FileStream fs = File.Open("tests.msgpack", FileMode.Append);
                MessagePackSerializer.Serialize(fs, testCase);
                fs.Close();
            }
#pragma warning restore 0162
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
                    },
                    new TestWidget()
                    {
                        Id = "78901",
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
                    },
                    new TestWidget()
                    {
                        Id = "78901",
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
                    {"Noo", [null, "Nis", null] },
                    {"Coo", ["Car", "Cdr"] }
                }
            };
            var target = new TestWidget()
            {
                Id = "diqjwdqw",
                TestProp2 = new()
                {
                    {"Foo", ["Biz", "Raz"] },
                    {"Zoo", ["Mis", "Mas", "Mar"]},
                    {"Roo", ["Ris"] },
                    {"Noo", ["Nis", null, null] }
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

        [Fact]
        public void TreeDiffer_Replace()
        {
            var sourceNode = new TestWidget("qwodijqwd").ToWidgetNode();
            var targetNode = ((WidgetBase)Text.H3("qwoijqd").Build()! with { Id = "qwdoijq" }).ToWidgetNode();

            TestAllDiffers(sourceNode, targetNode);
        }

        [Fact]
        public void TreeDiffer_ReplaceChildren()
        {
            var sourceNode = new TestWidget("qwiojdqw")
            {
                Children = [
                    new TestWidget("dwqiojqwd1"),
                    new TestWidget("dwqiojqwd2"),
                    new TestWidget("dwqiojqwd3"),
                    new TestWidget("dwqiojqwd4")
                ]
            }.ToWidgetNode();

            var targetNode = new TestWidget("qwiojdqw")
            {
                Children = [
                    new TestWidget2("dqiowjqwd1"),
                    new TestWidget2("dqiowjqwd2"),
                    new TestWidget2("dqiowjqwd3"),
                ]
            }.ToWidgetNode();

            TestAllDiffers(sourceNode, targetNode);
        }

        [Fact]
        public void TreeDiffer_NoChange()
        {
            var sourceNode = new TestWidget("dwqoijd")
            {
                Children = [
                    new TestWidget("dwqiojqd1")
                    {
                        TestProp2 = new(){
                            {"Foo", ["Bar"] }
                        }
                    },
                    new TestWidget2("dwqiojqd2"),
                    new TestWidget2("dwqiojqd2"),
                    new TestWidget2("dwqiojqd2"){
                        TestProp2 = new(){
                            {"Foo", [null]}
                        }
                    },
                ]
            }.ToWidgetNode();

            TestAllDiffers(sourceNode, sourceNode);
        }
    }
}
