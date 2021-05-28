using System.Threading;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core
{
    public class BotHostedService : IHostedService
    {
        private readonly ILogger<BotHostedService> _logger;
        private readonly IBotWebsocketService _botWebsocketService;
        private readonly IPluginService _pluginService;
        private readonly IHttpServerService _httpServerService;
        private readonly IMessageHubService _messageHubService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public BotHostedService(ILogger<BotHostedService> logger, 
            IBotWebsocketService botWebsocketService,
            IPluginService pluginService,
            IHttpServerService httpServerService,
            IMessageHubService messageHubService,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _botWebsocketService = botWebsocketService;
            _pluginService = pluginService;
            _httpServerService = httpServerService;
            _messageHubService = messageHubService;
            _hostApplicationLifetime = hostApplicationLifetime;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _hostApplicationLifetime.ApplicationStopped.Register(OnStopped);
            
            _logger.LogInformation("HOST - Bot 启动中...");
            await _pluginService.LoadPlugins();

            _pluginService.SubscribeToMessageHub();

            await _httpServerService.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _botWebsocketService.Connect();
        }
        
        private void OnStopping()
        {
            _logger.LogInformation("HOST - 准备关闭...");
            Task.Run(() => _httpServerService.Stop());
            Task.Run(() => _botWebsocketService.Stop()).Wait();
            _pluginService.UnloadPlugin();
            _messageHubService.Dispose();
        }
        
        private void OnStopped(){
            _logger.LogInformation("HOST - 机器人已停止");
        }
    }
}