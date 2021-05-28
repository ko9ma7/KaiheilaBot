using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HttpServerLite;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Services
{
    public class HttpServerService : IHttpServerService
    {
        private readonly ILogger<HttpServerService> _logger;
        private readonly IPluginService _pluginService;
        private readonly IMessageHubService _messageHubService;
        private readonly IConfiguration _configuration;
        private readonly Webserver _server;

        private List<string> _pluginList;
        
        public HttpServerService(
            ILogger<HttpServerService> logger, 
            IPluginService pluginService,
            IMessageHubService messageHubService,
            IConfiguration configuration)
        {
            _logger = logger;
            _pluginService = pluginService;
            _messageHubService = messageHubService;
            _configuration = configuration;
            _server = new Webserver();
        }
        
        public Task Start()
        {
            if (_configuration["EnableHttpServer"] == "false")
            {
                _logger.LogInformation("HS - HttpServer 在配置文件中禁用");
                return Task.CompletedTask;
            }
            
            _pluginList = _pluginService.GetPluginUniqueId();

            var serverUrl = _configuration["HttpServerUrl"];
            _server.Settings.Headers.Host = serverUrl;
            _server.Routes.Parameter.Add(HttpMethod.POST, "/{pluginId}", Router);
            _server.Start();
            
            _logger.LogInformation($"HS - 已开启 HttpServer，BaseUrl：{serverUrl}");
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            if (_server.IsListening is false)
            {
                return Task.CompletedTask;
            }
            _server.Stop();
            _logger.LogInformation("HS - 已关闭 HttpServer");
            return Task.CompletedTask;
        }

        private async Task Router(HttpContext ctx)
        {
            var pluginId = ctx.Request.Url.Parameters["pluginId"];
            var data = Encoding.UTF8.GetString(ctx.Request.Data);

            _logger.LogDebug($"HS - 收到信息：{data}");
            
            string responseData;

            if (_pluginList.Contains(pluginId))
            {
                ctx.Response.StatusCode = 200;
                responseData = "{\"{status}\":\"success\"}";
                _logger.LogDebug($"HS - 插件存在，已设置 Response，Status：200，Body：{responseData}");
                _messageHubService.Publish(pluginId, data);
                _logger.LogInformation($"HS - 收到信息，已发送至 MessageHub，插件 ID：{pluginId}");
            }
            else
            {
                ctx.Response.StatusCode = 500;
                responseData = "{\"status\":\"failed\"}";
                _logger.LogDebug($"HS - 插件不存在，已设置 Response，Status：500，Body：{responseData}");
                _logger.LogWarning($"HS - 收到不受支持的信息，请确认插件 {pluginId} 已加载");
            }

            ctx.Response.ContentType = "application/json";
            await ctx.Response.SendAsync(responseData);
            _logger.LogDebug("HS - 已返回 Response");
        }
    }
}