using Ivy.Core.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Core
{
    public enum TreeDifferType
    {
        JsonPatch,
        New
    }
    public record WidgetTreeOptions(
        bool Benchmark,
        TreeDifferType DiffType, 
        TreeDifferOptions? NewDiffOptions,
        Func<double, double, double, int>? ReportTimeCallback);
}
