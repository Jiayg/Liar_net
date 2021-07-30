using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Liar.HttpApi.Host.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn API", Version = "v1" });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.HttpApi.Host.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.Application.Contracts.xml"));
                //options.DocInclusionPredicate((docName, description) => true);
                //options.CustomSchemaIds(type => type.FullName);
                // 开启加权小锁
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                // Jwt Bearer 认证，必须是 oauth2
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });

            //services.AddAbpSwaggerGenWithOAuth(
            //   configuration["AuthServer:Authority"],
            //   new Dictionary<string, string>
            //   {
            //        {"Liar", "Liar API"}
            //   },
            //   options =>
            //   {
            //       options.SwaggerDoc("v1", new OpenApiInfo { Title = "Liar API", Version = "v1" });
            //       options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.HttpApi.Host.xml"));
            //       options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.Application.Contracts.xml"));
            //       //options.DocInclusionPredicate((docName, description) => true);
            //       //options.CustomSchemaIds(type => type.FullName);
            //   });

            // 接口可视化
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn API", Version = "v1" });
            //    // 采用bearer token认证
            //    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer",
            //        BearerFormat = "JWT",
            //        In = ParameterLocation.Header,
            //        Description = "JWT Authorization header using the Bearer scheme."
            //    });
            //    //设置全局认证
            //    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    //{
            //    //    {
            //    //        new OpenApiSecurityScheme
            //    //        {
            //    //            Reference = new OpenApiReference
            //    //            {
            //    //                Type = ReferenceType.SecurityScheme,
            //    //                Id = "Bearer"
            //    //            }
            //    //        },
            //    //        new string[] {}
            //    //    }
            //    //});

            //    options.OperationFilter<AddResponseHeadersFilter>();
            //    options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            //    options.OperationFilter<SecurityRequirementsOperationFilter>();

            //    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.HttpApi.Host.xml"));
            //    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.Application.Contracts.xml"));
            //    //options.DocInclusionPredicate((docName, description) => true);
            //    //options.CustomSchemaIds(type => type.FullName); 

            //});
        }
    }
}
