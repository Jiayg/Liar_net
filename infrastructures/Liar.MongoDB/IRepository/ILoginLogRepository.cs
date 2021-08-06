using System;
using System.Threading.Tasks;
using Liar.MongoDB.MongoEntities;
using Volo.Abp.Domain.Repositories;

namespace Liar.MongoDB.IRepository
{
    public interface ILoginLogRepository : IRepository<LoginLog, Guid>
    {
        Task<bool> AddAsync(LoginLog entity);
    }
}
