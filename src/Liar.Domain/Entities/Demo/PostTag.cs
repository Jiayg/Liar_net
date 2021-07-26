using Volo.Abp.Domain.Entities;

namespace Liar.Domain.Entities
{
    public class PostTag : Entity<int>
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public int TagId { get; set; }
    }
}
