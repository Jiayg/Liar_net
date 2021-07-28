using System.IO;
using Microsoft.Extensions.Configuration;

namespace Liar.Domain.Shared
{
    /// <summary>
    /// appsettings.json配置文件数据读取类
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 配置文件的根节点
        /// </summary>
        private static readonly IConfigurationRoot _config;

        /// <summary>
        /// Constructor
        /// </summary>
        static AppSettings()
        {
            // 加载appsettings.json，并构建IConfigurationRoot
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", true, true);
            _config = builder.Build();
        }

        public static class Hangfire
        {
            public static string Login => _config["Hangfire:Login"];

            public static string Password => _config["Hangfire:Password"];
        }

        public static class JwtAuth
        {
            public static string Audience => _config["JwtAuth:Audience"];
            public static string Issuer => _config["JwtAuth:Issuer"];
            public static string SecurityKey => _config["JwtAuth:SecurityKey"];
            public static string TokenTime => _config["JwtAuth:TokenTime"];
        }

        /// <summary>
        /// EnableDb
        /// </summary>
        public static string DBType => _config["ConnectionStrings:DBType"];

        /// <summary>
        /// MainDb
        /// </summary>
        public static string Default => _config["ConnectionStrings:MainDb"];

        /// <summary>
        /// AbpIdentityServer
        /// </summary>
        public static string IdentityServer => _config["ConnectionStrings:AbpIdentityServer"];

        /// <summary>
        /// ConnectionStrings
        /// </summary>
        public static string ConnectionStrings => _config.GetConnectionString(Default);
    }
}
