using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Core.Helper;
using Liar.Domain.Shared.ConfigModels;
using Liar.Liar.HttpApi.Host.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Liar.HttpApi.Host.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IAccountService _accountService;

        public AccountController(IOptionsSnapshot<JwtConfig> jwtConfig, IAccountService accountService)
        {
            this._jwtConfig = jwtConfig.Value;
            this._accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<UserTokenInfoDto>> LoginAsync([FromBody] UserLoginDto input)
        {
            var result = await _accountService.LoginAsync(input);

            return new UserTokenInfoDto
            {
                Token = JwtTokenHelper.CreateAccessToken(_jwtConfig, result.Content),
                RefreshToken = JwtTokenHelper.CreateRefreshToken(_jwtConfig, result.Content)
            };
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserInfoDto>> GetCurrentUserInfoAsync([FromRoute] long id)
        {
            return await _accountService.GetUserInfoAsync(id);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Logout()
        {
            return NoContent();
        }

        /// <summary>
        /// 生成1w 有序id
        /// </summary>
        /// <returns></returns>
        [HttpGet("IdGenerater")]
        [AllowAnonymous]
        public async Task<ActionResult<List<long>>> NextId()
        {
            var ids = new List<long>();
            for (int i = 0; i < 10000; i++)
            {
                ids.Add(IdGenerater.GetNextId());
            }
            return await Task.FromResult(ids);
        }
    }
}
