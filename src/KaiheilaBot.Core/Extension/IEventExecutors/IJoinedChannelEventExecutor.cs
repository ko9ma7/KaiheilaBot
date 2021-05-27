using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IJoinedChannelEventExecutor : IEventExecutor<BaseEvent<JoinedChannelEvent>>
    {
        public Task Execute(BaseEvent<JoinedChannelEvent> e);

        Task IEventExecutor<BaseEvent<JoinedChannelEvent>>.ExecuteInner(BaseEvent<JoinedChannelEvent> e)
        {
            return Execute(e);
        }
    }
}
