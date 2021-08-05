using Liar.Core.Microsoft.Extensions.Configuration;
using Liar.Domain.Shared.ConfigModels;
using Liar.MongoDB.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.Uow;

namespace Liar
{
    [DependsOn(
        typeof(LiarDomainModule),
        typeof(LiarDomainSharedModule),
        typeof(AbpMongoDbModule)
        )]
    public class LiarMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var mongoConfig = configuration.GetMongoDbSection().Get<MongoConfig>();

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = mongoConfig.ConnectionString;
            });

            context.Services.AddMongoDbContext<LiarMongoDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });
        }
    }
}
