using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Role
{
    public class RoleSetPermissonsDto : IDto
    {
        public long RoleId { set; get; }
        public long[] Permissions { get; set; }
    }
}
