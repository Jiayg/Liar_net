using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.User
{
    public class UserLoginDto : IInputDto
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
