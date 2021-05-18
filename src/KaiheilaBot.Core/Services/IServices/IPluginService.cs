using System.Collections.Generic;
using System.Threading.Tasks;

namespace KaiheilaBot.Core.Services.IServices
{
    public interface IPluginService
    {
        public Task LoadPlugins();

        public void SubscribeToMessageHub();

        public void UnloadPlugin();
        
        public void UnloadPlugin(string pluginUniqueId);

        public List<string> GetPluginUniqueId();
    }
}