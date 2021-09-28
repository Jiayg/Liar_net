using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Application.EventBus;
using Liar.Core.Consts;
using Liar.Core.Helper;
using Liar.HttpApi.Authorize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly TestBusPublish _busPublish;

        public TestController(ILogger<TestController> logger, TestBusPublish busPublish)
        {
            this._logger = logger;
            this._busPublish = busPublish;
        }

        /// <summary>
        /// get testing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {
            await _busPublish.ChangeStockCountAsync("11111111", 0);

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
        public string Post()
        {
            return "this is post request";
        }
 
        /// <summary>
        /// 测试生成10w有序id
        /// </summary>
        /// <returns></returns>
        [HttpGet("Generator_ID")]
        public ActionResult<List<long>> NextId([FromRoute] int count)
        {
            List<long> ids = new List<long>();
            
            if (count <= 0) 
                return ids;
            
            for (int i = 0; i < count; i++)
            {
                ids.Add(IdGenerater.GetNextId());
            }
            
            return ids;
        }
    }
}
