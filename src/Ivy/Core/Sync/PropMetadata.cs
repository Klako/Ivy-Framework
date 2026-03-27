using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static Ivy.Core.Sync.WidgetMetadata;

namespace Ivy.Core.Sync
{
    internal record PropMetadata
    {
        public bool AlwaysSerialize { get; }

        public string CamelCaseName { get; }

        public object? DefaultValue { get; }

        private PropertyInfo propInfo;

        private PropAttribute attribute;

        public PropMetadata(PropertyInfo propInfo, PropAttribute attribute, object? defaultValue)
        {
            AlwaysSerialize = attribute.AlwaysSerialize;
            CamelCaseName = Utils.PascalCaseToCamelCase(propInfo.Name);
            DefaultValue = defaultValue;
            this.propInfo = propInfo;
            this.attribute = attribute;
        }

        public object? GetValue(IWidget widget)
        {
            if (attribute.IsAttached)
            {
                if (!propInfo.PropertyType.IsArray || !propInfo.PropertyType.GetElementType()!.IsGenericType)
                    throw new InvalidOperationException("Attached properties must be arrays of nullable types.");

                var children = widget.Children;
                var attachedValues = new object?[children.Length];
                var widgetType = widget.GetType();
                var attachedName = attribute.AttachedName!;

                for (int i = 0; i < children.Length; i++)
                {
                    if (children[i] is IWidget childWidget)
                    {
                        attachedValues[i] = childWidget.GetAttachedValue(widgetType, attachedName);
                    }
                }
                return attachedValues;
            }

            return propInfo.GetValue(widget);
        }
    }
}
