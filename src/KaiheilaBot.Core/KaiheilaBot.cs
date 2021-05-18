using System;
using System.Threading.Tasks;
using KaiheilaBot.Core.Services;
using KaiheilaBot.Core.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KaiheilaBot.Core
{
    public static class KaiheilaBot
    {
        /// <summary>
        /// 启动 Host
        /// </summary>
        public static async Task<int> Startup()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                .WriteTo.Console()
                .WriteTo.File(path: @"logs/log-.log", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();

            try
            {
                Log.Logger.Information("Starting host...");
                await Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return 0;
        }

        /// <summary>
        /// 构建与运行 Generic Host
        /// </summary>
        private static async Task Run() =>
            await new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddHostedService<BotHostedService>();
                })
                .UseSerilog()
                .RunConsoleAsync();

    }
}