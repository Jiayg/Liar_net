using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Liar.Domain.Shared;
using Liar.Domain.Shared.UserContext;
using Liar.EntityFrameworkCore;
using Liar.HttpApi.Host.Authorize;
using Liar.HttpApi.Host.Filter;
using Liar.HttpApi.Host.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
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

            // 接口可视化
            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Learn API", Version = "v1" });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.HttpApi.Host.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Liar.Application.Contracts.xml"));
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
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

            var Issuer = AppSettings.JwtAuth.Issuer;
            var Audience = AppSettings.JwtAuth.Audience;

            context.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    //验证的一些设置，比如是否验证发布者，订阅者，密钥，以及生命时间等等
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.JwtAuth.SecurityKey)),
                        ValidateAudience = true,
                        ValidAudience = Audience,//订阅人
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(30)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        //接受到消息时调用
                        OnMessageReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            //如果是过期，在http heard中加入act参数
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("act", "expired");
                            }
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var userContext = context.HttpContext.RequestServices.GetService<IUserContext>();
                            var claims = context.Principal.Claims;
                            userContext.Id = long.Parse(claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
                            userContext.Account = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                            userContext.Name = claims.First(x => x.Type == ClaimTypes.Name).Value;
                            //userContext.Email = claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                            //string[] roleIds = claims.First(x => x.Type == ClaimTypes.Role).Value.Split(",", StringSplitOptions.RemoveEmptyEntries);
                            userContext.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                            return Task.CompletedTask;
                        }
                    };

                    //因为获取声明的方式默认是走微软定义的一套映射方式，如果我们想要走JWT映射声明，那么我们需要将默认映射方式给移除掉
                    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                });

            // 认证授权
            context.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Permission.Policy, policy => policy.Requirements.Add(new HttpApi.Host.Authorize.PermissionRequirement()));
            });

            //context.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
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

            app.UseCors(DefaultCorsPolicyName);

            app.UseSwagger();
            app.UseAbpSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Liar API");

                var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
                c.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                c.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                c.OAuthScopes("Liar");
            });

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseUnitOfWork();
            app.UseAuthorization();

            app.UseAuditing();

            app.UseConfiguredEndpoints();
        }
    }
}
