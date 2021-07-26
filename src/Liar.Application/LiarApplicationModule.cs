using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn( 
        typeof(LiarApplicationContractsModule),
        typeof(LiarDomainModule)
        )]
    public class LiarApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<LiarApplicationModule>();
            });
        }
    }
}
