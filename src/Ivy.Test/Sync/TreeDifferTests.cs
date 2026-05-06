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
    [Collection("TestCasesAccumulator")]
    public class TreeDifferTests
    {
        private TreeDiffer _linearNoPropDiffer = new(new(TreeChildrenDiffer.Linear, false));
        private TreeDiffer _linearWithPropDiffer = new(new(TreeChildrenDiffer.Linear, true));
        private TreeDiffer _lcsNoPropDiffer = new(new(TreeChildrenDiffer.LCS, false));
        private TreeDiffer _lcsWithPropDiffer = new(new(TreeChildrenDiffer.LCS, true));

        private readonly UpdateTestCaseFixture _fixture;

        public TreeDifferTests(UpdateTestCaseFixture fixture)
        {
            _fixture = fixture;
        }

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
            var convertedSource = SerializedWidget.FromWidgetNode(source);
            var convertedTarget = SerializedWidget.FromWidgetNode(target);

            (string, TreeDiffer)[] differs = [
                ("linear+nopropdiff",_linearNoPropDiffer),
                ("linear+propdiff", _linearWithPropDiffer),
                ("lcs+nopropdiff",_lcsNoPropDiffer),
                ("lcs+propdiff",_lcsWithPropDiffer)
            ];

            var updates = new List<(string, WidgetUpdate)>();

            foreach (var (index, (algo, differ)) in differs.Index())
            {
                var result = differ.ComputeDiff(source, target);
                switch (result)
                {
                    case WidgetUpdate update:
                        AssertUpdateIsSorted(update);
                        updates.Add((algo, update));
                        var updatedSource = convertedSource.ApplyDiff(update);
                        SerializedWidget.AssertEqual(convertedTarget, updatedSource);
                        break;
                    case WidgetNode newNode:
                        var convertedNewNode = SerializedWidget.FromWidgetNode(newNode);
                        SerializedWidget.AssertEqual(convertedTarget, convertedNewNode);
                        break;
                    case null:
                        SerializedWidget.AssertEqual(convertedTarget, convertedSource);
                        break;
                    default:
                        throw new Exception("Invalid result from ComputeDiff");
                }
            }
           
            if (updates.Count > 0)
            {
                _fixture.TestCases.Add(new(source, target, updates));
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
        public void TreeDiffer_ChildrenSameEnd()
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
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "34567",
                        TestProp3 = TestWidget.TestEnum.Second
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
                        TestProp1 = "nondefault",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "23456",
                        TestEvent = new(_ => ValueTask.CompletedTask)
                    },
                    new TestWidget()
                    {
                        Id = "34567",
                        TestProp3 = TestWidget.TestEnum.Second
                    }
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
