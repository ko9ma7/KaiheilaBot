using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace KaiheilaBot.Core.Extension
{
    /// <summary>
    /// 定义一个插件的所有信息
    /// </summary>
    public class PluginInfo
    {

        private readonly string _id;
        private readonly string _path;
        private readonly IPlugin _pluginInstance;
        private readonly List<PluginExecutorInfo> _executors;

        public PluginInfo(string id, string path, IPlugin pluginInstance)
        {
            _id = id;
            _path = path;
            _pluginInstance = pluginInstance;
            _executors = new List<PluginExecutorInfo>();
        }

        public void AddExecutor(string type, MethodInfo method, object instance)
        {
            _executors.Add(new PluginExecutorInfo()
            {
                TypeString = type,
                Method = method,
                ExecutorClassInstance = instance
            });
        }

        public List<PluginExecutorInfo> GetExecutors()
        {
            return _executors;
        }

        public string GetId()
        {
            return _id;
        }

        public IPlugin GetPluginInstance()
        {
            return _pluginInstance;
        }

        public string GetPath()
        {
            return _path;
        }

        public override string ToString()
        {
            return $"PluginId: {_id}, Path: {_path}, Executors: {_executors.Count}";
        }
    }
}
