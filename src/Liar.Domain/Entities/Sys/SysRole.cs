using System.Collections.ObjectModel;
using Liar.Domain.IEntities;

namespace Liar.Domain.Sys
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class SysRole : EfFullAuditEntity
    { 
        public long? DeptId { get; set; }

        public string Name { get; set; }

        public int Ordinal { get; set; }

        public long? Pid { get; set; }

        public string Tips { get; set; }

        public int? Version { get; set; }

        public virtual Collection<SysRelation> Relations { get; set; }
    }
}
