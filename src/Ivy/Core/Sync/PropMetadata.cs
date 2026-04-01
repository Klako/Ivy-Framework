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

        public PropAttribute Attribute { get; }

        private PropertyInfo propInfo;

        public PropMetadata(PropertyInfo propInfo, PropAttribute attribute, object? defaultValue)
        {
            AlwaysSerialize = attribute.AlwaysSerialize;
            CamelCaseName = Utils.PascalCaseToCamelCase(propInfo.Name);
            DefaultValue = defaultValue;
            Attribute = attribute;
            this.propInfo = propInfo;
        }

        public object? GetValue(IWidget widget)
        {
            if (Attribute.IsAttached)
            {
                if (!propInfo.PropertyType.IsArray || !propInfo.PropertyType.GetElementType()!.IsGenericType)
                    throw new InvalidOperationException("Attached properties must be arrays of nullable types.");

                var children = widget.Children;
                var attachedValues = new object?[children.Length];
                var widgetType = widget.GetType();
                var attachedName = Attribute.AttachedName!;

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
