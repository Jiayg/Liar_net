using Liar.Caching.Abstractions;
using Liar.Caching.CsRedis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Modularity;

namespace Liar.Caching
{
    public class LiarCachingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddSingleton(typeof(IRedisServiceResolver), typeof(RedisServiceResolver));
            // 这里不要注册为单例，否则无法热更新
            context.Services.TryAddTransient(serviceProvider =>
            {
                var redisServiceResolver = serviceProvider.GetService(typeof(IRedisServiceResolver)) as IRedisServiceResolver;
                return redisServiceResolver.Default();
            });
        }
    }
}
