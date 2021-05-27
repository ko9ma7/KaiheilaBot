using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IPinnedMessageEventExecutor : IEventExecutor<BaseEvent<PinnedMessageEvent>>
    {
        public Task Execute(BaseEvent<PinnedMessageEvent> e);

        Task IEventExecutor<BaseEvent<PinnedMessageEvent>>.ExecuteInner(BaseEvent<PinnedMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
