using System;
using System.Collections.Generic;
using System.Text;

namespace Liar.Domain.Shared.Dtos
{
    public interface ISearchPagedDto : IDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
