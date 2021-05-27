using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface ITextMessageEventExecutor : IEventExecutor<BaseMessageEvent<TextMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<TextMessageEvent> e);

        Task IEventExecutor<BaseMessageEvent<TextMessageEvent>>.ExecuteInner(BaseMessageEvent<TextMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
