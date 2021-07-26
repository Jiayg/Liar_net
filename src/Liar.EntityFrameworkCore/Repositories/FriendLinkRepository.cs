using Liar.Domain.Entities;
using Liar.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Liar.EntityFrameworkCore.Repositories.Blog
{
    /// <summary>
    /// FriendLinkRepository
    /// </summary>
    public class FriendLinkRepository : EfCoreRepository<LiarDbContext, FriendLink, int>, IFriendLinkRepository
    {
        public FriendLinkRepository(IDbContextProvider<LiarDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}