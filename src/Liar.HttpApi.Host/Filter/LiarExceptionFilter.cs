using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Liar.HttpApi.Host.Filter
{
    public class LiarExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<LiarExceptionFilter> _logger;

        //private readonly ILog _log;
        public LiarExceptionFilter(ILogger<LiarExceptionFilter> logger)
        {
            this._logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            // 错误日志记录
            _logger.LogError($"{context.HttpContext.Request.Path}|{context.Exception.Message}", context.Exception);
        }
    }
}
