using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IPrivateDeletedReactionEventExecutor : IEventExecutor<BaseEvent<PrivateDeletedReactionEvent>>
    {
        public Task Execute(BaseEvent<PrivateDeletedReactionEvent> e);

        Task IEventExecutor<BaseEvent<PrivateDeletedReactionEvent>>.ExecuteInner(BaseEvent<PrivateDeletedReactionEvent> e)
        {
            return Execute(e);
        }
    }
}
