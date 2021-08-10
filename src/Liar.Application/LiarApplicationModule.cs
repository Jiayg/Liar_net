using Liar.Application.HostedService;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(LiarApplicationContractsModule),
        typeof(LiarApplicationCachingModule)
        )]
    public class LiarApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<LiarApplicationAutoMapperProfile>(validate: false);
            });

            context.Services.AddHostedService<ChannelConsumersHostedService>();
        }
    }
}
