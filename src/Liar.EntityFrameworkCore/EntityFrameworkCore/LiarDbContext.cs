using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore
{
    [ConnectionStringName("MySQL")]
    public class LiarDbContext : AbpDbContext<LiarDbContext>
    {
        //public DbSet<AppUser> Users { get; set; }

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
