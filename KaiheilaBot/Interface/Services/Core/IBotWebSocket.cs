using System.Threading.Tasks;
using Websocket.Client;

namespace KaiheilaBot.Interface
{
    public interface IBotWebSocket
    {
        Task<int> Connect();
    }
}
