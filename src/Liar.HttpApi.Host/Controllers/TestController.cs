using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    [ApiController]
    public class TestController : AbpController
    {
        [HttpGet]

        [Route("testget")]
        public string get()
        {
            return "get hello world";
        }


        [HttpPost]
        [Route("testpost")]
        public string post()
        {
            return "post hello world";
        }

    }
}
