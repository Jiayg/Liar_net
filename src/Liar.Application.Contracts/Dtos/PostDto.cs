using System;

namespace Liar.Application.Contracts.Dtos
{
    public class PostDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// HTML
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Markdown
        /// </summary>
        public string Markdown { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }
    }
}
