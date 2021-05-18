using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core
{
    public class BotHostedService : IHostedService
    {
        private readonly ILogger<BotHostedService> _logger;
        private readonly IBotWebsocketService _botWebsocketService;
        private readonly IPluginService _pluginService;
        private readonly IConfiguration _configuration;

        public BotHostedService(ILogger<BotHostedService> logger, 
            IBotWebsocketService botWebsocketService,
            IPluginService pluginService,
            IConfiguration configuration)
        {
            _logger = logger;
            _botWebsocketService = botWebsocketService;
            _pluginService = pluginService;
            _configuration = configuration;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Bot 启动中...");
            await _pluginService.LoadPlugins();
            
            _pluginService.SubscribeToMessageHub();

            await _botWebsocketService.Connect();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // TODO: 卸载动作
            _pluginService.UnloadPlugin();
            return Task.CompletedTask;
        }
    }
}