using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;
using Ivy.Core;
using Ivy.Core.Server.Formatters;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using BenchmarkDotNet.ReportColumns;
using Ivy.Core.Sync;

namespace Ivy.Benchmark.Sync
{
    // For more information on the VS BenchmarkDotNet Diagnosers see https://learn.microsoft.com/visualstudio/profiling/profiling-with-benchmark-dotnet
    [CPUUsageDiagnoser]
    public partial class WidgetTreeSync
    {
        IWidget flatTreeSource;
        IWidget flatTreeTarget;

        IWidget binaryTreeSource;
        IWidget binaryTreeTarget;

        MessagePackSerializerOptions _serializerOptions = MessagePackSerializerOptions.Standard.WithResolver(
                CompositeResolver.Create(
                    new IMessagePackFormatter[] {
                        new JsonNodeMessagePackFormatter(),
                        new JsonObjectMessagePackFormatter(),
                        new JsonArrayMessagePackFormatter(),
                        new JsonValueMessagePackFormatter(),
                        new WidgetMessagePackFormatter()
                    },
                    new IFormatterResolver[] {
                        JsonNodeResolver.Instance,
                        WidgetMessagePackResolver.Instance,
                        ContractlessStandardResolver.Instance
                    }
                )
            );

        private static string[] texts = [
                "Five foxes four fairies",
                "Lorem ipsum dolor sit amet",
                "Aliquam augue massa",
                "Vivamus lobortis diam id nulla mattis"
            ];

        private IWidget[] GenerateTexts(int n)
        {
            var widgets = new IWidget[n];
            var rand = new Random();
            for (int i = 0; i < n; i++)
            {
                widgets[i] = (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!;
                widgets[i].Id = i.ToString();
            }
            return widgets;
        }

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

        [GlobalSetup]
        public void Setup()
        {
            flatTreeSource = new List(GenerateTexts(1000)) { Id = "pokqwd" };
            flatTreeTarget = new List(GenerateTexts(1000)) { Id = "pokqwd" };
            binaryTreeSource = GenerateBinaryTree(new Random(), (int)Math.Log2(1000));
            binaryTreeSource.Id = "dwqpokqwd";
            binaryTreeTarget = GenerateBinaryTree(new Random(), (int)Math.Log2(1000));
            binaryTreeTarget.Id = "dwqpokqwd";
        }

        [ReportColumn(Aggregation = Aggregation.Mean, Name = "Size", Unit = "bytes")]
        public int ResultSize { get; set; }

        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public int RustDiff_FlatTree()
        {
            var sourceWidgetJson = flatTreeSource.Serialize();
            var targetWidgetJson = flatTreeTarget.Serialize();
            var oldBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(sourceWidgetJson);
            var newBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(targetWidgetJson);
            var patch = NativeJsonDiff.ComputePatch(oldBytes, newBytes);
            var serialized = MessagePackSerializer.Serialize(patch, _serializerOptions);
            ResultSize = serialized.Length;
            return serialized.Length;
        }

        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public int NewDiff_FlatTree()
        {
            var diff = Ivy.Core.Sync.TreeDiffer.ComputeDiff(flatTreeSource, flatTreeTarget);
            var serialized = MessagePackSerializer.Serialize(diff, _serializerOptions);
            ResultSize = serialized.Length;
            return serialized.Length;
        }


        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public int RustDiff_BinaryTree()
        {
            var sourceWidgetJson = binaryTreeSource.Serialize();
            var targetWidgetJson = binaryTreeTarget.Serialize();
            var oldBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(sourceWidgetJson);
            var newBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(targetWidgetJson);
            var patch = NativeJsonDiff.ComputePatch(oldBytes, newBytes);
            var serialized = MessagePackSerializer.Serialize(patch, _serializerOptions);
            ResultSize = serialized.Length;
            return serialized.Length;
        }

        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public int NewDiff_BinaryTree()
        {
            var diff = Ivy.Core.Sync.TreeDiffer.ComputeDiff(binaryTreeSource, binaryTreeTarget);
            var serialized = MessagePackSerializer.Serialize(diff, _serializerOptions);
            ResultSize = serialized.Length;
            return serialized.Length;
        }

    }
}
