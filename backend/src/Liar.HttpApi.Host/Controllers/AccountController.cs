using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Domain.Shared.ConfigModels;
using Liar.Domain.Shared.Dtos;
using Liar.Domain.Shared.UserContext;
using Liar.HttpApi.Host.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;

namespace Liar.HttpApi.Host.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IAccountService _accountService;
        private readonly IUserContext _userContext;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IOptionsSnapshot<JwtConfig> jwtConfig, IAccountService accountService, IUserContext userContext, ILogger<AccountController> logger)
        {
            this._jwtConfig = jwtConfig.Value;
            this._accountService = accountService;
            this._userContext = userContext;
            this._logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserTokenInfoDto>> LoginAsync([FromBody] UserLoginDto input)
        {
            var result = await _accountService.LoginAsync(input);

            if (result.IsSuccess)
                return Created($"/session"
                        ,
                        new UserTokenInfoDto
                        {
                            Token = JwtTokenHelper.CreateAccessToken(_jwtConfig, result.Content),
                            RefreshToken = JwtTokenHelper.CreateRefreshToken(_jwtConfig, result.Content)
                        });

            return Problem(result.ProblemDetails);
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserInfoDto>> GetCurrentUserInfoAsync()
        {
            var result = await _accountService.GetUserInfoAsync(_userContext.Id);

            if (result != null)
                return result;

            return NotFound();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Logout()
        {
            return NoContent();
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserTokenInfoDto>> RefreshAccessTokenAsync([FromBody] UserRefreshTokenDto input)
        {
            var result = await _accountService.GetUserValidateInfoAsync(input.Id);

            if (result == null)
                return Ok(new UserTokenInfoDto
                {
                    Token = JwtTokenHelper.CreateAccessToken(_jwtConfig, result, input.RefreshToken),
                    RefreshToken = input.RefreshToken
                });

            return NotFound();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ChangePassword([FromBody] UserChangePwdDto input)
        {
            return Result(await _accountService.UpdatePasswordAsync(_userContext.Id, input));
        }
    }
}
