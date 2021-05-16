using KaiheilaBot.Interface;
using KaiheilaBot.Interface.Services;
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

namespace KaiheilaBot.Service
{
    public class PluginService<T> : IPluginLoader<T> where T : IPlugin
    {
        private readonly IList<T> plugins = new List<T>();
        private readonly IList<string> tempDlls = new List<string>();
        private AssemblyLoadContext context = new AssemblyLoadContext("plugin", true);
        private readonly ILogService log;
        public PluginService(ILogService service)
        {
            log = service;
        }
        public async Task<IList<T>> Load(Container container)
        {
            var type = typeof(T);
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
                            var plugin = (T)Activator.CreateInstance(i);
                            await plugin.PluginLoad(container);
                            plugins.Add(plugin);
                            log.Debug(plugin.GetType().Name + " 插件已注册！");
                        }
                    }
                    catch (Exception ex)
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
                            var plugin = (T)Activator.CreateInstance(i);
                            await plugin.PluginLoad(container);
                            plugins.Add(plugin);
                            log.Debug(plugin.GetType().Name + " 插件已注册！");
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return plugins;
        }

        public async Task Unload(Container container)
        {
            log.Information("正在卸载插件...");
            context.Unload();
            context = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            foreach (var plugin in plugins)
            {
                await plugin.PluginUnload(container);
            }
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
                catch
                {

                }
            }
        }
    }
}
