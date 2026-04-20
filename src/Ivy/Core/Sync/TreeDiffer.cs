using System.Reflection;

namespace Ivy.Core.Sync
{
    public class TreeDiffer
    {
        public static object? ComputeDiff(WidgetNode source, WidgetNode target)
        {
            if (!target.Type.Equals(source.Type))
            {
                return target;
            }

            string? id = null;

            if (source.Id != target.Id)
            {
                id = target.Id;
            }

            // Widgets are the same type
            var metadata = WidgetMetadata.FromWidgetType(source.GetType());

            // Compare props

            var propUpdates = new Dictionary<string, IPropUpdate>();

            foreach (var ((name, sourceValue), (_, targetValue)) in source.Props.Zip(target.Props))
            {
#if NEWDIFF_PROPDIFF
                var propUpdate = PropDiff(sourceValue, targetValue);
#else
                var propUpdate = sourceValue.DeepEquals(targetValue) ? null : new PropValueDiff(targetValue);
#endif

                if (propUpdate != null)
                {
                    propUpdates.Add(name, propUpdate);
                }
            }

            // Compare events

            string[]? eventsUpdate = null;

            if (!source.Events.SequenceEqual(target.Events))
            {
                eventsUpdate = target.Events.ToArray();
            }

            // Compare children
#if NEWDIFF_LCS
            var childrenChanges = ChildrenLCSDiff(source.Children, target.Children);
#else
            var childrenChanges = ChildrenLinearDiff(source.Children, target.Children);
#endif

            if (id != null
                || propUpdates.Count > 0
                || eventsUpdate != null
                || childrenChanges != null)
            {
                return new WidgetUpdate(
                    id: id,
                    props: propUpdates.Count > 0 ? propUpdates : null,
                    events: eventsUpdate,
                    children: childrenChanges);
            }

            return null;
        }

        private static IPropUpdate? PropDiff(IPropStructureNode? source, IPropStructureNode? target)
        {
            if (source == null && target == null)
            {
                return null;
            }

            if (source == null || target == null)
            {
                return new PropValueDiff(new PropStructureLeaf(target));
            }

            if (source is PropStructureObject sourceMap && target is PropStructureObject targetMap)
            {
                var maxLength = Math.Max(sourceMap.Count, targetMap.Count);
                Dictionary<string, IPropObjectOperation> changes = new(maxLength);

                var sourceKeys = sourceMap.Select(entry => entry.Key);
                var targetKeys = targetMap.Select(entry => entry.Key);

                foreach (var key in sourceKeys.Union(targetKeys))
                {
                    var sourceHasKey = sourceMap.TryGetValue(key, out var sourceValue);
                    var targetHasKey = targetMap.TryGetValue(key, out var targetValue);

                    if (sourceHasKey && targetHasKey)
                    {
                        var valueDiff = PropDiff(sourceValue!, targetValue!);
                        if (valueDiff != null)
                        {
                            changes.Add(key, new PropObjectUpdate(valueDiff));
                        }
                    }
                    else if (!targetHasKey)
                    {
                        changes.Add(key, new PropObjectRemove());
                    }
                    else if (!sourceHasKey)
                    {
                        changes.Add(key, new PropObjectSet(targetValue!));
                    }
                }

                if (changes.Count == 0)
                {
                    return null;
                }

                return new PropObjectDiff(changes);
            }
            else if (source is PropStructureList sourceList && target is PropStructureList targetList)
            {
                var sourceLength = sourceList.Count;
                var targetLength = targetList.Count;

                var commonLength = Math.Min(sourceLength, targetLength);
                List<(int, IPropUpdate)> changes = new(commonLength);

                for (int i = 0; i < commonLength; i++)
                {
                    var sourceElement = sourceList[i];
                    var targetElement = targetList[i];

                    var diff = PropDiff(sourceElement, targetElement);
                    if (diff != null)
                    {
                        changes.Add((i, diff));
                    }
                }

                List<IPropStructureNode> appends = new();

                if (targetLength > sourceLength)
                {
                    for (int i = commonLength; i < targetLength; i++)
                    {
                        appends.Add(targetList[i]);
                    }
                }

                var removals = Math.Max(0, sourceLength - targetLength);

                if (changes.Count == 0 && appends.Count == 0 && removals == 0)
                {
                    return null;
                }

                return new PropArrayDiff(changes, appends!, Math.Max(0, sourceLength - targetLength));
            }

            if (source.DeepEquals(target))
            {
                return null;
            }

            return new PropValueDiff(target);
        }

