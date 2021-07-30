using System.Threading.Tasks;
using Liar.Application.Contracts;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices;
using Liar.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Liar.HttpApi.Host.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("sys/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> CreateAsync([FromBody] UserCreationDto input)
        {
            return CreatedResult(await _userService.CreateAsync(input));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] long id)
        {
            return Result(await _userService.DeleteAsync(id));
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PageModelDto<UserDto>>> GetPagedAsync([FromQuery] UserSearchPagedDto search)
        {
            return await _userService.GetPagedAsync(search);
        }
    }
}
