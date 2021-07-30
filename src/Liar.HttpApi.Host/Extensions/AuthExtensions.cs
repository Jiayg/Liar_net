using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared.ConfigModels;
using Liar.Domain.Shared.UserContext;
using Liar.HttpApi.Host.Authorize;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Liar.HttpApi.Host.Extensions
{
    public static class AuthExtensions
    {
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
        }

        /// <summary>
        /// 认证授权
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permission.Policy, policy => policy.Requirements.Add(new HttpApi.Host.Authorize.PermissionRequirement()));
            });

            //context.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
        }
    }
}
