using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Ivy.Benchmark.Samples.Apps
{
    [App]
    internal class TreeApp : ViewBase
    {
        private record Node(string Text, ImmutableList<Node> Children);

        private static Random _rand = new Random(123456789);

        private static Node GenerateTree(int maxDepth)
        {

            var children = Enumerable.Repeat(0, maxDepth == 0 ? 0 : _rand.Next(5))
                .Select(_ => GenerateTree(maxDepth - 1))
                .ToImmutableList();

            return new Node(_rand.Next().ToString(), children);
        }

        private static Node _initialTree = GenerateTree(5);

        private object Node2Widget(Node node)
        {
            var subdirItems = node.Children.Select(Node2Widget);

            if (node.Children.IsEmpty)
            {
                return new ListItem(title: node.Text);
            }

            

            return Layout.Horizontal()
                | Text.Literal(node.Text)
                | new List(node.Children.Select(Node2Widget));
        }

        public override object? Build()
        {
            var nodeTree = UseState<Node?>(() => _initialTree);

            var mixTypes = UseState(false);

            var enableKeys = UseState(false);

            var search = UseState<string?>(null);

            UseEffect(() =>
            {
                var newTree = _initialTree;
                if (search.Value is not null)
                {
                    Node? FilterNode(Node node, string term)
                    {
                        var children = node.Children.Select(child => FilterNode(child, term))
                            .Where(child => child is not null)
                            .Cast<Node>()
                            .ToImmutableList();

                        if (children.IsEmpty && !node.Text.Contains(term))
                        {
                            return null;
                        }
                        return node with { Children = children };
                    }
                    ;
                    newTree = FilterNode(newTree, search.Value);
                }
                nodeTree.Set(newTree);
            }, search, enableKeys, mixTypes);

            return Layout.Vertical()
                | "Mix types" | mixTypes.ToBoolInput()
                | "Enable Keys" | enableKeys.ToBoolInput()
                | search.ToTextInput()
                | (nodeTree.Value is null ? new List() : new List(Node2Widget(nodeTree.Value)));
        }

    }
}
