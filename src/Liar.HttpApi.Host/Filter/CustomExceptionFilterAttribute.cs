using System.Threading.Tasks;
using Liar.Core.Extensions;
using Liar.Domain.Shared.UserContext;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var status = 500;
            var exception = context.Exception;
            var eventId = new EventId(exception.HResult);
            var userContext = context.HttpContext.RequestServices.GetService<IUserContext>();
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            //string className = descriptor.ControllerName;
            //string method = descriptor.ActionName;
            var hostAndPort = context.HttpContext.Request.Host.HasValue ? context.HttpContext.Request.Host.Value : string.Empty;
            var requestUrl = string.Concat(hostAndPort, context.HttpContext.Request.Path);
            var type = string.Concat("https://httpstatuses.com/", status);

            string title;
            string detial;
            if (exception is IAdncException)
            {
                title = "参数错误";
                detial = exception.Message;
            }
            else
            {
                title = $"系统异常";
                detial = $"系统异常,请联系管理员({eventId})";
            }

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Detail = detial,
                Type = type,
                Status = status,
                Instance = requestUrl
            };

            context.Result = new ObjectResult(problemDetails) { StatusCode = status };
            context.ExceptionHandled = true;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}
