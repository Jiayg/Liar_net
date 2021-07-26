using System.Collections.Generic;
using System.Threading.Tasks;
using Liar.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Liar.Domain.Repositories
{
    public interface ITagRepository : IRepository<Tag, int>
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<Tag> tags);
    }
}
