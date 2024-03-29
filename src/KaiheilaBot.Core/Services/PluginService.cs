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
using Microsoft.Extensions.DependencyInjection;
using KaiheilaBot.Core.Models.Events;

namespace KaiheilaBot.Core.Services
{
    public class PluginService : IPluginService
    {
        private readonly ILogger<PluginService> _logger;
        private readonly IMessageHubService _messageHubService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        private readonly List<PluginInfo> _plugins = new();
        
        public PluginService(ILogger<PluginService> logger,
            IMessageHubService messageHubService,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _messageHubService = messageHubService;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }
        
        public async Task LoadPlugins()
        {
            var sw = new Stopwatch();
            _logger.LogInformation("PS - 开始加载插件");
            sw.Start();
            var pluginDirectory = Path.Join(_configuration["PluginFolder"], "Plugins");
            var loaders = new Dictionary<string, PluginLoader>();

            // 载入插件 DLL 文件
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
                _logger.LogDebug($"PS - 已加载插件文件 {Path.GetFileName(pluginDll)}");
            }

            var executors = new List<string>();
            foreach (var typeName in Enum.GetNames(typeof(EnumEvents)))
            {
                executors.Add($"I{typeName}Executor");
                _logger.LogDebug($"PS - 已加载 Event 接口类型 I{typeName}Executor");
            }

            // 读取插件接口和实例化
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (var (pluginFilePath, loader) in loaders)
            {
                var name = Path.GetFileNameWithoutExtension(pluginFilePath);
                var pluginTypes = loader.LoadDefaultAssembly().GetTypes();

                var iPluginType = pluginTypes.First(t => typeof(IPlugin).IsAssignableFrom(t) && t.IsAbstract is false);
                var plugin = (IPlugin)Activator.CreateInstance(iPluginType);
                var pi = new PluginInfo(name, pluginFilePath, plugin);

                // 查找插件中是否存在 HttpServerDataResolver
                var iHttpServerDataResolverType = pluginTypes.FirstOrDefault(
                    t => typeof(IHttpServerDataResolver).IsAssignableFrom(t) && t.IsAbstract is false);
                if (iHttpServerDataResolverType is not null)
                {
                    var resolver = (IHttpServerDataResolver) Activator.CreateInstance(iHttpServerDataResolverType);
                    var resolveMethod = iHttpServerDataResolverType.GetMethod("Resolve");
                    pi.AddExecutor("HttpServerData", resolveMethod, resolver);
                    _logger.LogDebug($"PS - 已载入插件 {name} 的 Resolver");
                }
                
                foreach (var eventExecutorInterfaceType in executors)
                {
                    // 创建 Executor 类型
                    var iType = Type.GetType(
                        $"KaiheilaBot.Core.Extension.IEventExecutors.{eventExecutorInterfaceType}");

                    if (iType is null)
                    {
                        _logger.LogError($"PS - 创建 Executor 类型时出现错误，ID：{eventExecutorInterfaceType}");
                        continue;
                    }
                    
                    // 查找插件中是否存在该 Executor 类型
                    var iPluginExecutorType =
                        pluginTypes.FirstOrDefault(t => iType.IsAssignableFrom(t) && t.IsAbstract is false);

                    if (iPluginExecutorType is null)
                    {
                        continue;
                    }

                    // 创建实例，创建 Method
                    var executor = Activator.CreateInstance(iPluginExecutorType);
                    var executeMethod = iPluginExecutorType.GetMethod("Execute");
                    pi.AddExecutor(eventExecutorInterfaceType, executeMethod, executor);
                    _logger.LogDebug($"PS - 已载入插件 {name} 的 Executor - {eventExecutorInterfaceType}");
                }

                _plugins.Add(pi);
                _logger.LogInformation($"PS - 已载入插件 {pi.GetId()}");
            }

            sw.Stop();
            _logger.LogInformation($"PS - 已完成插件载入，共 {_plugins.Count} 个，耗时：{sw.ElapsedMilliseconds} 毫秒");
            
            // 执行 Initialize 方法初始化插件
            _logger.LogInformation("PS - 开始执行插件初始化方法");
            foreach (var pi in _plugins)
            {
                sw.Reset();
                sw.Start();
                await pi.GetPluginInstance().Initialize(
                    _serviceProvider.GetService<ILogger<IPlugin>>(),
                    _serviceProvider.GetService<IHttpApiRequestService>(),
                    Path.GetDirectoryName(pi.GetPath()));
                sw.Stop();
                _logger.LogInformation($"PS - 执行插件 {pi.GetId()} 初始化方法完成，耗时：{sw.ElapsedMilliseconds} 毫秒");
            }
        }

        public void SubscribeToMessageHub()
        {
            _logger.LogInformation("PS - 开始注册插件 Execute 方法");
            var sw = new Stopwatch();
            sw.Start();
            foreach (var pi in _plugins)
            {
                _messageHubService.Subscribe(pi);
            }
            sw.Stop();
            _logger.LogInformation($"PS - 已完成插件 Execute 方法注册，耗时 {sw.ElapsedMilliseconds} 毫秒");
        }

        public void UnloadPlugin()
        {
            var unloadGroup = new PluginInfo[_plugins.Count];
            _plugins.CopyTo(unloadGroup);
            foreach (var pi in unloadGroup)
            {
                UnloadPlugin(pi.GetId());
            }
        }
        
        public void UnloadPlugin(string pluginUniqueId)
        {
            var plugin = _plugins.FirstOrDefault(p => p.GetId() == pluginUniqueId);
            if (plugin is null)
            {
                return;
            }

            if (_messageHubService.UnSubscribe(plugin.GetId()) is not true)
            {
                return;
            }
            plugin.GetPluginInstance().Unload(
                _serviceProvider.GetService<ILogger<IPlugin>>(),
                _serviceProvider.GetService<IHttpApiRequestService>());
            _logger.LogInformation($"PS - 已卸载插件 {plugin.GetId()}");
            _plugins.Remove(plugin);
        }

        public List<string> GetPluginUniqueId()
        {
            return _plugins.Select(x => x.GetId()).ToList();
        }
    }
}
