using System.Threading.Tasks;

namespace KaiheilaBot
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Core.KaiheilaBot.Startup();
        }
    }
}
