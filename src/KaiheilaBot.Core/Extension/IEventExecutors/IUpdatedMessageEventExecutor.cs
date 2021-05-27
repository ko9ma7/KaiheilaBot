using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.ChannelRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedMessageEventExecutor : IEventExecutor<BaseEvent<UpdatedMessageEvent>>
    {
        public Task Execute(BaseEvent<UpdatedMessageEvent> e);

        Task IEventExecutor<BaseEvent<UpdatedMessageEvent>>.ExecuteInner(BaseEvent<UpdatedMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
