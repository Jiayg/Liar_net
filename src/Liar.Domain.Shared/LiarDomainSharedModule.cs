using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(AbpIdentityDomainSharedModule)
        )]
    public class LiarDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
