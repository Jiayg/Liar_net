using Liar.Application.Contracts.ServiceResult;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    public abstract class BaseController : AbpController
    {
        [NonAction]
        protected virtual ObjectResult Problem(Application.Contracts.ServiceResult.ProblemDetails problemDetails)
        {
            problemDetails.Instance = problemDetails.Instance ?? this.Request.Path.ToString();
            return Problem(problemDetails.Detail
                , problemDetails.Instance
                , problemDetails.Status
                , problemDetails.Title
                , problemDetails.Type);
        }

        [NonAction]
        protected virtual ActionResult<TValue> Result<TValue>(AppSrvResult<TValue> appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return appSrvResult.Content;
            return Problem(appSrvResult.ProblemDetails);
        }

        /// <summary>
        /// return statuscode 204
        /// </summary>
        /// <param name="appSrvResult"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ActionResult Result(AppSrvResult appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return NoContent();
            return Problem(appSrvResult.ProblemDetails);
        }

        /// <summary>
        ///  return statuscode 201
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="appSrvResult"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ActionResult<TValue> CreatedResult<TValue>(AppSrvResult<TValue> appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return Created(this.Request.Path, appSrvResult.Content);
            return Problem(appSrvResult.ProblemDetails);
        }
    }
}
