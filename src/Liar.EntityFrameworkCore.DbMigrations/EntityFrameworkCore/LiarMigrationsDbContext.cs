using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore
{
    public class LiarMigrationsDbContext : AbpDbContext<LiarMigrationsDbContext>
    {
        public LiarMigrationsDbContext(DbContextOptions<LiarMigrationsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 

            builder.ConfigureLiar();
        }
    }
}