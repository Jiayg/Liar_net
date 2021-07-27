using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Dapper Demo
    /// </summary>
    public class BaseDapperRepository : DapperRepository<LiarDbContext>, ITransientDependency
    {
        public BaseDapperRepository(IDbContextProvider<LiarDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<List<string>> GetAllPersonNames()
        {
            var db = await GetDbConnectionAsync();
            var dbt = await GetDbTransactionAsync();

            return (await db.QueryAsync<string>("select Name from People", transaction: dbt)).ToList();
        }

        public virtual async Task<int> UpdatePersonNames(string name)
        {
            var db = await GetDbConnectionAsync();
            var dbt = await GetDbTransactionAsync();

            return await db.ExecuteAsync("update People set Name = @NewName", new { NewName = name }, dbt);
        }
    }
}
