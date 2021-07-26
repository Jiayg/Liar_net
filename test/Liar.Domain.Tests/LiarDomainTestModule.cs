using Liar.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(LiarEntityFrameworkCoreTestModule)
        )]
    public class LiarDomainTestModule : AbpModule
    {

    }
}