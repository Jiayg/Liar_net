using System.Collections.Generic;
using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Role
{
    public class RolePermissionsCheckerDto : IDto
    {
        public IEnumerable<long> RoleIds { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
