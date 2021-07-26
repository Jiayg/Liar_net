using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace Liar
{
    [DependsOn(
        typeof(LiarDomainSharedModule),
        typeof(AbpObjectExtendingModule)
    )]
    public class LiarApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
