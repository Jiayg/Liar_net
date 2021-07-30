using Microsoft.Extensions.DependencyInjection;

namespace Liar.HttpApi.Host.Extensions
{
    public static class RoutingExtensions
    {
        public static void AddRoutingSetup(this IServiceCollection services)
        {
            // 路由配置
            services.AddRouting(options =>
            {
                // 设置URL为小写
                options.LowercaseUrls = true;
                // 在生成的URL后面添加斜杠
                options.AppendTrailingSlash = true;
            });
        }
    }
}
