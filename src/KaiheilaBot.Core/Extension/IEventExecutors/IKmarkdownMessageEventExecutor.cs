using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IKmarkdownMessageEventExecutor : IEventExecutor<BaseMessageEvent<KmarkdownMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<KmarkdownMessageEvent> e);

        Task IEventExecutor<BaseMessageEvent<KmarkdownMessageEvent>>.ExecuteInner(BaseMessageEvent<KmarkdownMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
