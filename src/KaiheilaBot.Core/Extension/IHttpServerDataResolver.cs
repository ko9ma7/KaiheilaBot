using System.Threading.Tasks;
using KaiheilaBot.Core.Models.Service;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Extension
{
    public interface IHttpServerDataResolver
    {
        public Task Resolve(HttpServerData data, ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService);
    }
}
