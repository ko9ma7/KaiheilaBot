using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IAddedChannelEventExecutor : IEventExecutor<BaseEvent<AddedChannelEvent>>
    {
        public Task Execute(BaseEvent<AddedChannelEvent> e);

        Task IEventExecutor<BaseEvent<AddedChannelEvent>>.ExecuteInner(BaseEvent<AddedChannelEvent> e)
        {
            return Execute(e);
        }
    }
}
