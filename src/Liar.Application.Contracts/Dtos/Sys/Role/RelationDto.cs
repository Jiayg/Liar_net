using System;
using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Role
{
    [Serializable]
    public class RelationDto : IDto
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public long? MenuId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long? RoleId { get; set; }
    }
}
