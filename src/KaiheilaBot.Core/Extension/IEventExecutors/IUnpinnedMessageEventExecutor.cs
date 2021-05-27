using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUnpinnedMessageEventExecutor : IEventExecutor<BaseEvent<UnpinnedMessageEvent>>
    {
        public Task Execute(BaseEvent<UnpinnedMessageEvent> e);

        Task IEventExecutor<BaseEvent<UnpinnedMessageEvent>>.ExecuteInner(BaseEvent<UnpinnedMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
