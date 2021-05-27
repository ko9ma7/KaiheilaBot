using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IMessageBtnClickEventExecutor : IEventExecutor<BaseEvent<MessageBtnClickEvent>>
    {
        public Task Execute(BaseEvent<MessageBtnClickEvent> e);

        Task IEventExecutor<BaseEvent<MessageBtnClickEvent>>.ExecuteInner(BaseEvent<MessageBtnClickEvent> e)
        {
            return Execute(e);
        }
    }
}
