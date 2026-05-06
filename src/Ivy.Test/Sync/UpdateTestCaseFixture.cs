using Ivy.Core.Sync;
using MessagePack;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Test.Sync
{
    [MessagePackObject(keyAsPropertyName: true)]
    public record UpdateTestCase(WidgetNode Source, WidgetNode Target, List<(string Algorithm,WidgetUpdate Update)> Updates);

    public class UpdateTestCaseFixture : IDisposable
    {
        private static readonly ConcurrentBag<UpdateTestCase> _allTestCases = new();
        public ConcurrentBag<UpdateTestCase> TestCases { get; } = _allTestCases;

        public void Dispose()
        {
#pragma warning disable 0162
            if (true)
            {
                FileStream fs = File.Open("tests.msgpack", FileMode.Truncate);
                MessagePackSerializer.Serialize(fs, TestCases.ToList());
                fs.Close();
            }
#pragma warning restore 0162
        }
    }

    [CollectionDefinition("TestCasesAccumulator")]
    public class ResultCollection : ICollectionFixture<UpdateTestCaseFixture>
    {

    }
}
