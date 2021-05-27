using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IImageMessageEventExecutor : IEventExecutor<BaseMessageEvent<ImageMessageEvent>>
    {
        public Task Execute(BaseMessageEvent<ImageMessageEvent> e);

        Task IEventExecutor<BaseMessageEvent<ImageMessageEvent>>.ExecuteInner(BaseMessageEvent<ImageMessageEvent> e)
        {
            return Execute(e);
        }
    }
}
