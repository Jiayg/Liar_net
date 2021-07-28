using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liar.Application.Contracts;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
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
        public async Task<ActionResult<UserInfoDto>> GetCurrentUserInfoAsync([FromRoute] long id)
        {
            return Result(await _accountService.GetUserInfoAsync(id));
        }
    }
}
