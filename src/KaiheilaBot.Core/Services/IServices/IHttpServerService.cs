using System.Threading.Tasks;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IHttpServerService
    {
        public Task Start();

        public Task Stop();
    }
}