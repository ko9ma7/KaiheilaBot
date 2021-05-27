using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedMessageEventExecutor : IEventExecutor<BaseEvent<DeletedMessageEvent>>
    {
        public Task Execute(BaseEvent<DeletedMessageEvent> e);

        Task IEventExecutor<BaseEvent<DeletedMessageEvent>>.ExecuteInner(BaseEvent<DeletedMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
