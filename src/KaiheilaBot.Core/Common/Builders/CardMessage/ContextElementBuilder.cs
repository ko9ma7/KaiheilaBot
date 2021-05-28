using System.Collections.Generic;
using KaiheilaBot.Core.Models.Objects.CardMessages;

namespace KaiheilaBot.Core.Common.Builders.CardMessage
{
    public class ContextElementBuilder
    {
        private readonly List<IContextElement> _contextElements = new();
        
        public IEnumerable<IContextElement> Build()
        {
            return _contextElements;
        }

        public ContextElementBuilder AddElement(IContextElement element)
        {
            _contextElements.Add(element);
            return this;
        }
    }
}
