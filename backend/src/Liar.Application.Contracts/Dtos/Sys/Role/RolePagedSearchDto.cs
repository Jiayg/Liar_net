using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Role
{
    public class RolePagedSearchDto : SearchPagedDto
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
    }
}
