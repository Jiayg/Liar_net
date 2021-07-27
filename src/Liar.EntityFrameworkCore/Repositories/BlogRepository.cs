using System.Threading.Tasks;
using Liar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore.Repositories
{
    public class BlogRepository : EfCoreRepository<LiarDbContext, Post>, IBlogRepository
    {
        public BlogRepository(IDbContextProvider<LiarDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<Post> GetInfo()
        {
            var db = await GetDbContextAsync();

            return await db.Set<Post>().FirstOrDefaultAsync();
        }
    }
}
