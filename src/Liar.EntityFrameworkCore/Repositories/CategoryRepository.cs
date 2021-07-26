using Liar.Domain.Entities;
using Liar.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Liar.EntityFrameworkCore.Repositories.Blog
{
    /// <summary>
    /// CategoryRepository
    /// </summary>
    public class CategoryRepository : EfCoreRepository<LiarDbContext, Category, int>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<LiarDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}