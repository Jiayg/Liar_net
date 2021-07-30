using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared;
using Liar.Domain.Shared.ConfigModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Dapper;
using Volo.Abp.Data;
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
        typeof(AbpEntityFrameworkCoreSqliteModule),
        typeof(AbpDapperModule)
        )]
    public class LiarEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var mysqlConfig = configuration.GetMysqlSection().Get<MysqlConfig>();

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = mysqlConfig.ConnectionString;
            });

            context.Services.AddAbpDbContext<LiarDbContext>(option =>
            {
                option.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                switch (mysqlConfig.DBType)
                {
                    case "MySQL":
                        options.UseMySQL();
                        break;
                    case "SqlServer":
                        options.UseSqlServer();
                        break;
                    case "PostgreSql":
                        options.UseNpgsql();
                        break;
                    case "Sqlite":
                        options.UseSqlite();
                        break;
                    default:
                        options.UseMySQL();
                        break;
                }
            });
        }
    }
}
