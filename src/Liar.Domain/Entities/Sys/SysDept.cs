using System.Collections.Generic;
using Liar.Domain.IEntities;

namespace Liar.Domain.Sys
{
    /// <summary>
    /// 部门表
    /// </summary>
    public class SysDept : EfFullAuditEntity
    {
        public string FullName { get; set; }

        public int Ordinal { get; set; }

        public long? Pid { get; set; }

        public string Pids { get; set; }

        public string SimpleName { get; set; }

        public string Tips { get; set; }

        public int? Version { get; set; }

        public virtual ICollection<SysUser> Users { get; set; }
    }
}
