using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.User
{
    public class UserChangePwdDto : IInputDto
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 当前密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string RePassword { get; set; }
    }
}
