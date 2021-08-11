using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared.ConfigModels;
using Liar.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Dapper;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace Liar
{
    [DependsOn(
        typeof(LiarDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
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
                options.ConnectionStrings.Default = mysqlConfig.MainConnectionString;
            });

            context.Services.AddAbpDbContext<LiarDbContext>(option =>
            {
                option.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL();
            });
        }
    }
}
