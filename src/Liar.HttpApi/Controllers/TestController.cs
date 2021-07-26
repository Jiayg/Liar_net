using Liar.Application.Contracts.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Liar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController
    {
        private readonly ITestService _testService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="testService"></param>
        public TestController(ITestService testService)
        {
            this._testService = testService;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        { 
            return _testService.get();
        }
    }
}
