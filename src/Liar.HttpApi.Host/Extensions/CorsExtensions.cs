using System;
using Liar.Core.Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Liar.HttpApi.Host.Extensions
{
    /// <summary>
    /// Cors 启动服务
    /// </summary>
    public static class CorsExtensions
    {
        public static void AddCorsSetup(this IServiceCollection services, IConfiguration configuration)
        {
            // 跨域配置
            services.AddCors(options =>
            {
                var _corsHosts = configuration.GetAllowCorsHosts().Split(",", StringSplitOptions.RemoveEmptyEntries);
                options.AddPolicy("Default", builder =>
                {
                    builder
                        .WithOrigins(_corsHosts)
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}
