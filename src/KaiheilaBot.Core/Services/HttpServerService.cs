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
        private readonly IMessageHubService _messageHubService;
        private readonly IConfiguration _configuration;
        private readonly Webserver _server;

        private readonly List<string> _pluginList;
        
        public HttpServerService(
            ILogger<HttpServerService> logger, 
            IPluginService pluginService,
            IMessageHubService messageHubService,
            IConfiguration configuration)
        {
            _logger = logger;
            _messageHubService = messageHubService;
            _configuration = configuration;
            _server = new Webserver();

            _pluginList = pluginService.GetPluginUniqueId();
        }
        
        public Task Start()
        {
            if (_configuration["EnableHttpServer"] == "false")
            {
                _logger.LogInformation("HttpServer 未开启");
                return Task.CompletedTask;
            }

            var serverUrl = _configuration["HttpServerUrl"];
            _server.Settings.Headers.Host = serverUrl;
            _server.Start();
            
            _logger.LogInformation($"已开启 HttpServer，BaseUrl：{serverUrl}");
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            if (_server.IsListening is false)
            {
                return Task.CompletedTask;
            }
            _server.Stop();
            _logger.LogInformation("已关闭 HttpServer");
            return Task.CompletedTask;
        }

        [ParameterRoute(HttpMethod.POST, "/{pluginId}")]
        public async Task Router(HttpContext ctx)
        {
            var pluginId = ctx.Request.Url.Parameters["pluginId"];
            var data = Encoding.UTF8.GetString(ctx.Request.Data);

            _logger.LogDebug($"HttpServer 收到信息：{data}");
            
            string responseData;

            if (_pluginList.Contains(pluginId + ".dll"))
            {
                ctx.Response.StatusCode = 200;
                responseData = "{\"{status}\":\"success\"}";
                _messageHubService.Publish(pluginId, data);
                _logger.LogInformation($"HttpServer 收到信息，已发送至 MessageHub，插件 ID：{pluginId}");
            }
            else
            {
                ctx.Response.StatusCode = 500;
                responseData = "{\"status\":\"failed\"}";
                _logger.LogWarning($"HttpServer收到不受支持的信息，请确认插件 {pluginId} 已加载");
            }

            ctx.Response.ContentType = "application/json";
            await ctx.Response.SendAsync(responseData);
            _logger.LogDebug("HttpServer 已返回 Response");
        }
    }
}