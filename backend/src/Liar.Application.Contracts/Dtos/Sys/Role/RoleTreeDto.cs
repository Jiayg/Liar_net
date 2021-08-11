using System;
using System.Collections.Generic;
using Liar.Application.Contracts.Dtos.Sys.Menu;
using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Role
{
    [Serializable]
    public class RoleTreeDto : IDto
    {
        public IEnumerable<Node<long>> TreeData { get; set; }
        public IEnumerable<long> CheckedIds { get; set; }
    }
}
