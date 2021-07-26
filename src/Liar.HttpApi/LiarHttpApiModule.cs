using Localization.Resources.AbpUi;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(AbpIdentityHttpApiModule),
        typeof(LiarApplicationContractsModule)
        )]
    public class LiarHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        { 
        } 
    }
}
