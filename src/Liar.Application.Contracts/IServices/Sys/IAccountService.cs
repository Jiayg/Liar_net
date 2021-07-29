using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.User;

namespace Liar.Application.Contracts.IServices.Sys
{
    public interface IAccountService: IAppService
    {   
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserValidateDto> LoginAsync(UserLoginDto input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultDetails<UserInfoDto>> GetUserInfoAsync(long id);
    }
}
