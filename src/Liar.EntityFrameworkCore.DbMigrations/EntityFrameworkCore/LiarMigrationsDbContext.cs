using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See LiarDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class LiarMigrationsDbContext : AbpDbContext<LiarMigrationsDbContext>
    {
        public LiarMigrationsDbContext(DbContextOptions<LiarMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            //builder.ConfigurePermissionManagement();
            //builder.ConfigureSettingManagement();
            //builder.ConfigureBackgroundJobs();
            //builder.ConfigureAuditLogging();
            //builder.ConfigureIdentity();
            //builder.ConfigureIdentityServer();
            //builder.ConfigureFeatureManagement();
            //builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside the ConfigureLiar method */

            builder.ConfigureLiar();
        }
    }
}