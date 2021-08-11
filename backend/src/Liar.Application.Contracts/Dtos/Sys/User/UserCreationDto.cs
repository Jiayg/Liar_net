namespace Liar.Application.Contracts.Dtos.Sys.User
{
    public class UserCreationDto : UserCreationAndUpdationDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
