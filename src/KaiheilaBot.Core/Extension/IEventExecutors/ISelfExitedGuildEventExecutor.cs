using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface ISelfExitedGuildEventExecutor : IEventExecutor<BaseEvent<SelfExitedGuildEvent>>
    {
        public Task Execute(BaseEvent<SelfExitedGuildEvent> e);

        Task IEventExecutor<BaseEvent<SelfExitedGuildEvent>>.ExecuteInner(BaseEvent<SelfExitedGuildEvent> e)
        {
            return Execute(e);
        }
    }
}
