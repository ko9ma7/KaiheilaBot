using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using KaiheilaBot.Core.Extension;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace LogEventDataPlugin
{
    public class LogEventDataPlugin : IPlugin
    {
        private ILogger<IPlugin> _logger;
        
        public Task<List<string>> Initialize(ILogger<IPlugin> logger, IHttpApiRequestService httpApiRequestService)
        {
            _logger = logger;
            var list = new PluginRequiredList()
                .RequireChannelRelatedEvents()
                .RequireGuildMemberEvents()
                .RequireGuildRelatedEvents()
                .RequireGuildRoleEvents()
                .RequireMessageRelatedEvents()
                .RequirePrivateMessageEvents()
                .RequireUserRelatedEvents()
                .GetRequireList();
            return Task.FromResult(list);
        }

        public Task Execute<T>(T data)
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