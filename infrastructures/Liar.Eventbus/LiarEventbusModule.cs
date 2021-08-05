using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared.ConfigModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;

namespace Liar
{
    [DependsOn(
        typeof(AbpEventBusAbstractionsModule),
        typeof(AbpEventBusModule))
        ]
    public class LiarEventBusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var rabbitMqConfig = configuration.GetRabbitMqSection().Get<RabbitMqConfig>();

            Configure<AbpRabbitMqOptions>(options =>
            {
                options.Connections.Default.UserName = rabbitMqConfig.UserName;
                options.Connections.Default.Password = rabbitMqConfig.Password;
                options.Connections.Default.HostName = rabbitMqConfig.VirtualHost;
                options.Connections.Default.Port = rabbitMqConfig.Port;
            });
            //Configure<AbpRabbitMqEventBusOptions>(options =>
            //{
            //    options.ClientName = rabbitMqConfig.EventBus.ClientName;
            //    options.ExchangeName = rabbitMqConfig.EventBus.ExchangeName;
            //});
        }
    }
}
