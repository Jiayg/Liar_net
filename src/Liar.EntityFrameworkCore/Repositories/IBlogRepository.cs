using System.Threading.Tasks;
using Liar.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Liar.EntityFrameworkCore.Repositories
{
    public interface IBlogRepository : IRepository<Post>
    {
        Task<Post> GetInfo();
    }
}
