using Liar.Caching.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Liar.Application.Caching
{
    public class TestCachingService : ITransientDependency
    {
        public readonly IRedisService redisService;
        public TestCachingService(IRedisServiceResolver redisServiceResolver)
        {
            redisService = redisServiceResolver.Default();
        }
        public string getLong()
        {
            return redisService.Get("testkey");
        }
    }
}
