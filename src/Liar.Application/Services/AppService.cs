using System;
using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using JetBrains.Annotations;
using Liar.Application.Contracts;
using Volo.Abp.Application.Services;

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

        public ResultDetails Fail(string msg)
        {
            return new ResultDetails((int)HttpStatusCode.NoContent, true, default, msg);
        }

        public ResultDetails Fail(HttpStatusCode? httpCode, string msg)
        {
            return new ResultDetails((int)httpCode.Value, true, default, msg);
        }
    }
}
