using System.Threading.Tasks;

namespace KaiheilaBot.Interface
{
    interface IBotService
    {
        Task StartApp(bool autoReconnect = true);
    }
}
