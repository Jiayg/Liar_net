using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Liar
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                      .ConfigureHostConfiguration(configuration =>
                      {
                          configuration.AddCommandLine(args);
                      })
                      .ConfigureWebHostDefaults(builder =>
                      {
                          builder.UseIISIntegration()
                                 .ConfigureKestrel(options =>
                                 {
                                     options.AddServerHeader = false;
                                 })
                                 .UseStartup<Startup>();
                      })
                      .ConfigureLogging(logging =>
                      {
                          // 1.过滤掉系统默认的一些日志 
                          logging.AddFilter("System", LogLevel.Error);
                          logging.AddFilter("Microsoft", LogLevel.Information);
                          logging.AddFilter("Volo.Abp", LogLevel.Information);

                          logging.ClearProviders();
                          logging.AddConsole();
                          logging.AddDebug();
                      })
                      .UseAutofac()
                      .UseNLog()
                      .Build()
                      .RunAsync();
        }
    }
}
