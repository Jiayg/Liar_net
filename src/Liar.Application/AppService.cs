using System;
using System.Linq.Expressions;
using System.Net;
using JetBrains.Annotations;
using Liar.Application.Contracts;
using Liar.Application.Contracts.ServiceResult;
using Volo.Abp.Application.Services;
using ProblemDetails = Liar.Application.Contracts.ServiceResult.ProblemDetails;

namespace Liar.Application
{
    public class AppService : ApplicationService, IAppService
    {
        public AppSrvResult AppSrvResult()
        {
            return new AppSrvResult();
        }

        public AppSrvResult<TValue> AppSrvResult<TValue>([NotNull] TValue value)
        {
            return new AppSrvResult<TValue>(value);
        }

        public ProblemDetails ProblemFail(HttpStatusCode statusCode, string detail)
        {

            return new ProblemDetails(statusCode, detail, null, null, null);
        }
        public ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null)
        {
            return new ProblemDetails(statusCode, detail, title, instance, type);
        }

        public Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions)
        {
            return expressions;
        }
    }
}
