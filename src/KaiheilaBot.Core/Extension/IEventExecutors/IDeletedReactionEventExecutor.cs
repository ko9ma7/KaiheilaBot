using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedReactionEventExecutor : IEventExecutor<BaseEvent<DeletedReactionEvent>>
    {
        public Task Execute(BaseEvent<DeletedReactionEvent> e);

        Task IEventExecutor<BaseEvent<DeletedReactionEvent>>.ExecuteInner(BaseEvent<DeletedReactionEvent> e)
        {
            return Execute(e);
        }
    }
}
