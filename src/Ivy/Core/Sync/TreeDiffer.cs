using OpenAI.Chat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ivy.Core.Sync
{
    internal class TreeDiffer
    {
        public static object? ComputeDiff(IWidget source, IWidget target)
        {
            if (!target.GetType().Equals(source.GetType()))
            {
                return target;
            }

            string? id = null;

            if (id != target.Id)
            {
                id = target.Id;
            }

            // Widgets are the same type
            var metadata = WidgetMetadata.FromWidgetType(source.GetType());

            // Compare props

            var propUpdates = new Dictionary<string, object?>();

            foreach (var (name, propMetadata) in metadata.PropMetadatas)
            {
                var sourceValue = propMetadata.GetValue(source);
                var targetValue = propMetadata.GetValue(target);

                if (!StructuralComparisons.StructuralEqualityComparer.Equals(sourceValue, targetValue))
                {
                    propUpdates.Add(name, targetValue);
                }
            }

            // Compare events

            string[]? eventsUpdate = null;

            var sourceEvents = metadata.GetEvents(source);
            var targetEvents = metadata.GetEvents(target);

            if (!StructuralComparisons.StructuralEqualityComparer.Equals(sourceEvents, targetEvents))
            {
                eventsUpdate = targetEvents.ToArray();
            }

            // Compare children

            var childrenChanges = ChildrenNaiveDiff(source, target);

            if (id != null
                || propUpdates.Count > 0
                || eventsUpdate != null
                || childrenChanges.Changes != null && childrenChanges.Changes.Length > 0)
            {
                return new WidgetUpdate(
                    id: id,
                    props: propUpdates,
                    events: eventsUpdate,
                    children: childrenChanges);
            }

            return null;
        }

        private static WidgetListDiff ChildrenNaiveDiff(IWidget source, IWidget target)
        {
            var changes = new List<IWidgetListOperation>();

            var commonLength = Math.Min(source.Children.Length, target.Children.Length);

            // Both widgets have a child at index i
            for (int i = 0; i < commonLength; i++)
            {
                var sourceChild = (IWidget)source.Children[i];
                var targetChild = (IWidget)target.Children[i];
                var result = ComputeDiff(sourceChild, targetChild);

                if (result is IWidget widget)
                {
                    changes.Add(new WidgetListReplace(i, widget));
                } else if (result is WidgetUpdate update)
                {
                    changes.Add(new WidgetListUpdate(i, update));
                }
            }

            if (source.Children.Length > target.Children.Length)
            {
                changes.Add(new WidgetListReplaceRange(commonLength, source.Children.Length, null));
            } else if (source.Children.Length < target.Children.Length)
            {
                var widgetsToAdd = target.Children
                    .TakeLast(target.Children.Length - source.Children.Length)
                    .Cast<IWidget>()
                    .ToArray();
                changes.Add(new WidgetListAddRange(source.Children.Length, widgetsToAdd));
            }

            return new WidgetListDiff(changes: changes.ToArray());
        }

        private static WidgetListDiff ChildrenLCSDiff(IWidget source, IWidget target)
        {
            var childrenChanges = new List<IWidgetListOperation>();

            if (source.Children.Length == 0)
            {
                childrenChanges.Add(new WidgetListAddRange(0, (IWidget[])target.Children));
            } else if (target.Children.Length == 0)
            {
                childrenChanges.Add(new WidgetListReplaceRange(0, source.Children.Length - 1, null));
            } else
            {
                var distances = new int[source.Children.Length + 1, target.Children.Length + 1];
                for (int i = 0; i < source.Children.Length; i++)
                {
                    distances[i, 0] = 0;
                }
                for (int i = 0; i < target.Children.Length; i++)
                {
                    distances[0, i] = 0;
                }
                var equalityComparer = new WidgetEqualityComparer();
                for (int i = 1; i < source.Children.Length + 1; i++)
                {
                    for (int j = 1; j < target.Children.Length + 1; j++)
                    {
                        if (equalityComparer.Equals((IWidget)source.Children[i - 1], (IWidget)target.Children[j - 1]))
                        {
                            distances[i, j] = distances[i - 1, j - 1] + 1;
                        } else
                        {
                            distances[i, j] = Math.Max(distances[i, j - 1], distances[i - 1, j]);
                        }
                    }
                }
                var subsequenceLength = distances[source.Children.Length - 1, target.Children.Length - 1];

            }

            return new WidgetListDiff(changes: childrenChanges.ToArray());
        }
    }
}
