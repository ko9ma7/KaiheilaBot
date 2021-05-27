using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUpdatedPrivateMessageEventExecutor : IEventExecutor<BaseEvent<UpdatedPrivateMessageEvent>>
    {
        public Task Execute(BaseEvent<UpdatedPrivateMessageEvent> e);

        Task IEventExecutor<BaseEvent<UpdatedPrivateMessageEvent>>.ExecuteInner(BaseEvent<UpdatedPrivateMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
