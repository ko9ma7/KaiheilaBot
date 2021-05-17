using SimpleInjector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KaiheilaBot.Interface.Services
{
    public interface IPluginLoader<T>
    {
        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public Task<IList<T>> Load();
        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public Task Unload();
    }
}
