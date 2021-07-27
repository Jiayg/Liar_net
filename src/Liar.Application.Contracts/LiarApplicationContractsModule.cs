using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(LiarDomainSharedModule)
    )]
    public class LiarApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
