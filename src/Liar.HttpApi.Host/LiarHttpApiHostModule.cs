using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Liar.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Liar
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(LiarHttpApiModule),
        typeof(LiarApplicationModule),
        typeof(LiarEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpSwashbuckleModule)
    )]
    public class LiarHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureUrls(configuration);
            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context, configuration);
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
                options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
            });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<LiarDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Liar.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<LiarDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Liar.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<LiarApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Liar.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<LiarApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}Liar.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(LiarApplicationModule).Assembly);
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {

        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                    {"Liar", "Liar API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Liar API", Version = "v1" });
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.HttpApi.xml"));
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.Application.Contracts.xml"));
                    //options.DocInclusionPredicate((docName, description) => true);
                    //options.CustomSchemaIds(type => type.FullName);
                });
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
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
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                //app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
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
