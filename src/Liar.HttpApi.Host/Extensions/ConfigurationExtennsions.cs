using System.Threading;
using AutoMapper.Configuration;
using Liar.Domain.Shared.ConfigModels;
using Microsoft.Extensions.DependencyInjection;
using Liar.Core.Microsoft.Extensions.Configuration;

namespace Liar.HttpApi.Host.Extensions
{
    public static class ConfigurationExtennsions
    {
        public static void AddConfigurationSetup(this IServiceCollection services)
        {
            var configuration = services.GetConfiguration();
            services.Configure<JwtConfig>(configuration.GetJWTSection());
            services.Configure<MongoConfig>(configuration.GetMongoDbSection());
            services.Configure<MysqlConfig>(configuration.GetMysqlSection());
            services.Configure<RabbitMqConfig>(configuration.GetRabbitMqSection());
            services.Configure<ThreadPoolSettings>(configuration.GetThreadPoolSettingsSection());
        }
    }
}
