using Ivy.Core;
using Ivy.Core.Sync;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.Test.Sync
{
    public class WidgetUpdateTests
    {
        private IWidget[] GenerateTexts(int n)
        {
            var widgets = new IWidget[n];
            string[] texts = [
                "Five foxes four fairies",
                "Lorem ipsum dolor sit amet",
                "Aliquam augue massa",
                "Vivamus lobortis diam id nulla mattis"
            ];
            var rand = new Random();
            for (int i = 0; i < n; i++)
            {
                widgets[i] = (IWidget)Text.H3(texts[rand.Next(texts.Length)]).Build()!;
                widgets[i].Id = i.ToString();
            }
            return widgets;
        }

        private static MessagePackSerializerOptions _serializerOptions =
            new MessagePackSerializerOptions(CompositeResolver.Create([new Core.Sync.WidgetMessagePackFormatter()], [StandardResolver.Instance]));

        [Fact]
        public void WidgetUpdate_SerializeResult()
        {
            var source = new List(GenerateTexts(100)) { Id = "pokqwd" };
            var target = new List(GenerateTexts(100)) { Id = "pokqwd" };
            var update = TreeDiffer.ComputeDiff(source.ToWidgetNode(), target.ToWidgetNode());
            var serialized = MessagePackSerializer.Serialize(update, SerializedWidget.SerializeOptions);
        }
    }
}
