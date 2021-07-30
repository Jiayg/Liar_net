using System.Linq;
using System.Text.Json;
using Liar.Core.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;

namespace Liar.HttpApi.Host.Extensions
{
    public static class ControllersExtensions
    {
        public static void AddControllersSetup(this IServiceCollection services)
        {
            services.AddControllers(options =>
            { 
                var filterMetadata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute && attribute.ServiceType.Equals(typeof(AbpExceptionFilter)));

                // 移除 AbpExceptionFilter
                //options.Filters.Remove(abpfilter);
                options.Filters.Remove(filterMetadata);
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
            }).AddJsonOptions(options =>
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
        }
    }
}
