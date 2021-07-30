using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using JetBrains.Annotations;
using Liar.Domain.Shared.Dtos;

namespace Liar.Domain.Shared
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    [Serializable]
    public class PageModelDto<T> : IDto
    {
        private IReadOnlyList<T> _data = Array.Empty<T>();
        public PageModelDto()
        {
        }

        public PageModelDto(SearchPagedDto search) : this(search, default, default)
        {
        }
        public PageModelDto(SearchPagedDto search, IReadOnlyList<T> data, int count, dynamic xData = null) : this(search.Limit, search.Offset, data, count)
        {
            this.XData = xData;
        }
        public PageModelDto(int limit, int offset, IReadOnlyList<T> data, int count, dynamic xData = null)
        {
            this.Limit = limit;
            this.Offset = offset;
            this.Total = count;
            this.Item = data;
            this.XData = xData;
        }

        private int Limit { get; set; }
        private int Offset { get; set; }
        private dynamic XData { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonPropertyName("item")]
        [NotNull]
        public IReadOnlyList<T> Item
        {
            get => _data;
            set
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (value != null)
                {
                    _data = value;
                }
            }
        }
    }
}
