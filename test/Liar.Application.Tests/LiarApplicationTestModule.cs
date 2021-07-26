using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(LiarApplicationModule),
        typeof(LiarDomainTestModule)
        )]
    public class LiarApplicationTestModule : AbpModule
    {

    }
}