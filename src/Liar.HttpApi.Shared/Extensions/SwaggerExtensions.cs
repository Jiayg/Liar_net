using Liar.Core.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;

namespace Liar.HttpApi.Shared.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                ApiInfoHelper.ApiInfos.ForEach(x =>
                {
                    options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo);
                });


                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.HttpApi.Host.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.Application.Contracts.xml"));
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);

            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // 遍历分组信息，生成Json
                ApiInfoHelper.ApiInfos.ForEach(x =>
                {
                    options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
                });
                // 模型的默认扩展深度，设置为 -1 完全隐藏模型
                options.DefaultModelsExpandDepth(-1);
                // API文档仅展开标记
                options.DocExpansion(DocExpansion.List);
                // API前缀设置为空
                //options.RoutePrefix = string.Empty;
                // API页面Title
                options.DocumentTitle = "Liar Api Document!";
            });
        }
    }
}
