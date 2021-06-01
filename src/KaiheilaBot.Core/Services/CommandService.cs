using System.Collections.Generic;
using System.Linq;
using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using KaiheilaBot.Core.Models.Service;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Services
{
    public class CommandService : ICommandService
    {
        private readonly ILogger<CommandService> _logger;
        private readonly List<CommandNode> _rootCommands;

        public CommandService(ILogger<CommandService> logger)
        {
            _logger = logger;

            _rootCommands = new List<CommandNode>();
        }
        
        public void AddCommand(CommandNode rootCommandNode)
        {
            _rootCommands.Add(rootCommandNode);
        }

        public CommandParserStatus Parse(BaseMessageEvent<TextMessageEvent> e)
        {
            var channel = e.Data.TargetId;
            var author = e.Data.AuthorId;
            var authorRoles = e.Data.Extra.Author.Roles;
            var content = e.Data.Content.Split(' ');
            var cmdLength = content.Length;
            var currentProcess = 0;
            
            var node = _rootCommands.FirstOrDefault(x => x.Name == content[currentProcess]);

            if (node is null)
            {
                return CommandParserStatus.NoMatchCommand;
            }

            while (node.ChildNodes.Count != 0)
            {
                if ((node.AllowedChannels.Count == 0 || node.AllowedChannels.Contains(channel)) is false)
                {
                    return CommandParserStatus.ChannelNotAllowed;
                }

                // ReSharper disable once PossibleMultipleEnumeration
                if ((node.AllowedRoles.Any(authorRoles.Contains) || node.AllowedUsers.Contains(author)
                    || node.AllowedRoles.Count == 0 || node.AllowedUsers.Count == 0) is false)
                {
                    return CommandParserStatus.NoPermission;
                }
                
                currentProcess++;
                if (currentProcess == content.Length)
                {
                    break;
                }
                
                node = node.ChildNodes.FirstOrDefault(x => x.Name == content[currentProcess]);
                
                if (node is null)
                {
                    return CommandParserStatus.NoMatchCommand;
                }
            }

            var args = new List<string>();
            for (var i = currentProcess; i < content.Length; i++)
            {
                args.Add(content[i]);
            }

            var status = node.Function(args, _logger);

            return status != 0 ? CommandParserStatus.FunctionError : CommandParserStatus.Success;
        }
    }
}
