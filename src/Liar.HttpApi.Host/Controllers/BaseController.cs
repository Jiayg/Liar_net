using Liar.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using ProblemDetails = Liar.Application.Contracts.ProblemDetails;

namespace Liar.HttpApi.Host.Controllers
{
    public class BaseController : AbpController
    {
        [NonAction]
        protected virtual ObjectResult Problem(ProblemDetails problemDetails)
        {
            problemDetails.Instance = problemDetails.Instance ?? this.Request.Path.ToString();
            return Problem(problemDetails.Detail
                , problemDetails.Instance
                , problemDetails.Status
                , problemDetails.Title
                , problemDetails.Type);
        }

        //[NonAction]
        //protected virtual ObjectResult Problem(Refit.ApiException exception)
        //{
        //    var problemDetails = ((Refit.ValidationApiException)exception).Content;

        //    return Problem(problemDetails.Detail
        //            , problemDetails.Instance
        //            , problemDetails.Status
        //            , problemDetails.Title
        //            , problemDetails.Type);
        //}

        [NonAction]
        protected virtual ActionResult<TValue> Result<TValue>(AppSrvResult<TValue> appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return appSrvResult.Content;
            return Problem(appSrvResult.ProblemDetails);
        }

        [NonAction]
        protected virtual ActionResult Result(AppSrvResult appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return NoContent();
            return Problem(appSrvResult.ProblemDetails);
        }

        [NonAction]
        protected virtual ActionResult<TValue> CreatedResult<TValue>(AppSrvResult<TValue> appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return Created(this.Request.Path, appSrvResult.Content);
            return Problem(appSrvResult.ProblemDetails);
        }
    }
}
