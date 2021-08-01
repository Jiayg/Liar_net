using System;
using System.Collections.Generic;
using System.Linq;

namespace Liar.Core.Liar
{
    public static class EnumerableExtensions
    {

        /// <summary>
        /// 分页计算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<T> Take<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            query.ThrowIfNull();

            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        public static void ThrowIfNull(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }
    }

}
