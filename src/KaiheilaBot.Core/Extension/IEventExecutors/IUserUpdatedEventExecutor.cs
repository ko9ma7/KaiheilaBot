using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IUserUpdatedEventExecutor : IEventExecutor<BaseEvent<UserUpdatedEvent>>
    {
        public Task Execute(BaseEvent<UserUpdatedEvent> e);

        Task IEventExecutor<BaseEvent<UserUpdatedEvent>>.ExecuteInner(BaseEvent<UserUpdatedEvent> e)
        {
            return Execute(e);
        }
    }
}
