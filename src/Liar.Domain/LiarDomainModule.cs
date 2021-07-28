using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(LiarDomainSharedModule)
    )]
    public class LiarDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        { 
        }
    }
}
