using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.User
{
    public class UserRefreshTokenDto : IInputDto
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
