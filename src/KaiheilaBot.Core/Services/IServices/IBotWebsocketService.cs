using System.Threading.Tasks;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IBotWebsocketService
    {
        public Task<int> Connect();
    }
}