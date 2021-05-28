using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension
{
    public interface IEventExecutor<in T> where T : class
    {
        Task ExecuteInner(T e, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);
    }
}
