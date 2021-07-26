using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Liar.EntityFrameworkCore
{
    [DependsOn(
        typeof(LiarDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule),
        typeof(AbpEntityFrameworkCoreSqliteModule)
        )]
    public class LiarEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //LiarEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<LiarDbContext>(option =>
            {

            });

            //context.Services.AddAbpDbContext<LiarDbContext>(options =>
            //{
            //    /* Remove "includeAllEntities: true" to create
            //     * default repositories only for aggregate roots */
            //    options.AddDefaultRepositories(includeAllEntities: true);
            //});

            //Configure<AbpDbContextOptions>(options =>
            //{
            //    /* The main point to change your DBMS.
            //     * See also LiarMigrationsDbContextFactory for EF Core tooling. */
            //    options.UseMySQL();
            //});
        }
    }
}
