using System.Collections.Generic;
using Liar.Core.Consts;
using Liar.Core.Helper;
using Liar.HttpApi.Shared.Authorize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Liar.HttpApi.Host.Controllers
{
    [Route("test")]
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = LiarApiVersionConsts.v2)]
    public class TestController
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// get testing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string get()
        {
            _logger.LogInformation("LogInformation");
            _logger.LogDebug("LogDebug");
            _logger.LogWarning("LogWarning");
            _logger.LogError("LogError");
            return "this is get request";
        }

        /// <summary>
        /// post testing
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string post()
        {
            return "this is post request";
        }

        /// <summary>
        /// 测试生成1w有序id
        /// </summary>
        /// <returns></returns>
        [HttpGet("Idgenerater")]
        public ActionResult<List<long>> NextId()
        {
            var ids = new List<long>();
            for (int i = 0; i < 10000; i++)
            {
                ids.Add(IdGenerater.GetNextId());
            }
            return ids;
        }
    }
}
