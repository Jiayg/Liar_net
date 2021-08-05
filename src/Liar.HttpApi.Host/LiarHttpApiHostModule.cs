using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using Abp.AspNetCore.Mvc.ExceptionHandling;
using Liar.Core.Helper;
using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared.UserContext;
using Liar.EntityFrameworkCore;
using Liar.HttpApi.Host.Authorize;
using Liar.HttpApi.Shared.Extensions;
using Liar.HttpApi.Shared.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(LiarApplicationModule),
        typeof(LiarConfigModule),
        typeof(LiarSwaggerModule),
        typeof(LiarEntityFrameworkCoreDbMigrationsModule)
    )]
    public class LiarHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            // 错误消息发送到客户端
            Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = true;
            });

            context.Services.AddHttpContextAccessor();
            context.Services.AddMemoryCache();

            // 默认拦截
            context.Services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                                options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                                options.JsonSerializerOptions.Encoder = SystemTextJsonHelper.GetAdncDefaultEncoder();
                                //该值指示是否允许、不允许或跳过注释。
                                options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                                //dynamic与匿名类型序列化设置
                                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                                //dynamic
                                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                                //匿名类型
                                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                            });
            context.Services.Configure<ApiBehaviorOptions>(options =>
            {
                //关闭自动验证
                //options.SuppressModelStateInvalidFilter = true;
                //格式化验证信息
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var problemDetails = new ProblemDetails
                    {
                        Detail = context.ModelState.GetValidationSummary("<br>"),
                        Title = "参数错误",
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "https://httpstatuses.com/400",
                        Instance = context.HttpContext.Request.Path
                    };

                    return new ObjectResult(problemDetails)
                    {
                        StatusCode = problemDetails.Status
                    };
                };
            });

            // 跨域配置
            context.Services.AddCors(options =>
            {
                var _corsHosts = configuration.GetAllowCorsHosts().Split(",", StringSplitOptions.RemoveEmptyEntries);
                options.AddPolicy(DefaultCorsPolicyName, builder =>
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

            context.Services.AddAuthenticationSetup(configuration);
            context.Services.AddAuthorization<PermissionHandlerLocal>();

            // 路由配置
            context.Services.AddRouting(options =>
            {
                // 设置URL为小写
                options.LowercaseUrls = true;
                // 在生成的URL后面添加斜杠
                options.AppendTrailingSlash = true;
            });

            context.Services.AddSingleton<IUserContext, UserContext>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);

            app.UseStaticFiles();

            app.UseCustomExceptionHandler();

            app.UseRealIp(x =>
            {
                x.HeaderKeys = new string[] { "X-Forwarded-For", "X-Real-IP" };
            });

            app.UseCors(DefaultCorsPolicyName);

            app.UseUnitOfWork();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // 路由映射
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
