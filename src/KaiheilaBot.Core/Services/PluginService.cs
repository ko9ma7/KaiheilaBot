using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KaiheilaBot.Core.Extension;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using McMaster.NETCore.Plugins;

namespace KaiheilaBot.Core.Services
{
    // TODO: 插件测试和日志记录
    public class PluginService : IPluginService
    {
        private readonly ILogger<PluginService> _logger;
        private readonly ILogger<IPlugin> _pluginLogger;
        private readonly IMessageHubService _messageHubService;
        private readonly IHttpApiRequestService _httpApiRequestService;
        private readonly IConfiguration _configuration;

        private readonly Dictionary<string, IPlugin> _plugins = new();
        
        public PluginService(ILogger<PluginService> logger,
            ILogger<IPlugin> pluginLogger,
            IMessageHubService messageHubService,
            IHttpApiRequestService httpApiRequestService,
            IConfiguration configuration)
        {
            _logger = logger;
            _pluginLogger = pluginLogger;
            _messageHubService = messageHubService;
            _httpApiRequestService = httpApiRequestService;
            _configuration = configuration;
        }
        
        public async Task LoadPlugins()
        {
            var pluginDirectory = Path.Join(_configuration["PluginFolder"], "Plugin");
            var loaders = new Dictionary<string, PluginLoader>();
            foreach (var dir in Directory.GetDirectories(pluginDirectory))
            {
                var dirName = Path.GetFileName(dir);
                var pluginDll = Path.Combine(dir, dirName + ".dll");
                if (File.Exists(pluginDll) is false)
                {
                    continue;
                }
                var loader = PluginLoader.CreateFromAssemblyFile(
                    pluginDll,
                    sharedTypes: new [] { typeof(IPlugin) });
                loaders.Add(pluginDll, loader);
            }
            
            // Rider IDE Configuration - 请不要删除下一行
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var (pluginName, loader) in loaders)
            {
                foreach (var pluginType in loader
                    .LoadDefaultAssembly()
                    .GetTypes()
                    .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                    _plugins.Add(pluginName, plugin);
                }
            }

            foreach (var (_, plugin) in _plugins)
            {
                await plugin.Initialize(_pluginLogger, _httpApiRequestService);
            }
        }

        public void SubscribeToMessageHub()
        {
            foreach (var (id, plugin) in _plugins)
            {
                _messageHubService.Subscribe<JsonElement>(async data =>
                {
                    await plugin.Execute(data);
                }, id);
            }
        }

        public void UnloadPlugin()
        {
            foreach (var (id, _) in _plugins)
            {
                UnloadPlugin(id);
            }
        }
        
        public void UnloadPlugin(string pluginUniqueId)
        {
            if (_plugins.Keys.Contains(pluginUniqueId) || _plugins.Keys.Contains(pluginUniqueId + ".dll"))
            {
                _messageHubService.UnSubscribe(pluginUniqueId);
            }

            _plugins[pluginUniqueId].Unload();
            _plugins[pluginUniqueId] = null;
            _plugins.Remove(pluginUniqueId);
        }

        public List<string> GetPluginUniqueId()
        {
            return new(_plugins.Keys);
        }
    }
}