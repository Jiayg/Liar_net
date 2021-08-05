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
                          logging.ClearProviders();
                          logging.AddConsole();
                          logging.SetMinimumLevel(LogLevel.Information);
                      })
                      .UseAutofac()
                      .UseNLog()
                      .Build()
                      .RunAsync();
        }
    }
}
