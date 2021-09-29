using System;
using System.Linq.Expressions;
using System.Net;
using JetBrains.Annotations;
using Liar.Application.Contracts;
using Liar.Application.Contracts.ServiceResult;
using Liar.Core.Helper;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Services;

namespace Liar.Application
{
    public class AppService : ApplicationService, IAppService
    {
        /// <summary>
        /// 当前http请求对象
        /// </summary>
        public static HttpContext CurrentHttpContext => HttpContextUtility.GetCurrentHttpContext();

        public static AppSrvResult AppSrvResult() => new();

        public static AppSrvResult<TValue> AppSrvResult<TValue>([NotNull] TValue value) => new(value);

        public static ProblemDetails ProblemFail(HttpStatusCode statusCode, string detail) => new(statusCode, detail, null, null, null);

        public static ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null) => new ProblemDetails(statusCode, detail, title, instance, type);

    }
}