        private static WidgetListDiff? ChildrenLinearDiff(WidgetNode[] source, WidgetNode[] target)
        {
            var changes = new List<IWidgetListOperation>(Math.Max(source.Length, target.Length));

            var commonLength = Math.Min(source.Length, target.Length);

            // Both widgets have a child at index i
            for (int i = 0; i < commonLength; i++)
            {
                var result = ComputeDiff(source[i], target[i]);

                if (result is WidgetNode widget)
                {
                    changes.Add(WidgetListSplice.Replace(i, widget));
                }
                else if (result is WidgetUpdate update)
                {
                    changes.Add(new WidgetListUpdate(i, update));
                }
            }

            if (source.Length > target.Length)
            {
                var diffLength = source.Length - target.Length;
                changes.Add(WidgetListSplice.RemoveRange(commonLength, diffLength));
            }
            else if (source.Length < target.Length)
            {
                var widgetsToAdd = target.TakeLast(target.Length - source.Length);
                changes.Add(WidgetListSplice.AddRange(source.Length, widgetsToAdd));
            }

            if (changes.Count == 0)
            {
                return null;
            }

            return new WidgetListDiff(changes: changes);
        }

        private static WidgetListDiff? ChildrenLCSDiff(WidgetNode[] source, WidgetNode[] target)
        {
            // Compute the matrix containing all jumps in distances
            // used to compute the subsequence itself
            var distances = new int[source.Length + 1, target.Length + 1];
            {
                for (int i = 0; i < source.Length; i++)
                {
                    distances[i, 0] = 0;
                }
                for (int i = 0; i < target.Length; i++)
                {
                    distances[0, i] = 0;
                }
                var equalityComparer = new WidgetNodeEqualityComparer();
                for (int i = 1; i < source.Length + 1; i++)
                {
                    for (int j = 1; j < target.Length + 1; j++)
                    {
                        if (equalityComparer.Equals(source[i - 1], target[j - 1]))
                        {
                            distances[i, j] = distances[i - 1, j - 1] + 1;
                        }
                        else
                        {
                            distances[i, j] = Math.Max(distances[i, j - 1], distances[i - 1, j]);
                        }
                    }
                }
            }

            // Compute the subsequence of pairs of widgets that match
            var subsequencePairs = new List<(int SourceIndex, int TargetIndex)>(Math.Max(source.Length, target.Length));
            {
                var sourceIndex = source.Length;
                var targetIndex = target.Length;
                while (distances[sourceIndex, targetIndex] > 0)
                {
                    var pairDistance = distances[sourceIndex, targetIndex];
                    if (distances[sourceIndex - 1, targetIndex] == pairDistance)
                    {
                        sourceIndex--;
                    }
                    else if (distances[sourceIndex, targetIndex - 1] == pairDistance)
                    {
                        targetIndex--;
                    }
                    else
                    {
                        subsequencePairs.Add((sourceIndex, targetIndex));
                        sourceIndex--;
                        targetIndex--;
                    }
                }
                subsequencePairs.Reverse();
            }

            // Compute the changes such that the matching widgets stay and the rest are edited.
            var changes = new List<IWidgetListOperation>();
            {
                var adjacentPairs = subsequencePairs
                    .Append((SourceIndex: source.Length, TargetIndex: target.Length))
                    .Zip(subsequencePairs.Prepend((SourceIndex: 0, TargetIndex: 0)));

                foreach (var (currentPair, previousPair) in adjacentPairs)
                {
                    var sourceLength = currentPair.SourceIndex - previousPair.SourceIndex;
                    var targetLength = currentPair.TargetIndex - previousPair.TargetIndex;
                    var minLength = Math.Min(sourceLength, targetLength);

                    for (int i = 0; i < minLength; i++)
                    {
                        var sourceIndex = previousPair.SourceIndex + i;
                        var targetIndex = previousPair.TargetIndex + i;
                        var widgetDiff = ComputeDiff(source[sourceIndex], target[targetIndex]);
                        if (widgetDiff is WidgetUpdate update)
                        {
                            changes.Add(new WidgetListUpdate(sourceIndex, update));
                        }
                        else if (widgetDiff is WidgetNode newWidget)
                        {
                            if (changes.LastOrDefault() is WidgetListSplice previousSplice
                                && previousSplice.Index == sourceIndex - 1)
                            {
                                changes.RemoveAt(changes.Count - 1);
                                changes.Add(WidgetListSplice.ReplaceRange(
                                    previousSplice.Index,
                                    previousSplice.Length + 1,
                                    previousSplice.Widgets.Append(newWidget)));
                            }
                            else
                            {
                                changes.Add(WidgetListSplice.Replace(sourceIndex, newWidget));
                            }
                        }
                    }

                    if (sourceLength > targetLength)
                    {
                        changes.Add(WidgetListSplice.RemoveRange(
                            previousPair.SourceIndex + targetLength,
                            sourceLength - targetLength));
                    }

                    if (sourceLength < targetLength)
                    {
                        changes.Add(WidgetListSplice.AddRange(
                            previousPair.SourceIndex + sourceLength,
                            target.TakeLast(targetLength - sourceLength)));
                    }
                }
            }

            if (changes.Count == 0)
            {
                return null;
            }

            return new WidgetListDiff(changes: changes);
        }
    }
}
