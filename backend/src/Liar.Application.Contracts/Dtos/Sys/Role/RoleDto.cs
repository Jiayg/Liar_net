using System;
using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.Role
{
    [Serializable]
    public class RoleDto : OutputDto
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// 父级角色Id
        /// </summary>
        public long? Pid { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Tips { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public string Permissions { get; set; }
    }
}
