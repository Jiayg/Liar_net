using Liar.Application.Contracts.Dtos.Sys.Menu;
using System.Threading.Tasks;
using Liar.Application.Contracts.IServices.Sys;
using Liar.HttpApi.Host.Authorize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Liar.Application.Services.Sys;
using System.Linq;

namespace Liar.HttpApi.Host.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("usr/menus")]
    [ApiController]
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;
        private readonly IAccountService _accountService;

        public MenuController(IMenuService menuService, IAccountService accountService)
        {
            this._menuService = menuService;
            this._accountService = accountService;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menuDto">菜单</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> CreateAsync([FromBody] MenuCreationDto menuDto)
        {
            return CreatedResult(await _menuService.CreateAsync(menuDto));
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input">菜单</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateAsync([FromRoute] long id, [FromBody] MenuUpdationDto input)
        {
            return Result(await _menuService.UpdateAsync(id, input));
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAsync([FromRoute] long id)
        {
            return Result(await _menuService.DeleteAsync(id));
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MenuNodeDto>>> GetlistAsync()
        {
            return await _menuService.GetlistAsync();
        }

        /// <summary>
        /// 获取侧边栏路由菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("routers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MenuRouterDto>>> GetMenusForRouterAsync()
        {
            long id = 0;
            var userValidateInfo = await _accountService.GetUserValidateInfoAsync(id);
            var roleIds = userValidateInfo.RoleIds.Split(",", System.StringSplitOptions.RemoveEmptyEntries).ToList();
            return await _menuService.GetMenusForRouterAsync(roleIds.Select(x => long.Parse(x)));
        }

        /// <summary>
        /// 根据角色获取菜单树
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns> 
        [HttpGet("{roleId}/menutree")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MenuTreeDto>> GetMenuTreeListByRoleIdAsync([FromRoute] long roleId)
        {
            return await _menuService.GetMenuTreeListByRoleIdAsync(roleId);
        }
    }
}
