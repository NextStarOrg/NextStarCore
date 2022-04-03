using System;
using System.Collections.Generic;

namespace NextStar.BlogService.Core.BlogDbModels
{
    public partial class Article
    {
        public Article()
        {
            ArticleContents = new HashSet<ArticleContent>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public virtual ArticleCategory ArticleCategory { get; set; } = null!;
        public virtual ICollection<ArticleContent> ArticleContents { get; set; }
    }
}
