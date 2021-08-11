using Liar.Domain.Sys;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore
{
    public class LiarDbContext : AbpDbContext<LiarDbContext>, IEfCoreDbContext
    { 
        public DbSet<SysUser> SysUser { get; set; }
        public DbSet<SysMenu> SysMenu { get; set; }
        public DbSet<SysRelation> SysRelation { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysDept> SysDept { get; set; }

        public LiarDbContext(DbContextOptions<LiarDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureLiar();
        }
    }
}
