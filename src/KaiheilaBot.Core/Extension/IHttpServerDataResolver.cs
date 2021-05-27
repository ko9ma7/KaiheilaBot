using System.Threading.Tasks;
using KaiheilaBot.Core.Models.Service;

namespace KaiheilaBot.Core.Extension
{
    public interface IHttpServerDataResolver
    {
        public Task Resolve(HttpServerData data);
    }
}
