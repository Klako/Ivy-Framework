using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Core.Sync
{
    internal class TreeDiffer
    {
        public static object Calculate(IWidget source, IWidget target)
        {
            if (!target.GetType().Equals(source.GetType()))
            {
                return target;
            }

            string id = source.Id!;

            if (id != target.Id)
            {
                id = target.Id!;
            }

            return null!;
        }
    }
}
