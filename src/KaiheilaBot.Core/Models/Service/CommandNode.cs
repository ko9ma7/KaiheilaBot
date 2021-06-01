using System;
using System.Collections.Generic;
using KaiheilaBot.Core.Services;
using Microsoft.Extensions.Logging;

namespace KaiheilaBot.Core.Models.Service
{
    public record CommandNode
    {
        public string Name { get; set; }
        public List<CommandNode> ChildNodes { get; set; } = new();
        public List<string> AllowedChannels { get; set; } = new();
        public List<long> AllowedRoles { get; set; } = new();
        public List<string> AllowedUsers { get; set; } = new();
        public Func<IReadOnlyCollection<string>, ILogger<CommandService>, int> Function { get; set; }
        
        public CommandNode(string name)
        {
            Name = name;
        }

        public CommandNode AddAllowedChannel(string channelId)
        {
            AllowedChannels.Add(channelId);
            return this;
        }

        public CommandNode AddChildNode(CommandNode childNode)
        {
            ChildNodes.Add(childNode);
            return this;
        }

        public CommandNode AddAllowedRoles(long roleId)
        {
            AllowedRoles.Add(roleId);
            return this;
        }

        public CommandNode AddAllowedUsers(string userId)
        {
            AllowedUsers.Add(userId);
            return this;
        }

        public CommandNode SetFunction(Func<IReadOnlyCollection<string>, ILogger<CommandService>, int> function)
        {
            Function = function;
            return this;
        }
    }
}
