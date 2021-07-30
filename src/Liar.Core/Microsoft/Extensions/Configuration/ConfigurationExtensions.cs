using Microsoft.Extensions.Configuration;

namespace Liar.Core.Microsoft.Extensions.Configuration
{
    public static partial class ConfigurationExtensions
    {

        /// <summary>
        /// 获取跨域配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GetAllowCorsHosts(this IConfiguration configuration)
        {
            return configuration.GetValue("CorsHosts", string.Empty);
        }

        /// <summary>
        /// 获取mq配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfigurationSection GetRabbitMqSection(this IConfiguration configuration)
        {
            return configuration.GetSection("RabbitMq");
        }

        /// <summary>
        /// 获取redis配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfigurationSection GetRedisSection(this IConfiguration configuration)
        {
            return configuration.GetSection("Redis");
        }

        /// <summary>
        /// 获取数据库配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfigurationSection GetMysqlSection(this IConfiguration configuration)
        {
            return configuration.GetSection("DateBase");
        }

        /// <summary>
        /// 获取Mongo配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfigurationSection GetMongoDbSection(this IConfiguration configuration)
        {
            return configuration.GetSection("MongoDb");
        }

        /// <summary>
        /// 获取JWT配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfigurationSection GetJWTSection(this IConfiguration configuration)
        {
            return configuration.GetSection("JWT");
        }

        public static IConfigurationSection GetThreadPoolSettingsSection(this IConfiguration configuration)
        {
            return configuration.GetSection("ThreadPoolSettings");
        }
    }
}
