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
        private readonly IMessageHubService _messageHubService;
        private readonly IBotWebsocketService _botWebsocketService;
        private readonly IConfiguration _configuration;

        // 测试使用，非成品
        public BotHostedService(ILogger<BotHostedService> logger, 
            IMessageHubService messageHubService, 
            IBotWebsocketService botWebsocketService,
            IConfiguration configuration)
        {
            _logger = logger;
            _messageHubService = messageHubService;
            _botWebsocketService = botWebsocketService;
            _configuration = configuration;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(_configuration["Token"]);
            _logger.LogInformation("Bot 启动中...");
            _messageHubService.Subscribe<JsonElement>(async data =>
            {
                _logger.LogDebug(data.ToString());
                await Task.Delay(500, cancellationToken);
            }, "123");

            await _botWebsocketService.Connect();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}