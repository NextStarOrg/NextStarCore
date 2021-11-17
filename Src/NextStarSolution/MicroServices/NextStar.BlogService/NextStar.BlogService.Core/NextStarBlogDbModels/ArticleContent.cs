using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.NextStarBlogDbModels
{
    public partial class ArticleContent
    {
        public int Id { get; set; }
        public Guid ArticleKey { get; set; }
        /// <summary>
        /// markdown text
        /// </summary>
        public string Origin { get; set; } = null!;
        public string ConvertHtml { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public virtual Article ArticleKeyNavigation { get; set; } = null!;
    }
}
