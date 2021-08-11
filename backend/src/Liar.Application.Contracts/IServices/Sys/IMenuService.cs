using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.Menu;
using Liar.Application.Contracts.ServiceResult;

namespace Liar.Application.Contracts.IServices.Sys
{
    public interface IMenuService : IAppService
    {
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<AppSrvResult<long>> CreateAsync(MenuCreationDto input);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<AppSrvResult> UpdateAsync(long id, MenuUpdationDto input);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        Task<AppSrvResult> DeleteAsync(long id);

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns> 
        Task<List<MenuNodeDto>> GetlistAsync();

        /// <summary>
        /// 获取左侧路由菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<List<MenuRouterDto>> GetMenusForRouterAsync(List<long> roleIds);

        /// <summary>
        /// 获取指定角色的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<MenuTreeDto> GetMenuTreeListByRoleIdAsync(long roleId);
    }
}
