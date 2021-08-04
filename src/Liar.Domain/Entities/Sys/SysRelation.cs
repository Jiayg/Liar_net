using Liar.Domain.IEntities;

namespace Liar.Domain.Sys
{
    /// <summary>
    /// 菜单角色关系表
    /// </summary>
    public class SysRelation : EfEntity
    {
        public long MenuId { get; set; }

        public long RoleId { get; set; }

        public virtual SysRole Role { get; set; }

        public virtual SysMenu Menu { get; set; }
    }
}
