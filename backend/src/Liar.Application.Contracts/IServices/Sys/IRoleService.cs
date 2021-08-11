using Liar.Application.Contracts.Dtos.Sys.Role;
using Liar.Application.Contracts.ServiceResult;
using Liar.Domain.Shared;
using System.Threading.Tasks;
using Volo.Abp.Uow;

namespace Liar.Application.Contracts.IServices.Sys
{
    public interface IRoleService : IAppService
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<AppSrvResult<long>> CreateAsync(RoleCreationDto input);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<AppSrvResult> UpdateAsync(long id, RoleUpdationDto input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        Task<AppSrvResult> DeleteAsync(long id);

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns> 
        [UnitOfWork]
        Task<AppSrvResult> SetPermissonsAsync(RoleSetPermissonsDto input);

        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RoleTreeDto> GetRoleTreeListByUserIdAsync(long userId);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageModelDto<RoleDto>> GetPagedAsync(RolePagedSearchDto input);
    }
}
