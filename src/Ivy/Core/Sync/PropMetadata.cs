using Ivy.Core.Helpers;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Ivy.Core.Sync
{
    internal record PropMetadata
    {
        public bool AlwaysSerialize { get; }

        public string CamelCaseName { get; }

        public object? DefaultValue { get; }

        public JsonNode? DefaultJsonValue { get; }

        public PropAttribute Attribute { get; }

        private PropertyInfo propInfo;

        private Func<IWidget, object> getter;

        public PropMetadata(PropertyInfo propInfo, PropAttribute attribute, object? defaultValue)
        {
            AlwaysSerialize = attribute.AlwaysSerialize;
            CamelCaseName = Utils.PascalCaseToCamelCase(propInfo.Name);
            DefaultValue = defaultValue;
            DefaultJsonValue = JsonSerializer.SerializeToNode(defaultValue, _serializerOptions);
            Attribute = attribute;
            this.propInfo = propInfo;

            var targetType = propInfo.DeclaringType!;
            var exInstance = Expression.Parameter(typeof(IWidget), "t");
            var exConvertToType = Expression.Convert(exInstance, targetType);
            var exMemberAccess = Expression.MakeMemberAccess(exConvertToType, propInfo);       // t.PropertyName
            var exConvertToObject = Expression.Convert(exMemberAccess, typeof(object));     // Convert(t.PropertyName, typeof(object))
            var lambda = Expression.Lambda<Func<IWidget, object>>(exConvertToObject, exInstance);
            getter = lambda.Compile();
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

            return getter(widget);
        }

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonEnumConverter(),
                new ValueTupleConverterFactory()
            }
        };

        public JsonNode? GetValueAsJson(IWidget widget)
        {
            return JsonSerializer.SerializeToNode(GetValue(widget), _serializerOptions);
        }
    }
}
