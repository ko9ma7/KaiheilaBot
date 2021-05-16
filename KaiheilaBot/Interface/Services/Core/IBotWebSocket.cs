using System.Threading.Tasks;

namespace KaiheilaBot.Interface
{
    public interface IBotWebSocket
    {
        Task<int> Connect();
    }
}
