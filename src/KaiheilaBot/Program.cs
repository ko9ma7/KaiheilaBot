using System.IO;
using System.Threading.Tasks;

namespace KaiheilaBot
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var instanceDirectory = Path.Join(currentDirectory, "Instance");
            var configFilePath = Path.Join(instanceDirectory, "config.yml");
            if (Directory.Exists(instanceDirectory) is false)
            {
                Directory.CreateDirectory(instanceDirectory);
            }

            if (File.Exists(configFilePath) is false)
            {
                File.Copy(Path.Join(currentDirectory,"defaultConfig.yml"),
                    configFilePath);
            }
            
            await Core.KaiheilaBot.Startup(instanceDirectory);
        }
    }
}
