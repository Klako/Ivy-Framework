using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Core.Sync
{
    public enum TreeChildrenDiffer
    {
        Linear,
        LCS
    }

    public record TreeDifferOptions(
        TreeChildrenDiffer ChildrenDiffer = TreeChildrenDiffer.Linear,
        bool PropDiff = false);
}
