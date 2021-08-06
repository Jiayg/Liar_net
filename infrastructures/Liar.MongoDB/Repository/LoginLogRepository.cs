using System;
using System.Threading.Tasks;
using Liar.MongoDB.IRepository;
using Liar.MongoDB.MongoDb;
using Liar.MongoDB.MongoEntities;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Liar.MongoDB.Repository
{
    public class LoginLogRepository : MongoDbRepository<LiarMongoDbContext, LoginLog, Guid>, ILoginLogRepository
    {
        public LoginLogRepository(IMongoDbContextProvider<LiarMongoDbContext> contextProvider) : base(contextProvider)
        {
        }

        public async Task<bool> AddAsync(LoginLog entity)
        {
            return await InsertAsync(entity) != null;
        }
    }
}
