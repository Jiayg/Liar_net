using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Liar.EntityFrameworkCore
{
    [DependsOn(
        typeof(LiarEntityFrameworkCoreModule)
        )]
    public class LiarEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LiarMigrationsDbContext>();
        }
    }
}
