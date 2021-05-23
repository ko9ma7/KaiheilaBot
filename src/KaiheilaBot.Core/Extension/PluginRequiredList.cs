using System;
using System.Collections.Generic;
using System.Linq;
using KaiheilaBot.Core.Models.Events;

namespace KaiheilaBot.Core.Extension
{
    public class PluginRequiredList
    {
        private readonly List<string> _requireList = new();

        public PluginRequiredList RequireChannelRelatedEvents()
        {
            foreach (var type in Enum.GetNames(typeof(EnumChannelRelatedEvents)))
            {
                _requireList.Add(type);
            }
            return this;
        }

        public PluginRequiredList RequireGuildMemberEvents()
        {
            foreach (var type in Enum.GetNames(typeof(EnumGuildMemberEvents)))
            {
                _requireList.Add(type);
            }
            return this;
        }
        
        public PluginRequiredList RequireGuildRelatedEvents()
        {
            foreach (var type in Enum.GetNames(typeof(EnumGuildRelatedEvents)))
            {
                _requireList.Add(type);
            }
            return this;
        }
        
        public PluginRequiredList RequireGuildRoleEvents()
        {
            foreach (var type in Enum.GetNames(typeof(EnumGuildRoleEvents)))
            {
                _requireList.Add(type);
            }
            return this;
        }
        
        public PluginRequiredList RequireMessageRelatedEvents()
        {
            foreach (var type in Enum.GetNames(typeof(EnumMessageRelatedEvents)))
            {
                _requireList.Add(type);
            }
            return this;
        }
        
        public PluginRequiredList RequirePrivateMessageEvents()
        {
            foreach (var type in Enum.GetNames(typeof(EnumPrivateMessageEvents)))
            {
                _requireList.Add(type);
            }
            return this;
        }
        
        public PluginRequiredList RequireUserRelatedEvents()
        {
            foreach (var type in Enum.GetNames(typeof(EnumUserRelatedEvents)))
            {
                _requireList.Add(type);
            }
            return this;
        }
        
        public PluginRequiredList RequireChannelRelatedEvents(EnumChannelRelatedEvents e)
        {
            _requireList.Add(e.ToString());
            return this;
        }

        public PluginRequiredList RequireGuildMemberEvents(EnumGuildMemberEvents e)
        {
            _requireList.Add(e.ToString());
            return this;
        }
        
        public PluginRequiredList RequireGuildRelatedEvents(EnumGuildRelatedEvents e)
        {
            _requireList.Add(e.ToString());
            return this;
        }
        
        public PluginRequiredList RequireGuildRoleEvents(EnumGuildRoleEvents e)
        {
            _requireList.Add(e.ToString());
            return this;
        }
        
        public PluginRequiredList RequireMessageRelatedEvents(EnumMessageRelatedEvents e)
        {
            _requireList.Add(e.ToString());
            return this;
        }
        
        public PluginRequiredList RequirePrivateMessageEvents(EnumPrivateMessageEvents e)
        {
            _requireList.Add(e.ToString());
            return this;
        }
        
        public PluginRequiredList RequireUserRelatedEvents(EnumUserRelatedEvents e)
        {
            _requireList.Add(e.ToString());
            return this;
        }

        public List<string> GetRequireList()
        {
            return _requireList.Distinct().ToList();
        }
    }
}