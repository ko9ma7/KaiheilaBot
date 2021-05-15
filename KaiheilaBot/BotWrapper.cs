using KaiheilaBot.Core;
using KaiheilaBot.Interface;
using KaiheilaBot.Models;
using SimpleInjector;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace KaiheilaBot
{
    /// <summary>
    /// 最基础的Bot，创建后需要加载插件
    /// </summary>
    public class BotWrapper
    {
        private readonly Container Container;
        /// <summary>
        /// 创建Bot
        /// </summary>
        public BotWrapper()
        {
            //注册DI
            Container = new Container();
            Container.Register<ILogService, LogService>(Lifestyle.Singleton);
            Container.Register<IBotService, BotService>();
            Container.Register<IBotWebSocket, BotWebsocket>();
            Container.Register<IBotRequest, BotRequest>();
            Container.Register<Shared>(Lifestyle.Singleton);
            Container.Register<Config>(Lifestyle.Singleton);
        }

        public async Task Start()
        {
            var log = Container.GetInstance<ILogService>();
            log.Information("正在加载插件...");
            if (!Directory.Exists("Plugin"))
            {
                Directory.CreateDirectory("Plugin");
            }
            IList<Assembly> assemblies = new List<Assembly>();
            foreach(var file in Directory.GetFiles("Plugin", "*.dll"))
            {
                assemblies.Add(Assembly.LoadFrom(file));
            }
            Container.Register(typeof(IPlugin<>),assemblies);
            log.Information("插件加载完毕！...");
            var bot = Container.GetInstance<IBotService>();
            await bot.StartApp();
        }
    }
}
