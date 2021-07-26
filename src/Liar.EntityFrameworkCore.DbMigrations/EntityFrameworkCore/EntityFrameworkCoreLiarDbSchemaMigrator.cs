using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Liar.Data;
using Volo.Abp.DependencyInjection;

namespace Liar.EntityFrameworkCore
{
    public class EntityFrameworkCoreLiarDbSchemaMigrator
        : ILiarDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreLiarDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the LiarMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<LiarMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}