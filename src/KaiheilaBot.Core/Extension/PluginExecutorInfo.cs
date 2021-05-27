using System.Reflection;

namespace KaiheilaBot.Core.Extension
{
    /// <summary>
    /// 插件 Execute 方法信息
    /// </summary>
    public record PluginExecutorInfo
    {
        public string TypeString{ get; set; }

        public MethodInfo Method { get; set; }

        public object ExecutorClassInstance { get; set; }
    }
}
