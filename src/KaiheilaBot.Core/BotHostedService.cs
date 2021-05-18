using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core
{
    public class BotHostedService : IHostedService
    {
        private readonly ILogger<BotHostedService> _logger;

        public BotHostedService(ILogger<BotHostedService> logger)
        {
            _logger = logger;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Bot 启动中...");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}