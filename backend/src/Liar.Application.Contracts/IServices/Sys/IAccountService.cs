using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.ServiceResult;

namespace Liar.Application.Contracts.IServices.Sys
{
    public interface IAccountService : IAppService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppSrvResult<UserValidateDto>> LoginAsync(UserLoginDto input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserInfoDto> GetUserInfoAsync(long id);

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserValidateDto> GetUserValidateInfoAsync(long id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns> 
        Task<AppSrvResult> UpdatePasswordAsync(long id, UserChangePwdDto input);
    }
}
