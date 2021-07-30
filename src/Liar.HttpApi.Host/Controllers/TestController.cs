using System.Collections.Generic;
using Liar.Core.Helper;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : AbpController
    {
        /// <summary>
        /// get testing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string get()
        {
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
