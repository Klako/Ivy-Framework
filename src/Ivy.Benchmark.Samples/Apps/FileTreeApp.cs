using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Ivy.Benchmark.Samples.Apps
{
    [App]
    internal class FileTreeApp : ViewBase
    {
        private record File(string Name);
        private record Directory(string Name, ImmutableList<Directory> Directories, ImmutableList<File> Files);

        public static Random _rand = new Random(123456789);

        private static Directory GenerateFileTree(int maxDepth)
        {
            var directories = Enumerable.Repeat(0, maxDepth == 0 ? 0 : _rand.Next(5))
                .Select(_ => GenerateFileTree(maxDepth - 1))
                .ToImmutableList();

            var files = Enumerable.Repeat(0, _rand.Next(5))
                .Select(_ => new File(_rand.Next().ToString()))
                .ToImmutableList();

            return new Directory(_rand.Next().ToString(), directories, files);
        }

        private static Directory _initialFileTree = GenerateFileTree(5);

        private MenuItem Directory2MenuItem(Directory dir)
        {
            var subdirItems = dir.Directories.Select(Directory2MenuItem);
            var fileItems = dir.Files.Select(file =>
                new MenuItem(Label: file.Name,
                             Icon: Icons.File));

            return new MenuItem(
                Label: dir.Name,
                Children: subdirItems.Concat(fileItems).ToArray(),
                Expanded: true,
                Icon: Icons.Folder);
        }


        public override object? Build()
        {
            var interactCounter = UseState(0);

            var fileTree = UseState<Directory?>(() => _initialFileTree);

            var enableKeys = UseState(false);

            var search = UseState<string?>(null);

            UseEffect(() =>
            {
                var newTree = _initialFileTree;
                if (search.Value is not null)
                {
                    Directory? FilterDirectory(Directory dir, string term)
                    {
                        var files = dir.Files.RemoveAll((file) => !file.Name.Contains(term));
                        var directories = dir.Directories.Select(subdir => FilterDirectory(subdir, term))
                            .Where(subdir => subdir != null)
                            .Cast<Directory>()
                            .ToImmutableList();

                        if (files.IsEmpty && directories.IsEmpty)
                        {
                            return null;
                        }
                        return dir with { Directories = directories, Files = files };
                    };
                    newTree = FilterDirectory(newTree, search.Value);
                }
                fileTree.Set(newTree);
                interactCounter.Set(interactCounter.Value + 1);
            }, search, enableKeys);

            return Layout.Vertical()
                | interactCounter.ToNumberInput().Disabled().TestId("interactCounter")
                | search.ToTextInput().TestId("searchText")
                | (fileTree.Value is null ? new Tree() : new Tree(Directory2MenuItem(fileTree.Value)));
        }
    }
}
