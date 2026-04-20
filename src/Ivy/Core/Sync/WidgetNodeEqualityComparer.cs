using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Ivy.Core.Sync
{
    internal class WidgetNodeEqualityComparer : IEqualityComparer<WidgetNode>
    {
        public bool Equals(WidgetNode? x, WidgetNode? y)
        {
            if (x == null)
            {
                return y == null;
            }
            if (y == null)
            {
                return x == null;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (!x.Type.Equals(y.Type))
            {
                return false;
            }

            if (x.Id != y.Id)
            {
                return false;
            }

            var metadata = WidgetMetadata.FromWidgetType(x.Type);

            foreach (var ((_, xValue), (_, yValue)) in x.Props.Zip(y.Props))
            {
                if (!xValue.DeepEquals(yValue))
                {
                    return false;
                }
            }

            if (!x.Events.SequenceEqual(y.Events))
            {
                return false;
            }

            if (x.Children.Length != y.Children.Length)
            {
                return false;
            }

            for (int i = 0; i < x.Children.Length; i++)
            {
                if (!Equals(x.Children[i], y.Children[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode([DisallowNull] WidgetNode obj)
        {
            throw new NotImplementedException();
        }
    }
}
