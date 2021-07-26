using Liar.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Liar.Domain.Repositories
{
    public interface IPostRepository : IRepository<Post, int>
    {
    }
}
