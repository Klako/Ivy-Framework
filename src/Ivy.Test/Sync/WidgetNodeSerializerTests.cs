using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Text.Json.Nodes;

namespace Ivy.Test.Sync
{
    public class WidgetNodeSerializerTests
    {
        [Fact]
        public void TestWidget_BasicValues()
        {
            var expected = new SerializedWidget(
                TestWidget.StringType,
                "greoij");

            var widget = new TestWidget()
            {
                Id = expected.Id
            }.ToWidgetNode();

            var result = SerializedWidget.FromWidgetNode(widget);

            Assert.Equivalent(expected, result, true);
        }

        [Fact]
        public void TestWidget_AddedValues()
        {
            var expected = new SerializedWidget(
                TestWidget.StringType,
                "greoij")
            {
                Props = ImmutableDictionary<string, IPropStructureNode>.Empty
                    .Add("testProp1", new PropStructureLeaf("nondefault")),
                Events = ["TestEvent"],
                Children = []
            };

            var widget = new TestWidget()
            {
                TestProp1 = "nondefault",
                TestEvent = new(_ => ValueTask.CompletedTask)
            };
            widget.Id = expected.Id;

            var result = SerializedWidget.FromWidgetNode(widget.ToWidgetNode());

            SerializedWidget.AssertEqual(expected, result);
        }

        [Fact]
        public void TestWidget_WithChildren()
        {
            var expected = new SerializedWidget("Ivy.Test.Sync.TestWidget", "greoij")
            {
                Events = [],
                Props = ImmutableDictionary<string, IPropStructureNode>.Empty,
                Children =
                [
                    new SerializedWidget("Ivy.Test.Sync.TestWidget", "diojwef"){
                        Events = [],
                        Props = ImmutableDictionary<string, IPropStructureNode>.Empty
                            .Add("testProp1", new PropStructureLeaf("nondefault")),
                        Children = []
                    },
                    new SerializedWidget("Ivy.Test.Sync.TestWidget", "diojwef"){
                        Events = [],
                        Props = ImmutableDictionary<string, IPropStructureNode>.Empty,
                        Children = []
                    }
                ]
            };

            var widget = new TestWidget()
            {
                Id = expected.Id,
                Children = new object[]
                {
                    new TestWidget()
                    {
                        Id = expected.Children[0].Id,
                        TestProp1 = "nondefault"
                    },
                    new TestWidget(){Id = expected.Children[1].Id}
                }
            };

            var result = SerializedWidget.FromWidgetNode(widget.ToWidgetNode());

            SerializedWidget.AssertEqual(expected, result);
        }

        [Fact]
        public void TestWidget_WithEnumProp()
        {
            var expected = new SerializedWidget(
                TestWidget.StringType,
                "greoij")
            {
                Props = ImmutableDictionary<string, IPropStructureNode>.Empty
                    .Add("testProp3", new PropStructureLeaf("Second")),
                Children = []
            };

            var widget = new TestWidget()
            {
                TestProp3 = TestWidget.TestEnum.Second,
                TestEvent = new(_ => ValueTask.CompletedTask)
            };
            widget.Id = expected.Id;

            var result = SerializedWidget.FromWidgetNode(widget.ToWidgetNode());

            SerializedWidget.AssertEqual(expected, result);
        }

        [Fact]
        public void TestWidget_WithComplexProp()
        {
            var testProp2 = new PropStructureObject(
                ImmutableDictionary<string, IPropStructureNode>.Empty
                    .Add("Foo", new PropStructureList(
                        ImmutableList<IPropStructureNode>.Empty
                            .AddRange([new PropStructureLeaf("Bar"),
                                       new PropStructureLeaf(null)]))));

            var expected = new SerializedWidget("Ivy.Test.Sync.TestWidget", "dqwoijd")
            {
                Props = ImmutableDictionary<string, IPropStructureNode>.Empty
                    .Add("testProp2", testProp2)
            };

            var widget = new TestWidget(expected.Id)
            {
                TestProp2 = new()
                {
                    {"Foo", ["Bar", null] }
                }
            };

            var result = SerializedWidget.FromWidgetNode(widget.ToWidgetNode());

            SerializedWidget.AssertEqual(expected, result);
        }
    }
}
