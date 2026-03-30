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
        public static object Calculate(IWidget source, IWidget target)
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

            var childrenChanges = new List<IWidgetListOperation>();

            

            return new WidgetUpdate(
                id: id,
                props: propUpdates,
                events: eventsUpdate,
                children: new WidgetListDiff(changes: childrenChanges.ToArray()));
        }
    }
}
