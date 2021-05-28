using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.UserRelatedEvents;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension.IEventExecutors
{
    public interface IMessageBtnClickEventExecutor : IEventExecutor<BaseEvent<MessageBtnClickEvent>>
    {
        public Task Execute(BaseEvent<MessageBtnClickEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);

        Task IEventExecutor<BaseEvent<MessageBtnClickEvent>>.ExecuteInner(BaseEvent<MessageBtnClickEvent> e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            return Execute(e, logger, httpApiRequestService);
        }
    }
}
