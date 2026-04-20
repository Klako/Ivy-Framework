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
using Ivy.NativeJsonDiff;

namespace Ivy.Benchmark.Sync
{
    // For more information on the VS BenchmarkDotNet Diagnosers see https://learn.microsoft.com/visualstudio/profiling/profiling-with-benchmark-dotnet
    [CPUUsageDiagnoser]
    public partial class WidgetTreeSync
    {
        WidgetNode flatTreeSourceNode;
        WidgetNode flatTreeTargetNode;

        WidgetNode binaryTreeSourceNode;
        WidgetNode binaryTreeTargetNode;

        byte[] binaryTreeoOldBytes;
        byte[] binaryTreeNewBytes;

        byte[] flatTreeOldBytes;
        byte[] flatTreeNewBytes;

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
            var flatTreeSource = new List(GenerateTexts(1000)) { Id = "pokqwd" };
            var flatTreeTarget = new List(GenerateTexts(1000)) { Id = "pokqwd" };
            var binaryTreeSource = GenerateBinaryTree(new Random(), (int)Math.Log2(1000));
            binaryTreeSource.Id = "dwqpokqwd";
            var binaryTreeTarget = GenerateBinaryTree(new Random(), (int)Math.Log2(1000));
            binaryTreeTarget.Id = "dwqpokqwd";

            flatTreeSourceNode = new WidgetNode(flatTreeSource);
            flatTreeTargetNode = new WidgetNode(flatTreeTarget);

            binaryTreeSourceNode = new WidgetNode(binaryTreeSource);
            binaryTreeTargetNode = new WidgetNode(binaryTreeTarget);

            var binarySourceJson = binaryTreeSource.Serialize();
            var binaryTargetJson = binaryTreeTarget.Serialize();
            binaryTreeoOldBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(binarySourceJson);
            binaryTreeNewBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(binaryTargetJson);

            var flatSourceJson = flatTreeSource.Serialize();
            var flatTargetJson = flatTreeTarget.Serialize();
            flatTreeOldBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(flatSourceJson);
            flatTreeNewBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(flatTargetJson);
        }

        //[ReportColumn(Aggregation = Aggregation.Mean, Name = "Size", Unit = "bytes")]
        //public int ResultSize { get; set; }

        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public object RustDiff_FlatTree()
        {
            return JsonDiffer.ComputePatch(flatTreeOldBytes, flatTreeNewBytes);
        }

        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public object NewDiff_FlatTree()
        {
            return TreeDiffer.ComputeDiff(flatTreeSourceNode, flatTreeTargetNode);
        }


        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public object RustDiff_BinaryTree()
        {
            return JsonDiffer.ComputePatch(binaryTreeoOldBytes, binaryTreeNewBytes);
        }

        [Benchmark]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public object NewDiff_BinaryTree()
        {
            return TreeDiffer.ComputeDiff(binaryTreeSourceNode, binaryTreeTargetNode);
        }

    }
}
