using Liar.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Liar.Domain.Repositories
{
    /// <summary>
    /// IPostRepository
    /// </summary>
    public interface IPostRepository : IRepository<Post, int>
    {

    }
}