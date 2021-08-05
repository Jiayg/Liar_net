using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(AbpEventBusAbstractionsModule),
        typeof(AbpEventBusModule))
        ]
    public class LiarEventbusModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
