using Liar.HttpApi.Extensions;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(typeof(AbpAspNetCoreModule))]
    public class LiarConfigModule : AbpModule
    {
        /// <summary>
        /// 配置模型模块
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddConfiguration();
        }
    }
}
