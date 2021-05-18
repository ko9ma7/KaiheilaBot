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
            var pluginDirectory = Path.Join(currentDirectory, "Plugins");
            var configFilePath = Path.Join(instanceDirectory, "config.yml");
            if (Directory.Exists(instanceDirectory) is false)
            {
                Directory.CreateDirectory(instanceDirectory);
            }

            if (Directory.Exists(pluginDirectory) is false)
            {
                Directory.CreateDirectory(pluginDirectory);
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
