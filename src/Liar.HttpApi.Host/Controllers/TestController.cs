using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Application.EventBus;
using Liar.Core.Consts;
using Liar.Core.Helper;
using Liar.HttpApi.Authorize;
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
        private readonly TestBusPublish busPublish;

        public TestController(ILogger<TestController> logger, TestBusPublish busPublish)
        {
            this._logger = logger;
            this.busPublish = busPublish;
        }

        /// <summary>
        /// get testing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> get()
        {
            await busPublish.ChangeStockCountAsync("11111111", 0);

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
