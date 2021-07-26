using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Liar.Domain.Repositories
{
    public interface IPostTagRepository : IRepository<PostTag, int>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="postTags"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<PostTag> postTags);
    }
}
