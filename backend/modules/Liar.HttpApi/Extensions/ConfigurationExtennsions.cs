using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared.ConfigModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Liar.HttpApi.Extensions
{
    public static class ConfigurationExtennsions
    {
        public static void AddConfiguration(this IServiceCollection services)
        {
            var configuration = services.GetConfiguration();
            services.Configure<AppConfig>(configuration.GetAppSection());
            services.Configure<JwtConfig>(configuration.GetJWTSection());
            services.Configure<MongoConfig>(configuration.GetMongoDbSection());
            services.Configure<MysqlConfig>(configuration.GetMysqlSection());
            services.Configure<RabbitMqConfig>(configuration.GetRabbitMqSection());
            services.Configure<ThreadPoolSettings>(configuration.GetThreadPoolSettingsSection());
        }
    }
}
