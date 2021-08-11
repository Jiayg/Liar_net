using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.ServiceResult;
using Liar.Domain.Shared;

namespace Liar.Application.Contracts.IServices
{
    public interface IUserService : IAppService
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<long>> CreateAsync(UserCreationDto input);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> UpdateAsync(long id, UserUpdationDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppSrvResult> DeleteAsync(long id);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult> SetRoleAsync(long id, UserSetRoleDto input);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<AppSrvResult> ChangeStatusAsync(long id, int status);

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<AppSrvResult> ChangeStatusAsync(IEnumerable<long> id, int status);

        /// <summary>
        /// 获取当前用户是否拥有指定权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        Task<List<string>> GetPermissionsAsync(long userId, IEnumerable<string> permissions);

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<PageModelDto<UserDto>> GetPagedAsync(UserSearchPagedDto search);
    }
}
