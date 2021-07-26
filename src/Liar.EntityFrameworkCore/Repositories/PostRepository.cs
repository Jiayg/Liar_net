using Liar.Domain.Entities;
using Liar.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore.Repositories.Blog
{
    /// <summary>
    /// PostRepository
    /// </summary>
    public class PostRepository : EfCoreRepository<LiarDbContext, Post, int>, IPostRepository
    {
        public PostRepository(IDbContextProvider<LiarDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}