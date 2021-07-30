using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Liar.Domain.Shared
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    [Serializable]
    public class PageModelDto<T>
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonPropertyName("item")]
        public List<T> Item { get; set; }
    }
}
