using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface ICardMessageEventExecutor : IEventExecutor<BaseMessageEvent<CardMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<CardMessageEvent> e);

        Task IEventExecutor<BaseMessageEvent<CardMessageEvent>>.ExecuteInner(BaseMessageEvent<CardMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
