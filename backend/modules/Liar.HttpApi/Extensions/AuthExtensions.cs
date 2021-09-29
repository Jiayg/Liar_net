using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared.ConfigModels;
using Liar.Domain.Shared.UserContext;
using Liar.HttpApi.Authorize;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Liar.HttpApi.Extensions
{
    public static class AuthExtensions
    {
        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var jwtConfig = configuration.GetJWTSection().Get<JwtConfig>();

            services.AddAuthentication(options =>
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
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SymmetricSecurityKey)),
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,//订阅人
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
                    OnChallenge = async context =>
                    {
                        // 跳过默认的处理逻辑，返回下面的模型数据
                        context.HandleResponse();

                        var status = (int)HttpStatusCode.Unauthorized;
                        var hostAndPort = context.Request.Host.HasValue ? context.Request.Host.Value : string.Empty;
                        var requestUrl = string.Concat(hostAndPort, context.Request.Path);
                        var type = string.Concat("https://httpstatuses.com/", status);
                        var title = "请求失败";
                        var detial = "获取授权失败,请先登录！";
                        var problemDetails = new ProblemDetails
                        {
                            Title = title
                            ,
                            Detail = detial
                            ,
                            Type = type
                            ,
                            Status = status
                            ,
                            Instance = requestUrl
                        };

                        context.Response.ContentType = "application/problem+json";
                        context.Response.StatusCode = status;

                        var stream = context.Response.Body;
                        await JsonSerializer.SerializeAsync(stream, problemDetails);
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

                        userContext.Id = long.Parse(claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                        userContext.Account = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                        userContext.Name = claims.First(x => x.Type == ClaimTypes.Name).Value;
                        userContext.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                        return Task.CompletedTask;
                    }
                };
                //因为获取声明的方式默认是走微软定义的一套映射方式，如果我们想要走JWT映射声明，那么我们需要将默认映射方式给移除掉
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            });
        }

        /// <summary>
        /// 认证授权
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorization<THandler>(this IServiceCollection services) where THandler : PermissionHandler
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permission.Policy, policy => policy.Requirements.Add(new PermissionRequirement()));
            });
            services.AddScoped<IAuthorizationHandler, THandler>();
        }
    }
}
