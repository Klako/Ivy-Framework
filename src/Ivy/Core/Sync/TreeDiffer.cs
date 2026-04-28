using System.Reflection;

namespace Ivy.Core.Sync
{
    public class TreeDiffer(TreeDifferOptions options)
    {
        public object? ComputeDiff(WidgetNode source, WidgetNode target)
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

            // Compare props

            var propUpdates = new Dictionary<string, IPropUpdate>();

            foreach (var ((name, sourceValue), (_, targetValue)) in source.Props.Zip(target.Props))
            {
                var propUpdate = options.PropDiff switch
                {
                    true => PropDiff(sourceValue, targetValue),
                    false => (sourceValue.DeepEquals(targetValue) ? null : new PropValueDiff(targetValue))
                };

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
            var childrenChanges = options.ChildrenDiffer switch
            {
                TreeChildrenDiffer.Linear => ChildrenLinearDiff(source.Children, target.Children),
                TreeChildrenDiffer.LCS => ChildrenLCSDiff(source.Children, target.Children),
                _ => throw new ArgumentException("Invalid child differ option")
            };

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

        private IPropUpdate? PropDiff(IPropStructureNode? source, IPropStructureNode? target)
        {
            if (source == null && target == null)
            {
                return null;
            }

            if (source == null || target == null)
            {
                return new PropValueDiff(new PropStructureLeaf(target));
            }

            if (source is PropStructureObject sourceObj && target is PropStructureObject targetObj)
            {
                var sourceMembers = sourceObj.Members;
                var targetMembers = targetObj.Members;

                var maxLength = Math.Max(sourceMembers.Count, targetMembers.Count);
                Dictionary<string, IPropObjectOperation> changes = new(maxLength);

                var sourceKeys = sourceMembers.Select(entry => entry.Key);
                var targetKeys = targetMembers.Select(entry => entry.Key);

                foreach (var key in sourceKeys.Union(targetKeys))
                {
                    var sourceHasKey = sourceMembers.TryGetValue(key, out var sourceValue);
                    var targetHasKey = targetMembers.TryGetValue(key, out var targetValue);

                    if (sourceHasKey && targetHasKey)
                    {
                        var valueDiff = PropDiff(sourceValue, targetValue);
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
                var sourceMembers = sourceList.Members;
                var targetMembers = targetList.Members;

                var sourceLength = sourceMembers.Count;
                var targetLength = targetMembers.Count;

                var commonLength = Math.Min(sourceLength, targetLength);
                List<(int, IPropUpdate)> changes = new(commonLength);

                for (int i = 0; i < commonLength; i++)
                {
                    var sourceElement = sourceMembers[i];
                    var targetElement = targetMembers[i];

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
                        appends.Add(targetMembers[i]);
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
        
        private WidgetListDiff? ChildrenLinearDiff(WidgetNode[] source, WidgetNode[] target)
        {
            var changes = new List<IWidgetListOperation>(Math.Max(source.Length, target.Length));

            var commonLength = Math.Min(source.Length, target.Length);

            // Both widgets have a child at index i
            for (int i = 0; i < commonLength; i++)
            {
                var result = ComputeDiff(source[i], target[i]);

                if (result is WidgetNode widget)
                {
                    if (changes.LastOrDefault() is WidgetListSplice previousSplice
                        && previousSplice.Index == i - 1)
                    {
                        changes.RemoveAt(changes.Count - 1);
                        changes.Add(WidgetListSplice.ReplaceRange(
                            previousSplice.Index,
                            previousSplice.Length + 1,
                            previousSplice.Widgets.Append(widget)));
                    }
                    else
                    {
                        changes.Add(WidgetListSplice.Replace(i, widget));
                    }
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
                changes.Add(WidgetListSplice.InsertRange(source.Length, widgetsToAdd));
            }

            if (changes.Count == 0)
            {
                return null;
            }

            return new WidgetListDiff(changes: changes);
        }

        private WidgetListDiff? ChildrenLCSDiff(WidgetNode[] source, WidgetNode[] target)
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
                // We say 'nth' rather than index because we are
                // looking at distances[1.., 1..]
                // Row and column 0 are baselines for the algorithm
                var sourceNth = source.Length;
                var targetNth = target.Length;
                while (distances[sourceNth, targetNth] > 0)
                {
                    var pairDistance = distances[sourceNth, targetNth];
                    if (distances[sourceNth - 1, targetNth] == pairDistance)
                    {
                        sourceNth--;
                    }
                    else if (distances[sourceNth, targetNth - 1] == pairDistance)
                    {
                        targetNth--;
                    }
                    else
                    {
                        subsequencePairs.Add((sourceNth - 1, targetNth - 1));
                        sourceNth--;
                        targetNth--;
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
                        var insertIndex = previousPair.SourceIndex + sourceLength;
                        var insertWidgets = target
                            .Skip(previousPair.TargetIndex + sourceLength)
                            .Take(targetLength - sourceLength);
                        changes.Add(WidgetListSplice.InsertRange(insertIndex, insertWidgets));
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
