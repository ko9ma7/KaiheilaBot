using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedChannelEventExecutor : IEventExecutor<BaseEvent<UpdatedChannelEvent>>
    {
        public Task Execute(BaseEvent<UpdatedChannelEvent> e);

        Task IEventExecutor<BaseEvent<UpdatedChannelEvent>>.ExecuteInner(BaseEvent<UpdatedChannelEvent> e)
        {
            return Execute(e);
        }
    }
}
