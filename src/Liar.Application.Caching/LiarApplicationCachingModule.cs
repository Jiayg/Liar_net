using Liar.Caching.Abstractions;
using Liar.Core.Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(LiarCachingModule), 
        typeof(LiarApplicationContractsModule),
        typeof(LiarDomainSharedModule),
        typeof(LiarDomainModule)
    )]
    public class LiarApplicationCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.Configure<RedisOptions>(configuration.GetRedisSection()); 
        }
    }
}
