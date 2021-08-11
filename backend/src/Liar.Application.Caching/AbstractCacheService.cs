using System.Text;
using System.Threading.Tasks;
using Liar.Domain.Shared.Consts;
using Volo.Abp.DependencyInjection;

namespace Liar.Application.Caching
{
    public interface ICacheService
    {
        /// <summary>
        /// 预热缓存
        /// </summary>
        /// <returns></returns>
        Task PreheatAsync();
    }

    public abstract class AbstractCacheService : ICacheService, ITransientDependency
    {
        public abstract Task PreheatAsync();

        public virtual string ConcatCacheKey(params object[] items)
        {
            if (items == null || items.Length == 0)
                return string.Empty;

            var sbuilder = new StringBuilder();
            int index = 0;
            int total = items.Length;
            foreach (var item in items)
            {
                index++;
                sbuilder.Append(item);
                if (index != total)
                    sbuilder.Append(CachingConsts.LinkChar);
            }
            return sbuilder.ToString();
        }
    }
}
