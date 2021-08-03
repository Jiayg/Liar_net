using Liar.Core.Consts;
using Liar.Core.Helper;
using Liar.HttpApi.Shared.Authorize;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Liar.HttpApi.Host.Controllers
{
    [Route("test")]
    [ApiController]
    [ApiExplorerSettings(GroupName = LiarApiVersionConsts.v2)]
    public class TestController
    {
        /// <summary>
        /// get testing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Permission("testget")]
        public string get()
        {
            return "this is get request";
        }

        /// <summary>
        /// post testing
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission("testpost")]
        public string post()
        {
            return "this is post request";
        }

        /// <summary>
        /// 测试生成1w有序id
        /// </summary>
        /// <returns></returns>
        [HttpGet("Idgenerater")]
        [Permission("testid")]
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
