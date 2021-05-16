using KaiheilaBot.Core;
using KaiheilaBot.Interface;
using KaiheilaBot.Models;
using KaiheilaBot.Service;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace KaiheilaBot
{
    /// <summary>
    /// 最基础的Bot，创建后需要加载插件
    /// </summary>
    public class BotWrapper
    {
        private readonly IList<string> tempDlls = new List<string>();
        private readonly Container Container;
        private ILogService log;
        private IList<IPlugin> plugins = new List<IPlugin>();
        private AssemblyLoadContext context = new AssemblyLoadContext("plugin", true);
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
            Container.Register<IConsole, ConsoleService>();
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public async virtual Task Start()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "Plugin";
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            log = Container.GetInstance<ILogService>();
            log.Information("正在加载插件...");
            if (!Directory.Exists("Plugin"))
            {
                Directory.CreateDirectory("Plugin");
            }
            Register();
            log.Information("插件加载完毕！...");
            var bot = Container.GetInstance<IBotService>();
            await bot.StartApp();
        }

        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            RemoveCache();
        }

        private async Task Handle(ReceiveMessage message, IList<IPlugin> plugins)
        {
            foreach(var plugin in plugins)
            {
                switch(plugin.HandleType)
                {
                    case EventType.ChannelTextMessage:
                        if (message.Data.Extra is ExtraText)
                        {
                            var author = (message.Data.Extra as ExtraText);
                            if (!author.Author.IsBot && !author.Author.IsSystem)
                            {
                                await plugin.Handle(new TextMessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                            }
                        }
                        break;
                    case EventType.BotInvited:
                        if(message.Data.Extra is Invited)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.ChannelCreated:
                        if (message.Data.Extra is NewChannel)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.ChannelEdited:
                        if (message.Data.Extra is EditChannel)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.ChannelRemoved:
                        if (message.Data.Extra is DeleteChannel)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MemberChangeNick:
                        if (message.Data.Extra is MemberChangeNick)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MemberJoinChannel:
                        if (message.Data.Extra is MemberJoin)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MemberLeaveChannel:
                        if (message.Data.Extra is MemberLeave)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessageChanged:
                        if (message.Data.Extra is UpdateMessage)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessagePinned:
                        if (message.Data.Extra is PinnedMessage)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessageReacted:
                        if (message.Data.Extra is Reaction)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                    case EventType.MessageRemoved:
                        if (message.Data.Extra is DeleteMessage)
                        {
                            await plugin.Handle(new MessageEventArgs(message.Data, Container.GetInstance<IConsole>()));
                        }
                        break;
                }
            }

        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            context.Unload();
            context = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            RemoveCache();
            context = new AssemblyLoadContext("plugin", true);
            Register();
        }

        private void Register()
        {
            var type = typeof(IPlugin);
            var hub = Container.GetInstance<Shared>();
            hub.messageHub.ClearSubscriptions();
            foreach (var file in Directory.GetFiles("Plugin", "*.dll", SearchOption.AllDirectories))
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    if (!Directory.Exists("Temp"))
                    {
                        Directory.CreateDirectory("Temp");
                    }
                    var temp = Path.Combine(Environment.CurrentDirectory, "Temp", Path.GetRandomFileName());
                    tempDlls.Add(temp);
                    File.Copy(file, temp, true);
                    try
                    {
                        var assembly = context.LoadFromAssemblyPath(temp);
                        foreach (var i in assembly.GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract))
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(i);
                            plugins.Add(plugin);
                            log.Debug(plugin.GetType().Name + " 插件已注册！");
                        }
                    }
                    catch(Exception ex)
                    {
                        //dll 加载错误，无视
                        log.Error(file + " 插件加载失败！" + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(file);
                        foreach (var i in assembly.GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract))
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(i);
                            plugins.Add(plugin);
                            log.Debug(plugin.GetType().Name + " 插件已注册！");
                        }
                    }
                    catch
                    {

                    }

                }
            }
            hub.messageHub.Subscribe<ReceiveMessage>(async message => {
                await Handle(message, plugins);
            });
        }

        private void RemoveCache()
        {
            log.Information("正在卸载插件...");
            plugins.Clear();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var temp in tempDlls)
                    {
                        sb.AppendLine("del /f " + temp);
                    }
                    sb.AppendLine("(goto) 2>nul & del \"%~f0\"");
                    File.WriteAllText("remove.bat", sb.ToString());
                    Process.Start("remove.bat");
                }
                catch{

                }
            }
        }
    }
}
