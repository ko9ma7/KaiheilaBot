using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface ISelfJoinedGuildEventExecutor : IEventExecutor<BaseEvent<SelfJoinedGuildEvent>>
    {
        public Task Execute(BaseEvent<SelfJoinedGuildEvent> e);

        Task IEventExecutor<BaseEvent<SelfJoinedGuildEvent>>.ExecuteInner(BaseEvent<SelfJoinedGuildEvent> e)
        {
            return Execute(e);
        }
    }
}