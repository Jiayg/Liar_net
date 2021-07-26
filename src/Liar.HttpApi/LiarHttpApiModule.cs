using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        //typeof(AbpIdentityHttpApiModule),
        typeof(LiarApplicationContractsModule)
        )]
    public class LiarHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
