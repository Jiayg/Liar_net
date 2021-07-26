using System.Threading.Tasks;

namespace Liar.Data
{
    public interface ILiarDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
