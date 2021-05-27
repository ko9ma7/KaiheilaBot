using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IAddedReactionEventExecutor : IEventExecutor<BaseEvent<AddedReactionEvent>>
    {
        public Task Execute(BaseEvent<AddedReactionEvent> e);

        Task IEventExecutor<BaseEvent<AddedReactionEvent>>.ExecuteInner(BaseEvent<AddedReactionEvent> e)
        {
            return Execute(e);
        }
    }
}
