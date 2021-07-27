using System;
using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using JetBrains.Annotations;
using Liar.Application.Contracts;
using Volo.Abp.Application.Services;

namespace Liar.Application
{
    public class LiarAppService : ApplicationService, ILiarAppService
    {
        public IObjectMapper Mapper { get; set; }

        protected AppSrvResult AppSrvResult()
        {
            return new AppSrvResult();
        }

        protected AppSrvResult<TValue> AppSrvResult<TValue>([NotNull] TValue value)
        {
            return new AppSrvResult<TValue>(value);
        }

        protected ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null)
        {
            return new ProblemDetails(statusCode, detail, title, instance, type);
        }

        protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions)
        {
            return expressions;
        }
    }
}
