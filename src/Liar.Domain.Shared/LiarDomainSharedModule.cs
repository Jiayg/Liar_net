using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn()]
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
