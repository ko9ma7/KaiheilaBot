using System.Text.Json;
using System.Threading.Tasks;
using KaiheilaBot.Core.Extension;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace LogEventDataPlugin
{
    /// <summary>
    /// 本项目引用了一个临时打包 KaiheilaBot.Core 用于测试的 Nuget 包。
    /// 如果你想要测试此插件，请打包 KaiheilaBot.Core ，版本设置为 1.0.0，
    /// 将打包好的 Nuget-Package 放在本地 Nuget Feed 路径下
    /// </summary>
    public class LogEventDataPlugin : IPlugin
    {
        private ILogger<IPlugin> _logger;
        
        public Task Initialize(ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            _logger = logger;
            _logger.LogInformation("已加载 LogEventDataPlugin.dll");
            return Task.CompletedTask;
        }

        public Task Execute(JsonElement data)
        {
            _logger.LogInformation(data.ToString());
            return Task.CompletedTask;
        }

        public Task Unload()
        {
            return Task.CompletedTask;
        }
    }
}