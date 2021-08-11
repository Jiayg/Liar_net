using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Role
{
    public class RoleMenuCodesDto : IDto
    {
        /// <summary>
        /// 菜单Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
    }
}
