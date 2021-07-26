using Volo.Abp.Domain.Entities;

namespace Liar.Domain.Entities
{
    public class Category : Entity<int>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
