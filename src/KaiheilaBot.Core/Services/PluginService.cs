using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KaiheilaBot.Core.Extension;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using McMaster.NETCore.Plugins;

namespace KaiheilaBot.Core.Services
{
    public class PluginService : IPluginService
    {
        private readonly ILogger<PluginService> _logger;
        private readonly ILogger<IPlugin> _pluginLogger;
        private readonly IMessageHubService _messageHubService;
        private readonly IHttpApiRequestService _httpApiRequestService;
        private readonly IConfiguration _configuration;

        private readonly Dictionary<string, IPlugin> _plugins = new();
        private readonly Dictionary<string, List<string>> _pluginRequiredList = new();
        
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
            var sw = new Stopwatch();
            _logger.LogInformation("开始加载插件");
            sw.Start();
            var pluginDirectory = Path.Join(_configuration["PluginFolder"], "Plugins");
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
            
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var (pluginFilePath, loader) in loaders)
            {
                foreach (var pluginType in loader
                    .LoadDefaultAssembly()
                    .GetTypes()
                    .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                    var pluginName = Path.GetFileName(pluginFilePath);
                    _plugins.Add(pluginName, plugin);
                }
            }

            foreach (var (pluginName, plugin) in _plugins)
            {
                var required = await plugin.Initialize(_pluginLogger, _httpApiRequestService);
                _pluginRequiredList.Add(pluginName, required);
                _logger.LogInformation($"已载入插件 {pluginName}");
            }
            sw.Stop();
            _logger.LogInformation($"已完成插件载入，共 {_plugins.Count} 个，耗时：{sw.ElapsedMilliseconds} 毫秒");
        }

        public void SubscribeToMessageHub()
        {
            _logger.LogInformation("开始注册插件 Execute 方法");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var (id, plugin) in _plugins)
            {
                var required = _pluginRequiredList[id];
                _messageHubService.Subscribe(plugin, required, id);
            }
            sw.Stop();
            _logger.LogInformation($"已完成插件 Execute 方法注册，耗时 {sw.ElapsedMilliseconds} 毫秒");
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