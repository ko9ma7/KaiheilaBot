using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.PrivateMessageEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IDeletedPrivateMessageEventExecutor : IEventExecutor<BaseEvent<DeletedPrivateMessageEvent>>
    {
        public Task Execute(BaseEvent<DeletedPrivateMessageEvent> e);

        Task IEventExecutor<BaseEvent<DeletedPrivateMessageEvent>>.ExecuteInner(BaseEvent<DeletedPrivateMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
