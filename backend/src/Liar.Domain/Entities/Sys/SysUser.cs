using System;
using Liar.Domain.IEntities;

namespace Liar.Domain.Sys
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class SysUser : EfFullAuditEntity, ISoftDelete
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long? DeptId { get; set; }

        /// <summary>
        /// email
        /// </summary>
		public string Email { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 角色id列表，以逗号分隔
        /// </summary>
        public string RoleIds { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        public int? Version { get; set; }

        public bool IsDeleted { get; set; }

        public virtual SysDept Dept { get; set; }
    }
}
