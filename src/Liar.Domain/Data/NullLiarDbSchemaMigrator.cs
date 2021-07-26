using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Liar.Data
{
    /* This is used if database provider does't define
     * ILiarDbSchemaMigrator implementation.
     */
    public class NullLiarDbSchemaMigrator : ILiarDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}