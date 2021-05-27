using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IExitedChannelEventExecutor : IEventExecutor<BaseEvent<ExitedChannelEvent>>
    {
        public Task Execute(BaseEvent<ExitedChannelEvent> e);

        Task IEventExecutor<BaseEvent<ExitedChannelEvent>>.ExecuteInner(BaseEvent<ExitedChannelEvent> e)
        {
            return Execute(e);
        }
    }
}
