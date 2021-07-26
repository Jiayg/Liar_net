using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(LiarDomainSharedModule)
    )]
    public class LiarDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
