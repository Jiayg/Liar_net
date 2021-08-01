using Liar.HttpApi.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(typeof(AbpAspNetCoreModule))]
    public class LiarSwaggerModule : AbpModule
    {
        /// <summary>
        /// 接口可视化文档模块
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwagger();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.GetApplicationBuilder().UseSwagger().UseSwaggerUI();
        }
    }
}
