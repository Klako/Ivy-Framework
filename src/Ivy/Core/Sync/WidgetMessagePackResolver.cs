using Ivy.Core.Server.Formatters;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Core.Sync
{
    internal class WidgetMessagePackResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new WidgetMessagePackResolver();

        public IMessagePackFormatter<T>? GetFormatter<T>()
        {
            if (typeof(T).IsAssignableTo(typeof(IWidget)))
            {
                return (IMessagePackFormatter<T>)new WidgetMessagePackFormatter();
            }
            return null;
        }
    }
}
