using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Liar.EntityFrameworkCore;
using Liar.HangFire;
using Liar.HttpApi.Host.Filter;
using Liar.HttpApi.Host.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Liar
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(LiarApplicationModule),
        typeof(LiarEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpSwashbuckleModule)
    //typeof(LiarHangFireModule)
    )]
    public class LiarHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = true;
            });

            Configure<MvcOptions>(options =>
            {
                var filterMetadata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute && attribute.ServiceType.Equals(typeof(AbpExceptionFilter)));

                // 移除 AbpExceptionFilter
                options.Filters.Remove(filterMetadata);

                // 添加自己实现的 LiarExceptionFilter
                options.Filters.Add(typeof(LiarExceptionFilter));
            });

            // 跨域配置
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            // 路由配置
            context.Services.AddRouting(options =>
            {
                // 设置URL为小写
                options.LowercaseUrls = true;
                // 在生成的URL后面添加斜杠
                options.AppendTrailingSlash = true;
            });

            // 认证授权
            context.Services.AddAuthorization();

            context.Services.AddAbpSwaggerGenWithOAuth(
               configuration["AuthServer:Authority"],
               new Dictionary<string, string>
               {
                    {"Liar", "Liar API"}
               },
               options =>
               {
                   options.SwaggerDoc("v1", new OpenApiInfo { Title = "Liar API", Version = "v1" });
                   options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.HttpApi.Host.xml"));
                   options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.Application.Contracts.xml"));
                   //options.DocInclusionPredicate((docName, description) => true);
                   //options.CustomSchemaIds(type => type.FullName);
               });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment())
            {
                //app.UseErrorPage();
            }


            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();

            app.UseUnitOfWork();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseAbpSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Liar API");

                var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
                c.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                c.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                c.OAuthScopes("Liar");
            });

            app.UseAuditing();
            //app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
