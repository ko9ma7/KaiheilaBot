using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedChannelEventExecutor : IEventExecutor<BaseEvent<DeletedChannelEvent>>
    {
        public Task Execute(BaseEvent<DeletedChannelEvent> e);

        Task IEventExecutor<BaseEvent<DeletedChannelEvent>>.ExecuteInner(BaseEvent<DeletedChannelEvent> e)
        {
            return Execute(e);
        }
    }
}
