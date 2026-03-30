using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Ivy.Core.Sync
{
    internal class WidgetEqualityComparer : IEqualityComparer<IWidget>
    {
        public bool Equals(IWidget? x, IWidget? y)
        {
            if (x == null)
            {
                return y == null;
            }
            if (y == null)
            {
                return x == null;
            }

            if (!x.GetType().Equals(y.GetType()))
            {
                return false;
            }

            var metadata = WidgetMetadata.FromWidgetType(x.GetType());

            foreach (var (_, propMetadata) in metadata.PropMetadatas)
            {
                var xValue = propMetadata.GetValue(x);
                var yValue = propMetadata.GetValue(y);

                if (!StructuralComparisons.StructuralEqualityComparer.Equals(xValue, yValue))
                {
                    return false;
                }
            }

            var xEvents = metadata.GetEvents(x);
            var yEvents = metadata.GetEvents(y);

            if (!StructuralComparisons.StructuralEqualityComparer.Equals(xEvents, yEvents))
            {
                return false;
            }

            if (x.Children.Length != y.Children.Length)
            {
                return false;
            }

            for (int i = 0; i < x.Children.Length; i++)
            {
                if (x.Children[i] is IWidget xChild && y.Children[i] is IWidget yChild)
                {
                    if (Equals(xChild, yChild))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int GetHashCode([DisallowNull] IWidget obj)
        {
            throw new NotImplementedException();
        }
    }
}
