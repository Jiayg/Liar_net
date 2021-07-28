using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Application.Contracts;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Core.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    [Route("sys/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResultDetails<UserInfoDto>> GetCurrentUserInfoAsync([FromRoute] long id)
        {
            return await _accountService.GetUserInfoAsync(id);
        }

        /// <summary>
        /// 生成1w 有序id
        /// </summary>
        /// <returns></returns>
        [HttpGet("IdGenerater")]
        public async Task<ResultDetails<List<long>>> NextId()
        {
            var ids = new List<long>();
            for (int i = 0; i < 10000; i++)
            {
                ids.Add(IdGenerater.GetNextId());
            }
            return Result(await Task.FromResult(ids));
        }
    }
}
