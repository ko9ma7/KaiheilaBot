using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IPrivateAddedReactionEventExecutor : IEventExecutor<BaseEvent<PrivateAddedReactionEvent>>
    {
        public Task Execute(BaseEvent<PrivateAddedReactionEvent> e);

        Task IEventExecutor<BaseEvent<PrivateAddedReactionEvent>>.ExecuteInner(BaseEvent<PrivateAddedReactionEvent> e)
        {
            return Execute(e);
        }
    }
}
