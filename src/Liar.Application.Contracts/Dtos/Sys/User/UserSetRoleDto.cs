using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.User
{
    public class UserSetRoleDto : IInputDto
    {
        public long[] RoleIds { get; set; }
    }
}
