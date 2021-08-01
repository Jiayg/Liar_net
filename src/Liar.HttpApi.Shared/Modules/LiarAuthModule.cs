using Liar.HttpApi.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Liar
{ 
    [DependsOn(typeof(AbpAspNetCoreModule))]
    public class LiarAuthModule : AbpModule
    {
        /// <summary>
        /// 认证授权模块
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.AddAuthenticationSetup(configuration);
            context.Services.AddAuthorizationSetup();
        }
    }
}
