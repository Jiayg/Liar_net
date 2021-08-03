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
            // 高并发情况下注册为单列模式 否则在生产使用中可能会误用在瞬态请求中实例化，导致redis客户端几天之后被占满
            // 非单例模式下CSRedisCore会默认建立连接池，预热50个连接
            context.Services.TryAddSingleton(typeof(IRedisServiceResolver), typeof(RedisServiceResolver));

            // 这里不要注册为单例，否则无法热更新（获取到配置文件中的连接字符串）
            context.Services.TryAddSingleton(serviceProvider =>
            {
                var redisServiceResolver = serviceProvider.GetService(typeof(IRedisServiceResolver)) as IRedisServiceResolver;
                return redisServiceResolver.Default();
            });
        }
    }
}
