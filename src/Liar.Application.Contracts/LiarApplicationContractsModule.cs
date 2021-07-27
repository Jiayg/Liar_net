using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(LiarDomainSharedModule),
        typeof(AbpFluentValidationModule)
    )]
    public class LiarApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
